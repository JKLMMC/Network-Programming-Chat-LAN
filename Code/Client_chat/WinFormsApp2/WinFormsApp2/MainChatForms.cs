using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Net.Sockets; 
using System.Text;        
using System.Threading.Tasks; 
using System.Windows.Forms;

namespace WinFormsApp2
{
    public partial class MainChatForms : Form
    {
        private TcpClient client;
        private NetworkStream stream;
        private System.IO.StreamReader reader;
        private System.IO.StreamWriter writer;
        private System.Collections.Generic.Dictionary<string, PrivateChatForm> privateChats = new System.Collections.Generic.Dictionary<string, PrivateChatForm>();

        private string username;

        public MainChatForms(string name)
        {
            InitializeComponent();
            
            // Lưu tên người dùng trước khi khởi tạo UI mở rộng
            username = name;

            InitializeExtraComponents();
            SetupTyping();

            try {
                if (System.IO.File.Exists("log.txt"))
                    rtxtChatLog.Rtf = System.IO.File.ReadAllText("log.txt");
            } catch {}

            // Tự động kết nối Server khi mở Form Chat
            ConnectToServer();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            try {
                System.IO.File.WriteAllText("log.txt", rtxtChatLog.Rtf);
            } catch {}
        }

        private PictureBox picAvatar;
        private Panel pnlEmoji;
        private Label lblTyping;
        private Button btnColor;
        private ContextMenuStrip ctxMenu;
        private ColorDialog sharedColorDialog;
        private string currentColorHex = "";
        private System.Windows.Forms.Timer typingTimer = new System.Windows.Forms.Timer { Interval = 2000 };

        private void InitializeExtraComponents()
        {
            // 1. Avatar
            picAvatar = new PictureBox { Size = new Size(50, 50), Location = new Point(10, 10), SizeMode = PictureBoxSizeMode.Zoom };
            splitContainer1.Panel1.Controls.Add(picAvatar);
            picAvatar.BringToFront();
            lstOnlineUsers.Dock = DockStyle.None;
            lstOnlineUsers.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lstOnlineUsers.Location = new Point(0, 70);
            lstOnlineUsers.Size = new Size(splitContainer1.Panel1.Width, splitContainer1.Panel1.Height - 70);
            picAvatar.Image = GenerateAvatar(username);

            // 2. Panel Emoji & Đổi Màu
            panel1.Height = 110;
            pnlEmoji = new Panel { Location = new Point(10, 5), Size = new Size(250, 30) };
            string[] emojis = { "👍", "😂", "❤️", "😡", "😭" };
            for(int i=0; i<emojis.Length; i++) {
                Button btnE = new Button { Text = emojis[i], Size = new Size(30, 30), Location = new Point(i * 35, 0), FlatStyle = FlatStyle.Flat };
                btnE.Click += (s, e) => { txtMessage.Text += btnE.Text; txtMessage.SelectionStart = txtMessage.Text.Length; txtMessage.Focus(); };
                pnlEmoji.Controls.Add(btnE);
            }
            panel1.Controls.Add(pnlEmoji);
            
            txtMessage.Location = new Point(10, 40);
            txtMessage.Height = 64;

            sharedColorDialog = new ColorDialog();
            btnColor = new Button { Text = "Màu", Size = new Size(60, 30), Location = new Point(270, 5), FlatStyle = FlatStyle.Flat, BackColor = Color.LightGray };
            btnColor.Click += (s, e) => {
                if (sharedColorDialog.ShowDialog() == DialogResult.OK) {
                    currentColorHex = "#" + sharedColorDialog.Color.R.ToString("X2") + sharedColorDialog.Color.G.ToString("X2") + sharedColorDialog.Color.B.ToString("X2");
                    btnColor.BackColor = sharedColorDialog.Color;
                }
            };
            panel1.Controls.Add(btnColor);

            // 3. Label Typing
            lblTyping = new Label { Text = "Ai đó đang gõ...", ForeColor = Color.Gray, Location = new Point(340, 10), AutoSize = true, Visible = false };
            panel1.Controls.Add(lblTyping);

            // 4. Context Menu cho rtxtChatLog
            ctxMenu = new ContextMenuStrip();
            var itemReply = ctxMenu.Items.Add("Trả lời");
            itemReply.Click += ItemReply_Click;
            var itemFwd = ctxMenu.Items.Add("Chuyển tiếp");
            itemFwd.Click += ItemFwd_Click;
            rtxtChatLog.ContextMenuStrip = ctxMenu;
        }

