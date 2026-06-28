using System;
using System.Drawing;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace WinFormsApp2
{
    public class EmojiFeature
    {
        private readonly MainChatForms mainForm;
        private readonly EmojiPreviewPanel emojiPreview;
        private Image currentEmoji = null;

        public EmojiFeature(MainChatForms form)
        {
            mainForm = form;

            // 1. Tìm cái thanh Preview ảnh của mày
            emojiPreview = form.Controls.Find("emojiPreview", true)[0] as EmojiPreviewPanel;

            // 2. Tìm nút chọn ảnh của mày và gán sự kiện Click
            var btnEmojiImage = form.Controls.Find("btnEmojiImage", true)[0] as Button;
            if (btnEmojiImage != null)
                btnEmojiImage.Click += BtnEmojiImage_Click;

            // 🔴 ĐOẠN SỬA BẮT CẦU: Tự động tìm và "ké" sự kiện nút Gửi của nhóm trưởng
            Button btnSend = form.Controls.Find("btnSend", true)[0] as Button;
            if (btnSend == null)
            {
                // Phòng hờ nhóm trưởng đặt tên nút là btnSendMsg thì tìm thêm phát nữa
                var checks = form.Controls.Find("btnSendMsg", true);
                if (checks.Length > 0) btnSend = checks[0] as Button;
            }

            if (btnSend != null)
            {
                // Khi người dùng bấm nút Gửi trên Form chính
                btnSend.Click += (s, e) =>
                {
                    // Nếu kiểm tra thấy có ảnh đang chờ gửi trong thanh Preview
                    if (emojiPreview != null && emojiPreview.HasPending)
                    {
                        // Kích hoạt hàm gửi Emoji qua mạng ngay lập tức!
                        OnEmojiConfirmed(this, EventArgs.Empty);
                    }
                };
            }
            // ----------------------------------------------------------------

            if (emojiPreview != null)
            {
                emojiPreview.OnConfirmed += OnEmojiConfirmed;
                emojiPreview.OnCancelled += OnEmojiCancelled;
            }
        }

        private void BtnEmojiImage_Click(object sender, EventArgs e)
        {
            string fileName;
            Image img = EmojiImagePicker.PickAndResize(out fileName);

            if (img != null && emojiPreview != null)
            {
                currentEmoji = img;
                emojiPreview.SetPending(img, fileName);
            }
        }

        private async void OnEmojiConfirmed(object sender, EventArgs e)
        {
            if (currentEmoji == null || emojiPreview == null) return;

            await SendEmojiToServer(currentEmoji, emojiPreview.PendingFileName);
            emojiPreview.ClearPending();
            currentEmoji = null;
        }

        private void OnEmojiCancelled(object sender, EventArgs e)
        {
            currentEmoji = null;
        }

        private async Task SendEmojiToServer(Image img, string fileName)
        {
            try
            {
                string base64 = EmojiImagePicker.ImageToBase64(img);
                string target = "Tất cả";

                // Lấy target người nhận từ ListBox của nhóm trưởng
                if (mainForm.Controls.Find("lstOnlineUsers", true)[0] is ListBox lst)
                {
                    if (lst.SelectedItem != null)
                        target = lst.SelectedItem.ToString().Replace(" (new)", "");
                }

                // 🔥 ĐỒNG BỘ SANG DẤU GẠCH ĐỨNG '|' THEO ĐÚNG CHUẨN GIAO THỨC SERVER NHÓM
                string command = target == "Tất cả" 
                    ? $"EMOJI_BROADCAST|{base64}|{fileName}"
                    : $"EMOJI_PRIVATE|{target}|{base64}|{fileName}";

                // Hack lấy quyền sử dụng mạng (Writer) bằng kĩ thuật Reflection xịn sò của mày
                var writerField = typeof(MainChatForms).GetField("writer", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                if (writerField?.GetValue(mainForm) is System.IO.StreamWriter writer)
                {
                    writer.WriteLine(command);

                    // Hiển thị thông báo đã gửi lên khung chat log của chính mình
                    mainForm.Invoke(new Action(() =>
                    {
                        var rtxt = mainForm.Controls.Find("rtxtChatLog", true)[0] as RichTextBox;
                        if (rtxt != null)
                        {
                            rtxt.SelectionColor = Color.Green;
                            rtxt.AppendText($"[{DateTime.Now:HH:mm}] Bạn: [Gửi emoji ảnh {fileName}]{Environment.NewLine}");
                            rtxt.SelectionColor = rtxt.ForeColor;
                            rtxt.ScrollToCaret();
                        }
                    }));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi gửi emoji: " + ex.Message);
            }
        }
    }
}