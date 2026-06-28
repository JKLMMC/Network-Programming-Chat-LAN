using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace WinFormsApp2
{
    
    public static class EmojiImagePicker
    {
                public const int EMOJI_SIZE = 64;
        public static Image PickAndResize(out string fileName)
        {
            fileName = "";

            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = "Chọn ảnh làm emoji";
                ofd.Filter = "Ảnh|*.jpg;*.jpeg;*.png;*.gif;*.bmp;*.webp";

                if (ofd.ShowDialog() != DialogResult.OK)
                    return null;

                fileName = System.IO.Path.GetFileName(ofd.FileName);

                try
                {
                    Image original = Image.FromFile(ofd.FileName);
                    Image resized = ResizeImage(original, EMOJI_SIZE, EMOJI_SIZE);
                    original.Dispose();
                    return resized;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Không thể mở ảnh: " + ex.Message, "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
            }
        }

        public static Image ResizeImage(Image source, int width, int height)
        {
            Bitmap result = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(result))
            {
                g.InterpolationMode  = InterpolationMode.HighQualityBicubic;
                g.SmoothingMode      = SmoothingMode.AntiAlias;
                g.PixelOffsetMode    = PixelOffsetMode.HighQuality;
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.DrawImage(source, 0, 0, width, height);
            }
            return result;
        }

        public static string ImageToBase64(Image img)
        {
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                return Convert.ToBase64String(ms.ToArray(), Base64FormattingOptions.None);
            }
        }
    }
}