        private Image GenerateAvatar(string name)
        {
            Bitmap bmp = new Bitmap(50, 50);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                int hash = name.GetHashCode();
                Color bg = Color.FromArgb(255, Math.Abs(hash & 255), Math.Abs((hash >> 8) & 255), Math.Abs((hash >> 16) & 255));
                g.FillEllipse(new SolidBrush(bg), 0, 0, 50, 50);
                string firstLetter = name.Length > 0 ? name.Substring(0, 1).ToUpper() : "?";
                SizeF size = g.MeasureString(firstLetter, new Font("Segoe UI", 20, FontStyle.Bold));
                g.DrawString(firstLetter, new Font("Segoe UI", 20, FontStyle.Bold), Brushes.White, (50 - size.Width) / 2, (50 - size.Height) / 2);
            }
            return bmp;
        }

        private void SetupTyping()
        {
            txtMessage.TextChanged += (s, e) => {
                if (writer != null && !string.IsNullOrEmpty(txtMessage.Text)) {
                    try { writer.WriteLine("TYPING:" + username); } catch {}
                }
            };
            typingTimer.Tick += (s, e) => {
                lblTyping.Visible = false;
                typingTimer.Stop();
            };
        }

        private void ItemReply_Click(object sender, EventArgs e)
        {
            if (rtxtChatLog.SelectionLength > 0)
            {
                txtMessage.Text = $"[Trả lời: \"{rtxtChatLog.SelectedText}\"]\n>>> ";
                txtMessage.SelectionStart = txtMessage.Text.Length;
                txtMessage.Focus();
            }
        }

        private void ItemFwd_Click(object sender, EventArgs e)
        {
            if (rtxtChatLog.SelectionLength > 0)
            {
                string oldMsg = rtxtChatLog.SelectedText;
                Form fwdForm = new Form { Text = "Chuyển tiếp", Size = new Size(300, 150), StartPosition = FormStartPosition.CenterParent };
                ComboBox cbUsers = new ComboBox { Location = new Point(20, 20), Width = 240, DropDownStyle = ComboBoxStyle.DropDownList };
                foreach (var item in lstOnlineUsers.Items) {
                    if (item.ToString() != "Tất cả" && item.ToString() != username)
                        cbUsers.Items.Add(item.ToString());
                }
                if (cbUsers.Items.Count > 0) cbUsers.SelectedIndex = 0;
                
                Button btnSendFwd = new Button { Text = "Gửi", Location = new Point(100, 60), Width = 80 };
                btnSendFwd.Click += (s, ev) => {
                    if (cbUsers.SelectedItem != null && writer != null) {
                        string target = cbUsers.SelectedItem.ToString();
                        writer.WriteLine($"FWD:{target}:{oldMsg}");
                        rtxtChatLog.AppendText($"[Đã chuyển tiếp tin nhắn đến {target}]\n");
                        fwdForm.Close();
                    }
                };
                fwdForm.Controls.Add(cbUsers);
                fwdForm.Controls.Add(btnSendFwd);
                fwdForm.ShowDialog();
            }
        }

