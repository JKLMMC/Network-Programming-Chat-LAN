using System;
using System.Drawing;
using System.Windows.Forms;

namespace WinFormsApp2
{
   
    public class EmojiPreviewPanel : Panel
    {
        
        public event EventHandler OnCancelled;   
        public event EventHandler OnConfirmed;   
        
        public Image   PendingImage    { get; private set; }
        public string  PendingFileName { get; private set; }
        public bool    HasPending      => PendingImage != null;

        
        private PictureBox picPreview;
        private Label      lblHint;
        private Button     btnCancel;

        public EmojiPreviewPanel()
        {
            
            this.Height          = 80;
            this.Dock            = DockStyle.Bottom; 
            this.BackColor       = Color.FromArgb(240, 245, 255);
            this.BorderStyle     = BorderStyle.FixedSingle;
            this.Visible         = false; 
            
            picPreview = new PictureBox
            {
                Size     = new Size(64, 64),
                Location = new Point(8, 8),
                SizeMode = PictureBoxSizeMode.Zoom,
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.White
            };

            
            lblHint = new Label
            {
                AutoSize  = false,
                Size      = new Size(260, 40),
                Location  = new Point(82, 10),
                Text      = "📎 Emoji ảnh đã chọn\nBấm \"Gửi\" để gửi đi!",
                Font      = new Font("Segoe UI", 9.5f),
                ForeColor = Color.FromArgb(50, 80, 160)
            };

            
            btnCancel = new Button
            {
                Text      = "✕ Hủy",
                Size      = new Size(70, 28),
                Location  = new Point(350, 25),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(220, 80, 80),
                ForeColor = Color.White,
                Font      = new Font("Segoe UI", 9f, FontStyle.Bold),
                Cursor    = Cursors.Hand
            };
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.Click += (s, e) => ClearPending();

            this.Controls.Add(picPreview);
            this.Controls.Add(lblHint);
            this.Controls.Add(btnCancel);
        }

        
        public void SetPending(Image img, string fileName)
        {
           
            ClearImageOnly();

            PendingImage    = img;
            PendingFileName = fileName;
            picPreview.Image = img;

            lblHint.Text = $"📎 {fileName}\nBấm \"Gửi\" để gửi emoji ảnh!";
            this.Visible = true;
        }

        
        public void ClearPending()
        {
            ClearImageOnly();
            this.Visible = false;
            OnCancelled?.Invoke(this, EventArgs.Empty);
        }

        private void ClearImageOnly()
        {
            picPreview.Image = null;
            PendingImage?.Dispose();
            PendingImage    = null;
            PendingFileName = "";
        }
    }
}