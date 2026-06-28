#nullable disable
using System;
using System.Drawing;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace WinFormsApp2
{
    public partial class PrivateChatForm : Form
    {
        private string myUsername;
        private string targetUsername;
        private StreamWriter writer;
        // Tham chiếu tới danh sách online từ MainChatForms
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public System.Windows.Forms.ListBox OnlineUsersList { get; set; }

        private string currentColorHex = "";
        
        private Panel pnlHoverMenu;
        private string hoveredMessageText = "";
        private int lastHoverLine = -1;

        private void SetupHoverMenu()
        {
            pnlHoverMenu = new Panel();
            pnlHoverMenu.Size = new Size(70, 30);
            pnlHoverMenu.BackColor = Color.FromArgb(245, 245, 245);
            pnlHoverMenu.Visible = false;

            Button btnHoverReply = new Button();
            btnHoverReply.Text = "↩️";
            btnHoverReply.Size = new Size(30, 26);
            btnHoverReply.Location = new Point(2, 2);
            btnHoverReply.Cursor = Cursors.Hand;
            btnHoverReply.FlatStyle = FlatStyle.Flat;
            btnHoverReply.FlatAppearance.BorderSize = 0;
            btnHoverReply.Click += BtnHoverReply_Click;
            new ToolTip().SetToolTip(btnHoverReply, "Trả lời tin nhắn này");

            Button btnHoverFwd = new Button();
            btnHoverFwd.Text = "⏩";
            btnHoverFwd.Size = new Size(30, 26);
            btnHoverFwd.Location = new Point(36, 2);
            btnHoverFwd.Cursor = Cursors.Hand;
            btnHoverFwd.FlatStyle = FlatStyle.Flat;
            btnHoverFwd.FlatAppearance.BorderSize = 0;
            btnHoverFwd.Click += BtnHoverFwd_Click;
            new ToolTip().SetToolTip(btnHoverFwd, "Chuyển tiếp tin nhắn này");

            pnlHoverMenu.Controls.Add(btnHoverReply);
            pnlHoverMenu.Controls.Add(btnHoverFwd);
            rtxtChat.Controls.Add(pnlHoverMenu);

            rtxtChat.MouseMove += RtxtChat_MouseMove;
            rtxtChat.MouseLeave += RtxtChat_MouseLeave;
            pnlHoverMenu.MouseLeave += (s, ev) =>
            {
                Point mousePos = rtxtChat.PointToClient(Cursor.Position);
                if (!rtxtChat.ClientRectangle.Contains(mousePos))
                {
                    pnlHoverMenu.Visible = false;
                }
            };
        }

        private void RtxtChat_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                RichTextBox rtxt = (RichTextBox)sender;
                if (rtxt.TextLength == 0) return;

                int charIndex = rtxt.GetCharIndexFromPosition(e.Location);

                // Nếu chuột nằm quá dòng cuối cùng, ẩn menu
                Point lastCharPt = rtxt.GetPositionFromCharIndex(rtxt.TextLength - 1);
                if (e.Y > lastCharPt.Y + rtxt.Font.Height)
                {
                    pnlHoverMenu.Visible = false;
                    hoveredMessageText = "";
                    return;
                }

                string allText = rtxt.Text;
                System.Text.RegularExpressions.MatchCollection matches = System.Text.RegularExpressions.Regex.Matches(allText, @"^\[\d{2}:\d{2}\]", System.Text.RegularExpressions.RegexOptions.Multiline);

                System.Text.RegularExpressions.Match currentMessageMatch = null;
                System.Text.RegularExpressions.Match nextMessageMatch = null;

                for (int i = 0; i < matches.Count; i++)
                {
                    if (matches[i].Index <= charIndex)
                    {
                        currentMessageMatch = matches[i];
                        if (i + 1 < matches.Count)
                            nextMessageMatch = matches[i + 1];
                        else
                            nextMessageMatch = null;
                    }
                    else
                    {
                        break;
                    }
                }

                if (currentMessageMatch != null)
                {
                    int start = currentMessageMatch.Index;
                    int end = nextMessageMatch != null ? nextMessageMatch.Index : allText.Length;
                    string fullMsg = allText.Substring(start, end - start).Trim();

                    // Chỉ update và show lại nếu nội dung thay đổi hoặc menu đang bị ẩn
                    if (hoveredMessageText != fullMsg || !pnlHoverMenu.Visible)
                    {
                        hoveredMessageText = fullMsg;
                        Point startPt = rtxt.GetPositionFromCharIndex(start);

                        int menuY = startPt.Y;
                        if (menuY < 0) menuY = 0; // Fix lỗi bị che khi cuộn lên trên

                        pnlHoverMenu.Location = new Point(rtxt.ClientSize.Width - pnlHoverMenu.Width - 10, menuY);
                        pnlHoverMenu.Visible = true;
                        pnlHoverMenu.BringToFront();
                    }
                }
                else
                {
                    pnlHoverMenu.Visible = false;
                    hoveredMessageText = "";
                }
            }
            catch { pnlHoverMenu.Visible = false; }
        }

        private void RtxtChat_MouseLeave(object sender, EventArgs e)
        {
            Point mousePos = rtxtChat.PointToClient(Cursor.Position);
            if (!pnlHoverMenu.Bounds.Contains(mousePos))
            {
                pnlHoverMenu.Visible = false;
                hoveredMessageText = "";
            }
        }

        private void BtnHoverReply_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(hoveredMessageText))
            {
                string quotedText = hoveredMessageText;
                if (quotedText.Length > 100) quotedText = quotedText.Substring(0, 100) + "...";
                txtInput.Text = $"[Trả lời: “{quotedText}”]\n››› ";
                txtInput.SelectionStart = txtInput.Text.Length;
                txtInput.Focus();
                pnlHoverMenu.Visible = false;
            }
        }

        private void BtnHoverFwd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(hoveredMessageText)) return;
            string oldMsg = hoveredMessageText;
            if (oldMsg.Length > 200) oldMsg = oldMsg.Substring(0, 200) + "...";

            var onlineUsers = new System.Collections.Generic.List<string>();
            if (OnlineUsersList != null)
            {
                foreach (var item in OnlineUsersList.Items)
                {
                    string name = item.ToString().Replace(" (new)", "");
                    if (name != "Tất cả" && name != myUsername && name != targetUsername)
                        onlineUsers.Add(name);
                }
            }

            if (onlineUsers.Count == 0)
            {
                MessageBox.Show("Không có người dùng nào khác online để chuyển tiếp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

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

            Label lblPreview = new Label { Text = $"\"{oldMsg}\"", Location = new Point(15, 15), Size = new Size(380, 60), ForeColor = Color.FromArgb(80, 80, 80), Font = new Font("Segoe UI", 9, FontStyle.Italic), AutoEllipsis = true };
            Label lblChoose = new Label { Text = "Chọn người nhận:", Location = new Point(15, 85), AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
            ComboBox cbUsers = new ComboBox { Location = new Point(15, 110), Width = 375, DropDownStyle = ComboBoxStyle.DropDownList, Font = new Font("Segoe UI", 10), BackColor = Color.White };
            foreach (string u in onlineUsers) cbUsers.Items.Add(u);
            cbUsers.SelectedIndex = 0;

            Button btnSend = new Button { Text = "  Gửi ngay →", Location = new Point(235, 160), Size = new Size(155, 40), BackColor = Color.FromArgb(37, 99, 235), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 10, FontStyle.Bold), Cursor = Cursors.Hand };
            btnSend.FlatAppearance.BorderSize = 0;

            Button btnCancel2 = new Button { Text = "Hủy", Location = new Point(145, 160), Size = new Size(80, 40), BackColor = Color.FromArgb(220, 220, 225), ForeColor = Color.FromArgb(50, 50, 50), FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 10), Cursor = Cursors.Hand };
            btnCancel2.FlatAppearance.BorderSize = 0;
            btnCancel2.Click += (s, ev) => fwdForm.Close();

            btnSend.Click += (s, ev) =>
            {
                if (cbUsers.SelectedItem != null && writer != null)
                {
                    string target = cbUsers.SelectedItem.ToString();
                    writer.WriteLine($"FWD:{target}:{oldMsg.Replace("\r", "").Replace("\n", "<br>")}");
                    rtxtChat.SelectionFont = new Font(rtxtChat.Font, FontStyle.Italic);
                    rtxtChat.SelectionColor = Color.FromArgb(120, 120, 120);
                    rtxtChat.AppendText($"  📤 Bạn đã chuyển tiếp đến {target}{Environment.NewLine}");
                    rtxtChat.SelectionFont = rtxtChat.Font;
                    rtxtChat.SelectionColor = rtxtChat.ForeColor;
                    fwdForm.Close();
                }
            };

            fwdForm.Controls.AddRange(new Control[] { lblPreview, lblChoose, cbUsers, btnCancel2, btnSend });
            fwdForm.ShowDialog(this);
            pnlHoverMenu.Visible = false;
        }

        public PrivateChatForm(string me, string target, StreamWriter writer)
        {
            InitializeComponent();
            this.myUsername = me;
            this.targetUsername = target;
            this.writer = writer;
            this.Text = $"Chat riêng với: {targetUsername}";
            this.AcceptButton = btnSendMsg;
            this.lblName.Text = targetUsername;
            this.picAvatar.Image = MainChatForms.GetAvatar(targetUsername);
            SetupHoverMenu();

            try
            {
                string logFile = $"PrivateLog_{myUsername}_{targetUsername}.txt";
                if (System.IO.File.Exists(logFile))
                    rtxtChat.Rtf = System.IO.File.ReadAllText(logFile);
            }
            catch { }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            try
            {
                System.IO.File.WriteAllText($"PrivateLog_{myUsername}_{targetUsername}.txt", rtxtChat.Rtf);
            }
            catch { }
        }

        private void btnEmoji_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn != null)
            {
                txtInput.Text += btn.Text;
                txtInput.SelectionStart = txtInput.Text.Length;
                txtInput.Focus();
            }
        }

        private void btnColor_Click(object sender, EventArgs e)
        {
            if (sharedColorDialog.ShowDialog() == DialogResult.OK)
            {
                currentColorHex = "#" + sharedColorDialog.Color.R.ToString("X2") + sharedColorDialog.Color.G.ToString("X2") + sharedColorDialog.Color.B.ToString("X2");
                btnColor.BackColor = sharedColorDialog.Color;
            }
        }

        private void ItemReply_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(rtxtChat.SelectedText))
            {
                string quotedText = rtxtChat.SelectedText.Trim();
                if (quotedText.Length > 100) quotedText = quotedText.Substring(0, 100) + "...";
                txtInput.Text = $"[Trả lời: “{quotedText}”]\n››› ";
                txtInput.SelectionStart = txtInput.Text.Length;
                txtInput.Focus();
            }
        }

        private void ItemFwd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(rtxtChat.SelectedText))
            {
                MessageBox.Show("Vui lòng chọn đoạn tin nhắn muốn chuyển tiếp trước!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string oldMsg = rtxtChat.SelectedText.Trim();
            if (oldMsg.Length > 200) oldMsg = oldMsg.Substring(0, 200) + "...";

            // Lấy danh sách online từ MainChatForms
            var onlineUsers = new System.Collections.Generic.List<string>();
            if (OnlineUsersList != null)
            {
                foreach (var item in OnlineUsersList.Items)
                {
                    string name = item.ToString().Replace(" (new)", "");
                    if (name != "Tất cả" && name != myUsername && name != targetUsername)
                        onlineUsers.Add(name);
                }
            }

            // Nếu không có ai khác: gợi ý chuyển tiếp lại cho chính người trong cuộc chat
            if (onlineUsers.Count == 0)
            {
                MessageBox.Show("Không có người dùng nào khác online để chuyển tiếp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Popup chọn người nhận
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

            Button btnSend = new Button
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
            btnSend.FlatAppearance.BorderSize = 0;

            Button btnCancel2 = new Button
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
            btnCancel2.FlatAppearance.BorderSize = 0;
            btnCancel2.Click += (s, ev) => fwdForm.Close();

            btnSend.Click += (s, ev) =>
            {
                if (cbUsers.SelectedItem != null && writer != null)
                {
                    string target = cbUsers.SelectedItem.ToString();
                    writer.WriteLine($"FWD:{target}:{oldMsg.Replace("\r", "").Replace("\n", "<br>")}");

                    rtxtChat.SelectionFont = new Font(rtxtChat.Font, FontStyle.Italic);
                    rtxtChat.SelectionColor = Color.FromArgb(120, 120, 120);
                    rtxtChat.AppendText($"  📤 Bạn đã chuyển tiếp đến {target}{Environment.NewLine}");
                    rtxtChat.SelectionFont = rtxtChat.Font;
                    rtxtChat.SelectionColor = rtxtChat.ForeColor;
                    fwdForm.Close();
                }
            };

            fwdForm.Controls.AddRange(new Control[] { lblPreview, lblChoose, cbUsers, btnCancel2, btnSend });
            fwdForm.ShowDialog(this);
        }

        private void txtInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (e.Shift)
                {
                    txtInput.AppendText(Environment.NewLine);
                }
                else
                {
                    e.SuppressKeyPress = true; // Ngăn chặn tiếng 'ding' và xuống dòng
                    btnSendMsg.PerformClick();
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

        private void BtnSendMsg_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtInput.Text) && writer != null)
            {
                try
                {
                    string msgText = txtInput.Text.Trim();
                    msgText = FilterProfanity(msgText);
                    msgText = msgText.Replace("\r", "").Replace("\n", "<br>");

                    if (!string.IsNullOrEmpty(currentColorHex))
                    {
                        msgText = currentColorHex + "|" + msgText;
                    }

                    // Gửi lệnh CHAT RIÊNG cho Server
                    // Cú pháp: PRIVATE:NgườiNhận:NộiDung
                    writer.WriteLine($"PRIVATE:{targetUsername}:{msgText}");

                    // Tự in tin nhắn của mình lên khung chat
                    AppendMessage("Tôi", msgText.Replace("<br>", "\n"));
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
                        string base64File = Convert.ToBase64String(fileBytes, Base64FormattingOptions.None);
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
                Color msgColor = rtxtChat.ForeColor;
                string content = message;

                // Bóc màu nếu có prefix màu #RRGGBB|
                if (content.StartsWith("#") && content.Contains("|"))
                {
                    int pipeIdx = content.IndexOf("|");
                    string hex = content.Substring(0, pipeIdx);
                    if (hex.Length == 7)
                    {
                        try { msgColor = ColorTranslator.FromHtml(hex); } catch { }
                        content = content.Substring(pipeIdx + 1);
                    }
                }

                // --- In tên + giờ ---
                rtxtChat.SelectionFont = new Font(rtxtChat.Font, FontStyle.Bold);
                rtxtChat.SelectionColor = rtxtChat.ForeColor;
                rtxtChat.AppendText($"[{DateTime.Now:HH:mm}] {sender}: ");
                rtxtChat.SelectionFont = rtxtChat.Font;

                // --- Kiểm tra có block trích dẫn [Trả lời: "..."] không ---
                string replyPrefix = "[Trả lời: \u201c";
                string replySuffix = "\u201d]";
                string mainContent = content;

                if (content.StartsWith(replyPrefix))
                {
                    int endIdx = content.IndexOf(replySuffix);
                    if (endIdx != -1)
                    {
                        string quoted = content.Substring(replyPrefix.Length, endIdx - replyPrefix.Length);
                        int afterReply = content.IndexOf("\u203a\u203a\u203a ", endIdx);
                        mainContent = afterReply != -1 ? content.Substring(afterReply + 4) : "";

                        // Vẽ block trích dẫn màu xám nghiêng
                        rtxtChat.AppendText(Environment.NewLine);
                        rtxtChat.SelectionFont = new Font(rtxtChat.Font, FontStyle.Italic);
                        rtxtChat.SelectionColor = Color.FromArgb(110, 110, 130);
                        rtxtChat.AppendText($"  \u258d \u00ab{quoted}\u00bb{Environment.NewLine}");
                        rtxtChat.SelectionFont = rtxtChat.Font;
                        rtxtChat.SelectionColor = rtxtChat.ForeColor;
                    }
                }

                // --- In nội dung chính ---
                if (!string.IsNullOrWhiteSpace(mainContent))
                {
                    rtxtChat.SelectionColor = msgColor;
                    rtxtChat.AppendText($"{mainContent}{Environment.NewLine}");
                    rtxtChat.SelectionColor = rtxtChat.ForeColor;
                }
                else
                {
                    rtxtChat.AppendText(Environment.NewLine);
                }

                rtxtChat.SelectionStart = rtxtChat.Text.Length;
                rtxtChat.ScrollToCaret();
            }
        }

        public void UpdateAvatar()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(UpdateAvatar));
                return;
            }
            this.picAvatar.Image = MainChatForms.GetAvatar(targetUsername);
        }

        public void InsertImage(string sender, Image img, string fileName)
        {
            if (rtxtChat.InvokeRequired)
            {
                rtxtChat.Invoke(new Action(() => InsertImage(sender, img, fileName)));
            }
            else
            {
                rtxtChat.SelectionFont = new Font(rtxtChat.Font, FontStyle.Bold);
                rtxtChat.AppendText($"[{DateTime.Now:HH:mm}] [{sender} đã gửi ảnh: {fileName}]{Environment.NewLine}");
                rtxtChat.SelectionFont = rtxtChat.Font;

                try
                {
                    IDataObject oldData = Clipboard.GetDataObject();
                    Clipboard.SetImage(img);
                    rtxtChat.ReadOnly = false;
                    rtxtChat.Paste();
                    rtxtChat.ReadOnly = true;
                    rtxtChat.AppendText(Environment.NewLine);
                    if (oldData != null) Clipboard.SetDataObject(oldData);
                }
                catch
                {
                    rtxtChat.AppendText("[Không thể hiển thị ảnh trực tiếp]" + Environment.NewLine);
                }

                rtxtChat.SelectionStart = rtxtChat.Text.Length;
                rtxtChat.ScrollToCaret();
            }
        }

        private void picAvatar_Click(object sender, EventArgs e)
        {

        }
    }
}
