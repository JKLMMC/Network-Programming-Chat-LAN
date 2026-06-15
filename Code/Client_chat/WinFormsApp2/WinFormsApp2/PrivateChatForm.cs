using System;
using System.Drawing;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace WinFormsApp2
{
    public class PrivateChatForm : Form
    {
        private string myUsername;
        private string targetUsername;
        private StreamWriter writer;

        private RichTextBox rtxtChat;
        private TextBox txtInput;
        private Button btnSendMsg;
        private Button btnSendFile;

        public PrivateChatForm(string me, string target, StreamWriter writer)
        {
            this.myUsername = me;
            this.targetUsername = target;
            this.writer = writer;

            InitializePrivateComponent();
        }

        private void InitializePrivateComponent()
        {
            this.Text = $"Chat riêng với: {targetUsername}";
            this.Size = new Size(450, 420);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            // Khung hiển thị nội dung chat
            rtxtChat = new RichTextBox { Dock = DockStyle.Top, Height = 290, ReadOnly = true, BackColor = Color.White, Font = new Font("Segoe UI", 10) };
            
            // Ô nhập tin nhắn
            txtInput = new TextBox { Location = new Point(12, 315), Width = 230, Multiline = true, Height = 50, Font = new Font("Segoe UI", 10) };

            // Nút gửi file
            btnSendFile = new Button { Location = new Point(248, 314), Width = 80, Height = 50, Text = "Gửi file", BackColor = Color.LightGray, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
            btnSendFile.Click += BtnSendFile_Click;
            
            // Nút gửi tin nhắn riêng
            btnSendMsg = new Button { Location = new Point(334, 314), Width = 88, Height = 50, Text = "Gửi", BackColor = SystemColors.Highlight, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 10, FontStyle.Bold) };

            btnSendMsg.Click += BtnSendMsg_Click;
            this.AcceptButton = btnSendMsg; // Bấm Enter là gửi tin luôn cho tiện

            this.Controls.Add(rtxtChat);
            this.Controls.Add(txtInput);
            this.Controls.Add(btnSendFile);
            this.Controls.Add(btnSendMsg);
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

        private void BtnSendMsg_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtInput.Text) && writer != null)
            {
                try
                {
                    string msgText = txtInput.Text.Trim();
                    msgText = FilterProfanity(msgText);
                    
                    // Gửi lệnh CHAT RIÊNG cho Server
                    // Cú pháp: PRIVATE:NgườiNhận:NộiDung
                    writer.WriteLine($"PRIVATE:{targetUsername}:{msgText}");

                    // Tự in tin nhắn của mình lên khung chat
                    AppendMessage("Tôi", msgText);
                    txtInput.Clear();
                    txtInput.Focus();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Không thể gửi tin nhắn riêng: " + ex.Message);
                }
            }
        }

        private void BtnSendFile_Click(object sender, EventArgs e)
        {
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

                        // Gửi file bằng lệnh FILE qua server
                        writer.WriteLine("FILE:" + targetUsername + ":" + fileName + ":" + base64File);
                        
                        // Báo lên khung chat riêng
                        rtxtChat.SelectionColor = Color.Green;
                        rtxtChat.AppendText($"[Bạn đã gửi file: {fileName}]\n");
                        rtxtChat.SelectionColor = rtxtChat.ForeColor;
                        rtxtChat.SelectionStart = rtxtChat.Text.Length;
                        rtxtChat.ScrollToCaret();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi gửi file: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Hàm giúp các file khác ghi đè tin nhắn nhận được vào khung hiển thị
        public void AppendMessage(string sender, string message)
        {
            if (rtxtChat.InvokeRequired)
            {
                rtxtChat.Invoke(new Action(() => AppendMessage(sender, message)));
            }
            else
            {
                rtxtChat.AppendText($"{sender}: {message}{Environment.NewLine}");
                rtxtChat.SelectionStart = rtxtChat.Text.Length;
                rtxtChat.ScrollToCaret(); // Tự cuộn màn hình xuống tin nhắn mới nhất
            }
        }
    }
}
