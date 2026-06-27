namespace WinFormsApp3
{
    partial class FrmForgotPassword
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lblTitle = new Label();
            lblUserName = new Label();
            btnReset = new Button();
            txtUserName = new TextBox();
            lblNewPassword = new Label();
            txtNewPassword = new TextBox();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitle.Location = new Point(274, 9);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(253, 38);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "QUÊN MẬT KHẨU";
            // 
            // lblUserName
            // 
            lblUserName.AutoSize = true;
            lblUserName.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblUserName.Location = new Point(224, 89);
            lblUserName.Name = "lblUserName";
            lblUserName.Size = new Size(146, 25);
            lblUserName.TabIndex = 1;
            lblUserName.Text = "Tên người dùng";
            // 
            // btnReset
            // 
            btnReset.BackColor = SystemColors.Highlight;
            btnReset.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnReset.ForeColor = SystemColors.ControlLightLight;
            btnReset.Location = new Point(342, 283);
            btnReset.Name = "btnReset";
            btnReset.Size = new Size(116, 35);
            btnReset.TabIndex = 2;
            btnReset.Text = "Đặt lại";
            btnReset.UseVisualStyleBackColor = false;
            btnReset.Click += this.btnReset_Click;
            // 
            // txtUserName
            // 
            txtUserName.Location = new Point(229, 117);
            txtUserName.Name = "txtUserName";
            txtUserName.Size = new Size(200, 27);
            txtUserName.TabIndex = 3;
            // 
            // lblNewPassword
            // 
            lblNewPassword.AutoSize = true;
            lblNewPassword.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblNewPassword.Location = new Point(224, 175);
            lblNewPassword.Name = "lblNewPassword";
            lblNewPassword.Size = new Size(130, 25);
            lblNewPassword.TabIndex = 4;
            lblNewPassword.Text = "Mật khẩu mới";
            // 
            // txtNewPassword
            // 
            txtNewPassword.Location = new Point(229, 203);
            txtNewPassword.Name = "txtNewPassword";
            txtNewPassword.Size = new Size(200, 27);
            txtNewPassword.TabIndex = 5;
            txtNewPassword.UseSystemPasswordChar = true;
            // 
            // FrmForgotPassword
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(txtNewPassword);
            Controls.Add(lblNewPassword);
            Controls.Add(txtUserName);
            Controls.Add(btnReset);
            Controls.Add(lblUserName);
            Controls.Add(lblTitle);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "FrmForgotPassword";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Quên mật khẩu";
            Load += FrmForgotPassword_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitle;
        private Label lblUserName;
        private Button btnReset;
        private TextBox txtUserName;
        private Label lblNewPassword;
        private TextBox txtNewPassword;
    }
}
