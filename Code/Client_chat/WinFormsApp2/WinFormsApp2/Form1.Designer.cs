namespace WinFormsApp2
{
    partial class LoginForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            txtUsername = new TextBox();
            txtPassword = new TextBox();
            lblTitle = new Label();
            lblSubtitle = new Label();
            lblPassword = new Label();
            btnLogin = new Button();
            lnkRegister = new LinkLabel();
            lnkForgot = new LinkLabel();
            pnlBackground = new Panel();
            pnlBackground.SuspendLayout();
            SuspendLayout();
            // 
            // txtUsername
            // 
            txtUsername.BackColor = Color.White;
            txtUsername.BorderStyle = BorderStyle.FixedSingle;
            txtUsername.Font = new Font("Segoe UI", 12F);
            txtUsername.ForeColor = Color.FromArgb(64, 64, 64);
            txtUsername.Location = new Point(34, 140);
            txtUsername.Name = "txtUsername";
            txtUsername.PlaceholderText = "Nhập tên người dùng...";
            txtUsername.Size = new Size(332, 34);
            txtUsername.TabIndex = 0;
            // 
            // txtPassword
            // 
            txtPassword.BackColor = Color.White;
            txtPassword.BorderStyle = BorderStyle.FixedSingle;
            txtPassword.Font = new Font("Segoe UI", 12F);
            txtPassword.ForeColor = Color.FromArgb(64, 64, 64);
            txtPassword.Location = new Point(34, 220);
            txtPassword.Name = "txtPassword";
            txtPassword.PasswordChar = '●'; // Hiển thị dấu chấm bảo mật khi nhập mật khẩu
            txtPassword.PlaceholderText = "Nhập mật khẩu...";
            txtPassword.Size = new Size(332, 34);
            txtPassword.TabIndex = 1;
            // 
            // lblTitle
            // 
            lblTitle.Anchor = AnchorStyles.Top;
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI Semibold", 22F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(32, 32, 32);
            lblTitle.Location = new Point(106, 25);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(188, 50);
            lblTitle.TabIndex = 4;
            lblTitle.Text = "Chào mừng";
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblSubtitle
            // 
            lblSubtitle.AutoSize = true;
            lblSubtitle.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            lblSubtitle.ForeColor = Color.FromArgb(64, 64, 64);
            lblSubtitle.Location = new Point(34, 110);
            lblSubtitle.Name = "lblSubtitle";
            lblSubtitle.Size = new Size(124, 23);
            lblSubtitle.TabIndex = 3;
            lblSubtitle.Text = "Tên người dùng";
            // 
            // lblPassword
            // 
            lblPassword.AutoSize = true;
            lblPassword.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            lblPassword.ForeColor = Color.FromArgb(64, 64, 64);
            lblPassword.Location = new Point(34, 190);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(84, 23);
            lblPassword.TabIndex = 5;
            lblPassword.Text = "Mật khẩu";
            // 
            // btnLogin
            // 
            btnLogin.BackColor = Color.FromArgb(0, 120, 212);
            btnLogin.Cursor = Cursors.Hand;
            btnLogin.FlatAppearance.BorderSize = 0;
            btnLogin.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 100, 180);
            btnLogin.FlatAppearance.MouseOverBackColor = Color.FromArgb(30, 140, 230);
            btnLogin.FlatStyle = FlatStyle.Flat;
            btnLogin.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            btnLogin.ForeColor = Color.White;
            btnLogin.Location = new Point(34, 280);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(332, 45);
            btnLogin.TabIndex = 2;
            btnLogin.Text = "Đăng nhập";
            btnLogin.UseVisualStyleBackColor = false;
            btnLogin.Click += button1_Click;
            // 
            // lnkRegister
            // 
            lnkRegister.ActiveLinkColor = Color.FromArgb(0, 100, 180);
            lnkRegister.AutoSize = true;
            lnkRegister.Font = new Font("Segoe UI", 10F);
            lnkRegister.LinkColor = Color.FromArgb(0, 120, 212);
            lnkRegister.Location = new Point(34, 345);
            lnkRegister.Name = "lnkRegister";
            lnkRegister.Size = new Size(116, 23);
            lnkRegister.TabIndex = 6;
            lnkRegister.TabStop = true;
            lnkRegister.Text = "Tạo tài khoản";
            lnkRegister.VisitedLinkColor = Color.FromArgb(0, 120, 212);
            lnkRegister.LinkClicked += new LinkLabelLinkClickedEventHandler(((LoginForm)this).lnkRegister_LinkClicked);
            // 
            // lnkForgot
            // 
            lnkForgot.ActiveLinkColor = Color.FromArgb(0, 100, 180);
            lnkForgot.AutoSize = true;
            lnkForgot.Font = new Font("Segoe UI", 10F);
            lnkForgot.LinkColor = Color.FromArgb(0, 120, 212);
            lnkForgot.Location = new Point(236, 345);
            lnkForgot.Name = "lnkForgot";
            lnkForgot.Size = new Size(130, 23);
            lnkForgot.TabIndex = 7;
            lnkForgot.TabStop = true;
            lnkForgot.Text = "Quên mật khẩu?";
            lnkForgot.VisitedLinkColor = Color.FromArgb(0, 120, 212);
            // 
            // pnlBackground
            // 
            pnlBackground.Anchor = AnchorStyles.None;
            pnlBackground.BackColor = Color.FromArgb(250, 251, 252);
            pnlBackground.Controls.Add(lblTitle);
            pnlBackground.Controls.Add(txtUsername);
            pnlBackground.Controls.Add(lblSubtitle);
            pnlBackground.Controls.Add(lblPassword);
            pnlBackground.Controls.Add(txtPassword);
            pnlBackground.Controls.Add(lnkForgot);
            pnlBackground.Controls.Add(btnLogin);
            pnlBackground.Controls.Add(lnkRegister);
            pnlBackground.Location = new Point(200, 40);
            pnlBackground.Name = "pnlBackground";
            pnlBackground.Size = new Size(400, 400);
            pnlBackground.TabIndex = 8;
            // 
            // LoginForm
            // 
            AcceptButton = btnLogin;
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(240, 242, 245);
            ClientSize = new Size(800, 500);
            Controls.Add(pnlBackground);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "LoginForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Đăng nhập hệ thống";
            Load += Login_Load;
            pnlBackground.ResumeLayout(false);
            pnlBackground.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TextBox txtUsername;
        private TextBox txtPassword; // Thêm biến ô nhập mật khẩu
        private Label lblTitle;
        private Label lblSubtitle;
        private Label lblPassword;   // Thêm biến tiêu đề mật khẩu
        private Button btnLogin;
        private LinkLabel lnkRegister;
        private LinkLabel lnkForgot;
        private Panel pnlBackground;
    }
}