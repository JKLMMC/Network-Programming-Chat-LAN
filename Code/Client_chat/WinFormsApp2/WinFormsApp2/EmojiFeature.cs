using System;
using System.Drawing;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace WinFormsApp2
{
    public class EmojiFeature
    {
        private readonly MainChatForms mainForm;
        private EmojiPreviewPanel emojiPreview;
        private Image currentEmoji = null;

        public EmojiFeature(MainChatForms form)
        {
            mainForm = form;

            var previews = form.Controls.Find("emojiPreview", true);
            if (previews.Length > 0)
            {
                emojiPreview = previews[0] as EmojiPreviewPanel;
            }
            else
            {
                emojiPreview = new EmojiPreviewPanel();
                emojiPreview.Name = "emojiPreview";
                form.Controls.Add(emojiPreview);
                emojiPreview.BringToFront();
            }

            var btnEmojiImages = form.Controls.Find("btnEmojiImage", true);
            Button btnEmojiImage = null;
            if (btnEmojiImages.Length > 0)
            {
                btnEmojiImage = btnEmojiImages[0] as Button;
            }
            else
            {
                btnEmojiImage = new Button();
                btnEmojiImage.Name = "btnEmojiImage";
                btnEmojiImage.Text = "Chọn Sticker";
                btnEmojiImage.Size = new Size(100, 30);
                btnEmojiImage.Location = new Point(10, form.Height - 120);
                form.Controls.Add(btnEmojiImage);
                btnEmojiImage.BringToFront();
            }
            
            if (btnEmojiImage != null)
                btnEmojiImage.Click += BtnEmojiImage_Click;

            Button btnSend = null;
            var checksSend = form.Controls.Find("btnSend", true);
            if (checksSend.Length > 0) btnSend = checksSend[0] as Button;
            else
            {
                var checksSendMsg = form.Controls.Find("btnSendMsg", true);
                if (checksSendMsg.Length > 0) btnSend = checksSendMsg[0] as Button;
            }

            if (btnSend != null)
            {
                btnSend.Click += (s, e) =>
                {
                    if (emojiPreview != null && emojiPreview.HasPending)
                    {
                        OnEmojiConfirmed(this, EventArgs.Empty);
                    }
                };
            }

            if (emojiPreview != null)
            {
                emojiPreview.OnCancelled += OnEmojiCancelled;
                emojiPreview.OnConfirmed += OnEmojiConfirmed;
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

                if (mainForm.Controls.Find("lstOnlineUsers", true)[0] is ListBox lst)
                {
                    if (lst.SelectedItem != null)
                        target = lst.SelectedItem.ToString().Replace(" (new)", "");
                }

                string command = target == "Tất cả" 
                    ? $"EMOJI_BROADCAST|{base64}|{fileName}"
                    : $"EMOJI_PRIVATE|{target}|{base64}|{fileName}";

                var writerField = typeof(MainChatForms).GetField("writer", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                if (writerField?.GetValue(mainForm) is System.IO.StreamWriter writer)
                {
                    writer.WriteLine(command);

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