        private void ConnectToServer()
        {
            try
            {
                client = new TcpClient("127.0.0.1", 8888);
                stream = client.GetStream();
                reader = new System.IO.StreamReader(stream, Encoding.UTF8);
                writer = new System.IO.StreamWriter(stream, Encoding.UTF8) { AutoFlush = true };

                // 1. Gửi tên người dùng lên Server ngay khi kết nối thành công (Dùng chuẩn LOGIN)
                writer.WriteLine("LOGIN:" + username);

                // 2. Chạy luồng ngầm nhận tin nhắn liên tục
                Task.Run(() => ListenForMessages());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối đến Server: " + ex.Message, "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ListenForMessages()
        {
            while (client != null && client.Connected)
            {
                try
                {
                    string message = reader.ReadLine();
                    if (message != null)
                    {
                        // PHÂN TÍCH TIN NHẮN TỪ SERVER GỬI VỀ
                        if (message.StartsWith("LIST:"))
                        {
                            string[] users = message.Substring(5).Split(',');
                            this.Invoke(new Action(() =>
                            {
                                lstOnlineUsers.Items.Clear();
                                lstOnlineUsers.Items.Add("Tất cả"); // Mặc định là chat nhóm
                                foreach (string u in users)
                                {
                                    if (!string.IsNullOrEmpty(u) && u != username) // Không tự hiện mình
                                    {
                                        lstOnlineUsers.Items.Add(u);
                                    }
                                }
                                lstOnlineUsers.SelectedIndex = 0; // Chọn sẵn "Tất cả"
                            }));
                        }
                        else if (message.StartsWith("CHAT:"))
                        {
                            string chatMsg = message.Substring(5);
                            this.Invoke(new Action(() =>
                            {
                                Color msgColor = rtxtChatLog.ForeColor;
                                int splitIdx = chatMsg.IndexOf(": ");
                                if (splitIdx != -1) {
                                    string content = chatMsg.Substring(splitIdx + 2);
                                    string senderPrefix = chatMsg.Substring(0, splitIdx + 2);
                                    
                                    if (content.StartsWith("#") && content.Contains("|"))
                                    {
                                        int pipeIdx = content.IndexOf("|");
                                        string hex = content.Substring(0, pipeIdx);
                                        if (hex.Length == 7) {
                                            try { msgColor = ColorTranslator.FromHtml(hex); } catch {}
                                            content = content.Substring(pipeIdx + 1);
                                        }
                                    }
                                    rtxtChatLog.AppendText(senderPrefix);
                                    rtxtChatLog.SelectionColor = msgColor;
                                    rtxtChatLog.AppendText(content + Environment.NewLine);
                                    rtxtChatLog.SelectionColor = rtxtChatLog.ForeColor;
                                }
                                else {
                                    rtxtChatLog.AppendText(chatMsg + Environment.NewLine);
                                }
                            }));
                        }
                        else if (message.StartsWith("TYPING:"))
                        {
                            string typer = message.Substring(7);
                            if (typer != username)
                            {
                                this.Invoke(new Action(() => {
                                    lblTyping.Text = typer + " đang gõ...";
                                    lblTyping.Visible = true;
                                    typingTimer.Stop();
                                    typingTimer.Start();
                                }));
                            }
                        }
                        else if (message.StartsWith("PRIVATE_CHAT:"))
                        {
                            string[] parts = message.Split(new char[] { ':' }, 3);
                            if (parts.Length == 3)
                            {
                                string senderName = parts[1];
                                string msg = parts[2];
                                this.Invoke(new Action(() =>
                                {
                                    if (!privateChats.ContainsKey(senderName) || privateChats[senderName].IsDisposed)
                                    {
                                        PrivateChatForm pcf = new PrivateChatForm(username, senderName, writer);
                                        privateChats[senderName] = pcf;
                                        pcf.Show();
                                    }
                                    privateChats[senderName].AppendMessage(senderName, msg);
                                }));
                            }
                        }
                        else if (message.StartsWith("FILE:"))
                        {
                            // Cú pháp từ server: FILE:Sender:FileName:Base64
                            string[] parts = message.Split(new char[] { ':' }, 4);
                            if (parts.Length == 4)
                            {
                                string senderName = parts[1];
                                string fileName = parts[2];
                                string fileData = parts[3];

                                this.Invoke(new Action(() =>
                                {
                                    rtxtChatLog.SelectionColor = Color.Green;
                                    rtxtChatLog.AppendText($"[{senderName} đã gửi file: {fileName}]\n");
                                    rtxtChatLog.SelectionColor = rtxtChatLog.ForeColor;

                                    DialogResult res = MessageBox.Show($"Bạn nhận được file '{fileName}' từ {senderName}.\nBạn có muốn tải về máy không?", "Nhận File", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                                    if (res == DialogResult.Yes)
                                    {
                                        using (SaveFileDialog sfd = new SaveFileDialog())
                                        {
                                            sfd.FileName = fileName;
                                            if (sfd.ShowDialog() == DialogResult.OK)
                                            {
                                                byte[] fileBytes = Convert.FromBase64String(fileData);
                                                System.IO.File.WriteAllBytes(sfd.FileName, fileBytes);
                                                MessageBox.Show("Lưu file thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            }
                                        }
                                    }
                                }));
                            }
                        }
                    }
                }
                catch
                {
                    break;
                }
            }
        }

        private string[] badWords = new string[] { "cặc", "đụ", "lồn", "đĩ", "fuck", "shit" };

        private string FilterProfanity(string text)
        {
            foreach (string word in badWords)
            {
                text = System.Text.RegularExpressions.Regex.Replace(text, word, "***", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            }
            return text;
        }

        // XỬ LÝ SỰ KIỆN BẤM NÚT "GỬI" (Đã map chuẩn biến txtMessage và stream)
        public void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                string msgText = txtMessage.Text.Trim();
                if (string.IsNullOrWhiteSpace(msgText)) return;

                if (msgText.Length > 500) msgText = msgText.Substring(0, 500);

                msgText = FilterProfanity(msgText);

                if (!string.IsNullOrEmpty(currentColorHex))
                {
                    msgText = currentColorHex + "|" + msgText;
                }

                if (stream != null)
                {
                    string target = "Tất cả";
                    if (lstOnlineUsers.SelectedItem != null)
                    {
                        target = lstOnlineUsers.SelectedItem.ToString();
                    }

                    if (target == "Tất cả")
                    {
                        writer.WriteLine("CHAT:" + msgText);
                    }
                    else
                    {
                        if (!privateChats.ContainsKey(target) || privateChats[target].IsDisposed)
                        {
                            PrivateChatForm pcf = new PrivateChatForm(username, target, writer);
                            privateChats[target] = pcf;
                            pcf.Show();
                        }
                        privateChats[target].Focus();
                        MessageBox.Show("Vui lòng chat riêng trong cửa sổ vừa mở ra!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    txtMessage.Clear(); // Dọn trống ô nhập
                    txtMessage.Focus(); // Đưa con trỏ chuột về gõ tiếp
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể gửi tin nhắn: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSendFile_Click(object sender, EventArgs e)
        {
            // ... (Phần code gửi file giữ nguyên) ...
            try
            {
                using (OpenFileDialog ofd = new OpenFileDialog())
                {
                    ofd.Title = "Chọn file cần gửi";
                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        byte[] fileBytes = System.IO.File.ReadAllBytes(ofd.FileName);
                        if (fileBytes.Length > 5 * 1024 * 1024)
                        {
                            MessageBox.Show("Vui lòng gửi file dưới 5MB để đảm bảo kết nối!", "File quá lớn", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        string base64File = Convert.ToBase64String(fileBytes);
                        string fileName = System.IO.Path.GetFileName(ofd.FileName);
                        string target = "Tất cả";
                        if (lstOnlineUsers.SelectedItem != null) target = lstOnlineUsers.SelectedItem.ToString();
                        writer.WriteLine("FILE:" + target + ":" + fileName + ":" + base64File);
                        rtxtChatLog.SelectionColor = Color.Green;
                        rtxtChatLog.AppendText($"[Bạn đã gửi file: {fileName}]\n");
                        rtxtChatLog.SelectionColor = rtxtChatLog.ForeColor;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi gửi file: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lstOnlineUsers_DoubleClick(object sender, EventArgs e)
        {
            if (lstOnlineUsers.SelectedItem != null)
            {
                string target = lstOnlineUsers.SelectedItem.ToString();
                if (target != "Tất cả" && target != username)
                {
                    if (!privateChats.ContainsKey(target) || privateChats[target].IsDisposed)
                    {
                        PrivateChatForm pcf = new PrivateChatForm(username, target, writer);
                        privateChats[target] = pcf;
                        pcf.Show();
                    }
                    else
                    {
                        privateChats[target].Focus();
                    }
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void lstOnlineUsers_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}