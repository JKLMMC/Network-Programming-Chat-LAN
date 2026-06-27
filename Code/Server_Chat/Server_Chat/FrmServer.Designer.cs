namespace Server_Chat
{
    partial class FrmServer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            btnStart = new Button();
            btnStop = new Button();
            lstOnlineUsers = new ListBox();
            lblOnline = new Label();
            lblLog = new Label();
            rtbLog = new RichTextBox();
            btnKich = new Button();
            btnLock = new Button();
            btnUnlock = new Button();
            btnDelete = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(341, 9);
            label1.Name = "label1";
            label1.Size = new Size(198, 38);
            label1.TabIndex = 0;
            label1.Text = "SERVER CHAT";
            // 
            // btnStart
            // 
            btnStart.BackColor = SystemColors.Highlight;
            btnStart.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnStart.ForeColor = SystemColors.ControlLightLight;
            btnStart.Location = new Point(187, 89);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(94, 33);
            btnStart.TabIndex = 1;
            btnStart.Text = "Start";
            btnStart.UseVisualStyleBackColor = false;
            // 
            // btnStop
            // 
            btnStop.BackColor = SystemColors.Highlight;
            btnStop.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnStop.ForeColor = SystemColors.ControlLightLight;
            btnStop.Location = new Point(585, 89);
            btnStop.Name = "btnStop";
            btnStop.Size = new Size(94, 33);
            btnStop.TabIndex = 2;
            btnStop.Text = "Stop";
            btnStop.UseVisualStyleBackColor = false;
            // 
            // lstOnlineUsers
            // 
            lstOnlineUsers.FormattingEnabled = true;
            lstOnlineUsers.Location = new Point(12, 177);
            lstOnlineUsers.Name = "lstOnlineUsers";
            lstOnlineUsers.Size = new Size(269, 204);
            lstOnlineUsers.TabIndex = 3;
            // 
            // lblOnline
            // 
            lblOnline.AutoSize = true;
            lblOnline.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblOnline.Location = new Point(72, 198);
            lblOnline.Name = "lblOnline";
            lblOnline.Size = new Size(162, 23);
            lblOnline.TabIndex = 4;
            lblOnline.Text = "Người dùng online";
            // 
            // lblLog
            // 
            lblLog.AutoSize = true;
            lblLog.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblLog.Location = new Point(515, 198);
            lblLog.Name = "lblLog";
            lblLog.Size = new Size(130, 23);
            lblLog.TabIndex = 5;
            lblLog.Text = "Nhật ký Server";
            // 
            // rtbLog
            // 
            rtbLog.Location = new Point(280, 177);
            rtbLog.Name = "rtbLog";
            rtbLog.ReadOnly = true;
            rtbLog.Size = new Size(590, 204);
            rtbLog.TabIndex = 6;
            rtbLog.Text = "";
            // 
            // btnKich
            // 
            btnKich.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnKich.Location = new Point(105, 428);
            btnKich.Name = "btnKich";
            btnKich.Size = new Size(101, 29);
            btnKich.TabIndex = 7;
            btnKich.Text = "Kich Users";
            btnKich.UseVisualStyleBackColor = true;
            // 
            // btnLock
            // 
            btnLock.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnLock.Location = new Point(268, 428);
            btnLock.Name = "btnLock";
            btnLock.Size = new Size(141, 29);
            btnLock.TabIndex = 8;
            btnLock.Text = "Khóa tài khoản";
            btnLock.UseVisualStyleBackColor = true;
            // 
            // btnUnlock
            // 
            btnUnlock.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnUnlock.Location = new Point(463, 428);
            btnUnlock.Name = "btnUnlock";
            btnUnlock.Size = new Size(140, 29);
            btnUnlock.TabIndex = 9;
            btnUnlock.Text = "Mở tài khoản";
            btnUnlock.UseVisualStyleBackColor = true;
            // 
            // btnDelete
            // 
            btnDelete.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnDelete.Location = new Point(654, 428);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(134, 29);
            btnDelete.TabIndex = 10;
            btnDelete.Text = "Xóa tài khoản";
            btnDelete.UseVisualStyleBackColor = true;
            // 
            // FrmServer
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(882, 553);
            Controls.Add(btnDelete);
            Controls.Add(btnUnlock);
            Controls.Add(btnLock);
            Controls.Add(btnKich);
            Controls.Add(lblLog);
            Controls.Add(lblOnline);
            Controls.Add(btnStop);
            Controls.Add(btnStart);
            Controls.Add(label1);
            Controls.Add(lstOnlineUsers);
            Controls.Add(rtbLog);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "FrmServer";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "SERVER CHAT";
            Load += FrmServer_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Button btnStart;
        private Button btnStop;
        private ListBox lstOnlineUsers;
        private Label lblOnline;
        private Label lblLog;
        private RichTextBox rtbLog;
        private Button btnKich;
        private Button btnLock;
        private Button btnUnlock;
        private Button btnDelete;
    }
}