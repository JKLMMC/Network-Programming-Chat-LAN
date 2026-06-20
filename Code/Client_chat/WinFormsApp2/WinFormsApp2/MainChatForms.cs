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

            // Sinh Avatar tĩnh
            picAvatar.Image = GenerateAvatar(username);
            SetupTyping();
            
            txtMessage.KeyDown += txtMessage_KeyDown;

            try {
                if (System.IO.File.Exists("log.txt"))
                    rtxtChatLog.Rtf = System.IO.File.ReadAllText("log.txt");
            } catch {}

            // Tự động kết nối Server khi mở Form Chat (phải đợi Form Load xong để có Handle cho Invoke)
            this.Load += (s, e) => ConnectToServer();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            try {
                System.IO.File.WriteAllText("log.txt", rtxtChatLog.Rtf);
            } catch {}
        }

        private string currentColorHex = "";
        private System.Windows.Forms.Timer typingTimer = new System.Windows.Forms.Timer { Interval = 2000 };

        private void txtMessage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (e.Shift)
                {
                    txtMessage.AppendText(Environment.NewLine);
                }
                else
                {
                    e.SuppressKeyPress = true; // Ngăn chặn tiếng 'ding' và xuống dòng
                    btnSend_Click(this, EventArgs.Empty);
                }
            }
        }

        private void InsertImageIntoRichTextBox(RichTextBox rtxt, string senderName, Image img, string fileName)
        {
            rtxt.SelectionFont = new Font(rtxt.Font, FontStyle.Bold);
            rtxt.AppendText($"[{DateTime.Now:HH:mm}] {senderName}: gửi ảnh [{fileName}]{Environment.NewLine}");
            rtxt.SelectionFont = rtxt.Font;

            try {
                IDataObject oldData = Clipboard.GetDataObject();
                Clipboard.SetImage(img);
                rtxt.ReadOnly = false;
                rtxt.Paste();
                rtxt.ReadOnly = true;
                rtxt.AppendText(Environment.NewLine);
                if (oldData != null) Clipboard.SetDataObject(oldData);
            } catch {
                rtxt.AppendText("[Không thể hiển thị ảnh trực tiếp]" + Environment.NewLine);
            }
            
            rtxt.SelectionStart = rtxt.Text.Length;
            rtxt.ScrollToCaret();
        }

        // Helper: hiển thị tin nhắn kèm tên người gửi in đậm, timestamp, và block trích dẫn nếu có
        private void AppendChatMessage(RichTextBox rtxt, string senderName, string content, Color msgColor)
        {
            // --- 1. In tên + giờ ---
            rtxt.SelectionFont = new Font(rtxt.Font, FontStyle.Bold);
            rtxt.SelectionColor = rtxt.ForeColor;
            rtxt.AppendText($"[{DateTime.Now:HH:mm}] {senderName}: ");
            rtxt.SelectionFont = rtxt.Font;

            // --- 2. Kiểm tra có phần trích dẫn [Trả lời: "..."] không ---
            string replyPrefix = "[Trả lời: “";
            string replySuffix = "”]";
            string mainContent = content;

            if (content.StartsWith(replyPrefix))
            {
                int endIdx = content.IndexOf(replySuffix);
                if (endIdx != -1)
                {
                    // Trích lấy đoạn trích dẫn
                    string quoted = content.Substring(replyPrefix.Length, endIdx - replyPrefix.Length);
                    // Phần nội dung thực sự sau dấu \n›››
                    int afterReply = content.IndexOf("››› ", endIdx);
                    mainContent = afterReply != -1 ? content.Substring(afterReply + 4) : "";

                    // Vẽ block trích dẫn (màu xám nghieng)
                    rtxt.AppendText(Environment.NewLine);
                    rtxt.SelectionFont = new Font(rtxt.Font, FontStyle.Italic);
                    rtxt.SelectionColor = Color.FromArgb(110, 110, 130);
                    rtxt.AppendText($"  ▍ «{quoted}»{Environment.NewLine}");
                    rtxt.SelectionFont = rtxt.Font;
                    rtxt.SelectionColor = rtxt.ForeColor;
                }
            }

            // --- 3. In nội dung chính ---
            if (!string.IsNullOrWhiteSpace(mainContent))
            {
                rtxt.SelectionColor = msgColor;
                rtxt.AppendText(mainContent + Environment.NewLine);
                rtxt.SelectionColor = rtxt.ForeColor;
            }
            else
            {
                rtxt.AppendText(Environment.NewLine);
            }

            rtxt.SelectionStart = rtxt.Text.Length;
            rtxt.ScrollToCaret();
        }

        private void btnEmoji_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn != null)
            {
                txtMessage.Text += btn.Text;
                txtMessage.SelectionStart = txtMessage.Text.Length;
                txtMessage.Focus();
            }
        }

        private void btnColor_Click(object sender, EventArgs e)
        {
            if (sharedColorDialog.ShowDialog() == DialogResult.OK) {
                currentColorHex = "#" + sharedColorDialog.Color.R.ToString("X2") + sharedColorDialog.Color.G.ToString("X2") + sharedColorDialog.Color.B.ToString("X2");
                btnColor.BackColor = sharedColorDialog.Color;
            }
        }

        public static Image GenerateAvatar(string name)
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
                string quotedText = rtxtChatLog.SelectedText.Trim();
                // Giới hạn độ dài trích dẫn
                if (quotedText.Length > 100) quotedText = quotedText.Substring(0, 100) + "...";
                txtMessage.Text = $"[Trả lời: “{quotedText}”]\n››› ";
                txtMessage.SelectionStart = txtMessage.Text.Length;
                txtMessage.Focus();
            }
        }

        private void ItemFwd_Click(object sender, EventArgs e)
        {
            if (rtxtChatLog.SelectionLength == 0)
            {
                MessageBox.Show("Vui lòng chọn (bôi đen) đoạn tin nhắn muốn chuyển tiếp trước!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string oldMsg = rtxtChatLog.SelectedText.Trim();
            if (oldMsg.Length > 200) oldMsg = oldMsg.Substring(0, 200) + "...";

            // Lấy danh sách người dùng (lọc bỏ (new) và bản thân)
            var onlineUsers = new System.Collections.Generic.List<string>();
            foreach (var item in lstOnlineUsers.Items)
            {
                string name = item.ToString().Replace(" (new)", "");
                if (name != "Tất cả" && name != username)
                    onlineUsers.Add(name);
            }

            if (onlineUsers.Count == 0)
            {
                MessageBox.Show("Không có người dùng nào đang online để chuyển tiếp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Tạo Form chuyển tiếp đẹp
            Form fwdForm = new Form
            {
                Text = "📤 Chuyển tiếp tin nhắn",
                Size = new Size(420, 260),
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false,
                BackColor = Color.FromArgb(245, 247, 250),
                Font = new Font("Segoe UI", 10)
            };

            // Preview đoạn tin chọn
            Label lblPreview = new Label
            {
                Text = $"\"{oldMsg}\"",
                Location = new Point(15, 15),
                Size = new Size(380, 60),
                ForeColor = Color.FromArgb(80, 80, 80),
                Font = new Font("Segoe UI", 9, FontStyle.Italic),
                AutoEllipsis = true
            };

            Label lblChoose = new Label
            {
                Text = "Chọn người nhận:",
                Location = new Point(15, 85),
                AutoSize = true,
                ForeColor = Color.FromArgb(50, 50, 50),
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };

            ComboBox cbUsers = new ComboBox
            {
                Location = new Point(15, 110),
                Width = 375,
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 10),
                BackColor = Color.White
            };
            foreach (string u in onlineUsers) cbUsers.Items.Add(u);
            cbUsers.SelectedIndex = 0;

            Button btnSendFwd = new Button
            {
                Text = "  Gửi ngay →",
                Location = new Point(235, 160),
                Size = new Size(155, 40),
                BackColor = Color.FromArgb(37, 99, 235),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnSendFwd.FlatAppearance.BorderSize = 0;

            Button btnCancel = new Button
            {
                Text = "Hủy",
                Location = new Point(145, 160),
                Size = new Size(80, 40),
                BackColor = Color.FromArgb(220, 220, 225),
                ForeColor = Color.FromArgb(50, 50, 50),
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10),
                Cursor = Cursors.Hand
            };
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.Click += (s, ev) => fwdForm.Close();

            btnSendFwd.Click += (s, ev) =>
            {
                if (cbUsers.SelectedItem != null && writer != null)
                {
                    string target = cbUsers.SelectedItem.ToString();
                    writer.WriteLine($"FWD:{target}:{oldMsg}");

                    // Hiển thông báo đẹp trong khung chat
                    rtxtChatLog.SelectionFont = new Font(rtxtChatLog.Font, FontStyle.Italic);
                    rtxtChatLog.SelectionColor = Color.FromArgb(120, 120, 120);
                    rtxtChatLog.AppendText($"  📤 Bạn đã chuyển tiếp tin nhắn đến {target}{Environment.NewLine}");
                    rtxtChatLog.SelectionFont = rtxtChatLog.Font;
                    rtxtChatLog.SelectionColor = rtxtChatLog.ForeColor;
                    rtxtChatLog.SelectionStart = rtxtChatLog.Text.Length;
                    rtxtChatLog.ScrollToCaret();

                    fwdForm.Close();
                }
            };

            fwdForm.Controls.AddRange(new Control[] { lblPreview, lblChoose, cbUsers, btnCancel, btnSendFwd });
            fwdForm.ShowDialog(this);
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
                                string selected = lstOnlineUsers.SelectedItem?.ToString();
                                
                                // Bảo tồn trạng thái (new)
                                System.Collections.Generic.List<string> newUsers = new System.Collections.Generic.List<string>();
                                foreach (var item in lstOnlineUsers.Items) {
                                    if (item.ToString().EndsWith(" (new)")) {
                                        newUsers.Add(item.ToString().Replace(" (new)", ""));
                                    }
                                }

                                lstOnlineUsers.Items.Clear();
                                string allItem = newUsers.Contains("Tất cả") ? "Tất cả (new)" : "Tất cả";
                                lstOnlineUsers.Items.Add(allItem);
                                
                                foreach (string u in users)
                                {
                                    if (!string.IsNullOrEmpty(u) && u != username) // Không tự hiện mình
                                    {
                                        string displayU = newUsers.Contains(u) ? u + " (new)" : u;
                                        lstOnlineUsers.Items.Add(displayU);
                                    }
                                }

                                if (!string.IsNullOrEmpty(selected)) {
                                    int idx = lstOnlineUsers.FindStringExact(selected);
                                    if (idx != -1) lstOnlineUsers.SelectedIndex = idx;
                                    else lstOnlineUsers.SelectedIndex = 0;
                                } else {
                                    lstOnlineUsers.SelectedIndex = 0;
                                }
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
                                    string senderNameOnly = chatMsg.Substring(0, splitIdx);

                                    // Cập nhật (new)
                                    if (senderNameOnly != username)
                                    {
                                        if (lstOnlineUsers.SelectedItem == null || !lstOnlineUsers.SelectedItem.ToString().StartsWith("Tất cả"))
                                        {
                                            int allIdx = lstOnlineUsers.FindStringExact("Tất cả");
                                            if (allIdx != -1) lstOnlineUsers.Items[allIdx] = "Tất cả (new)";
                                        }
                                    }
                                    
                                    if (content.StartsWith("#") && content.Contains("|"))
                                    {
                                        int pipeIdx = content.IndexOf("|");
                                        string hex = content.Substring(0, pipeIdx);
                                        if (hex.Length == 7) {
                                            try { msgColor = ColorTranslator.FromHtml(hex); } catch {}
                                            content = content.Substring(pipeIdx + 1);
                                        }
                                    }
                                    AppendChatMessage(rtxtChatLog, senderNameOnly, content, msgColor);
                                }
                                else {
                                    rtxtChatLog.AppendText($"[{DateTime.Now:HH:mm}] " + chatMsg + Environment.NewLine);
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
                                        pcf.OnlineUsersList = lstOnlineUsers;
                                        privateChats[senderName] = pcf;
                                        // Không Show() tự động nữa
                                    }
                                    privateChats[senderName].AppendMessage(senderName, msg);

                                    // Cập nhật (new)
                                    if (!privateChats[senderName].Visible || !privateChats[senderName].Focused)
                                    {
                                        int idx = lstOnlineUsers.FindStringExact(senderName);
                                        if (idx != -1) lstOnlineUsers.Items[idx] = senderName + " (new)";
                                    }
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
                                    bool isImage = false;
                                    string ext = System.IO.Path.GetExtension(fileName).ToLower();
                                    if (ext == ".jpg" || ext == ".jpeg" || ext == ".png" || ext == ".gif") isImage = true;

                                    if (isImage)
                                    {
                                        try {
                                            byte[] fileBytes = Convert.FromBase64String(fileData);
                                            using (System.IO.MemoryStream ms = new System.IO.MemoryStream(fileBytes))
                                            {
                                                Image img = Image.FromStream(ms);
                                                if (privateChats.ContainsKey(senderName) && !privateChats[senderName].IsDisposed)
                                                {
                                                    privateChats[senderName].InsertImage(senderName, img, fileName);
                                                }
                                                else
                                                {
                                                    InsertImageIntoRichTextBox(rtxtChatLog, senderName, img, fileName);
                                                }
                                            }
                                        } catch {}
                                    }
                                    else
                                    {
                                        if (privateChats.ContainsKey(senderName) && !privateChats[senderName].IsDisposed)
                                        {
                                            privateChats[senderName].AppendMessage(senderName, $"[Đã gửi file: {fileName}]");
                                        }
                                        else 
                                        {
                                            rtxtChatLog.SelectionColor = Color.Green;
                                            rtxtChatLog.AppendText($"[{senderName} đã gửi file: {fileName}]\n");
                                            rtxtChatLog.SelectionColor = rtxtChatLog.ForeColor;
                                        }

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
                            pcf.OnlineUsersList = lstOnlineUsers;
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
                        string ext = System.IO.Path.GetExtension(ofd.FileName).ToLower();
                        string[] allowedExtensions = { ".txt", ".doc", ".docx", ".pdf", ".xlsx", ".pptx", ".jpg", ".jpeg", ".png", ".gif", ".zip", ".rar" };
                        if (Array.IndexOf(allowedExtensions, ext) == -1)
                        {
                            MessageBox.Show("Định dạng file không được hỗ trợ để đảm bảo an toàn!", "Lỗi bảo mật", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        byte[] fileBytes = System.IO.File.ReadAllBytes(ofd.FileName);
                        if (fileBytes.Length > 5 * 1024 * 1024)
                        {
                            MessageBox.Show("Vui lòng gửi file dưới 5MB để đảm bảo kết nối!", "File quá lớn", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        // Sử dụng Convert.ToBase64String với InsertLineBreaks=None để tránh xuống dòng trong base64
                        string base64File = Convert.ToBase64String(fileBytes, Base64FormattingOptions.None);
                        string fileName = System.IO.Path.GetFileName(ofd.FileName);
                        string target = "Tất cả";
                        if (lstOnlineUsers.SelectedItem != null) target = lstOnlineUsers.SelectedItem.ToString().Replace(" (new)", "");
                        writer.WriteLine("FILE:" + target + ":" + fileName + ":" + base64File);
                        rtxtChatLog.SelectionFont = new Font(rtxtChatLog.Font, FontStyle.Bold);
                        rtxtChatLog.AppendText($"[{DateTime.Now:HH:mm}] Bạn: gửi file [{fileName}]{Environment.NewLine}");
                        rtxtChatLog.SelectionFont = rtxtChatLog.Font;
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
                string rawTarget = lstOnlineUsers.SelectedItem.ToString();
                string target = rawTarget.Replace(" (new)", "");
                
                // Cập nhật lại list box để bỏ (new)
                if (rawTarget != target)
                {
                    int idx = lstOnlineUsers.SelectedIndex;
                    lstOnlineUsers.Items[idx] = target;
                }

                if (target != "Tất cả" && target != username)
                {
                    if (!privateChats.ContainsKey(target) || privateChats[target].IsDisposed)
                    {
                        PrivateChatForm pcf = new PrivateChatForm(username, target, writer);
                        pcf.OnlineUsersList = lstOnlineUsers;
                        privateChats[target] = pcf;
                        pcf.Show();
                    }
                    else
                    {
                        if (!privateChats[target].Visible) privateChats[target].Show();
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