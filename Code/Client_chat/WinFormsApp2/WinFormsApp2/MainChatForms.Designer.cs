namespace WinFormsApp2
{
    partial class MainChatForms
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
            splitContainer1 = new SplitContainer();
            label1 = new Label();
            lstOnlineUsers = new ListBox();
            panel1 = new Panel();
            btnSend = new Button();
            txtMessage = new TextBox();
            rtxtChatLog = new RichTextBox();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(label1);
            splitContainer1.Panel1.Controls.Add(lstOnlineUsers);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(panel1);
            splitContainer1.Panel2.Controls.Add(rtxtChatLog);
            splitContainer1.Size = new Size(932, 553);
            splitContainer1.SplitterDistance = 310;
            splitContainer1.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(3, 26);
            label1.Name = "label1";
            label1.Size = new Size(172, 25);
            label1.TabIndex = 1;
            label1.Text = "Người dùng online";
            label1.Click += label1_Click;
            // 
            // lstOnlineUsers
            // 
            lstOnlineUsers.Dock = DockStyle.Fill;
            lstOnlineUsers.FormattingEnabled = true;
            lstOnlineUsers.Location = new Point(0, 0);
            lstOnlineUsers.Name = "lstOnlineUsers";
            lstOnlineUsers.Size = new Size(310, 553);
            lstOnlineUsers.TabIndex = 0;
            // 
            // panel1
            // 
            panel1.Controls.Add(btnSend);
            panel1.Controls.Add(txtMessage);
            panel1.Dock = DockStyle.Bottom;
            panel1.Location = new Point(0, 483);
            panel1.Name = "panel1";
            panel1.Size = new Size(618, 70);
            panel1.TabIndex = 1;
            // 
            // btnSend
            // 
            btnSend.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnSend.BackColor = SystemColors.Highlight;
            btnSend.FlatStyle = FlatStyle.Flat;
            btnSend.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnSend.ForeColor = SystemColors.ButtonHighlight;
            btnSend.Location = new Point(524, 33);
            btnSend.Name = "btnSend";
            btnSend.Size = new Size(94, 34);
            btnSend.TabIndex = 1;
            btnSend.Text = "Gửi";
            btnSend.UseVisualStyleBackColor = false;
            btnSend.Click += btnSend_Click; // Liên kết sự kiện bấm nút Gửi thành công
            // 
            // txtMessage
            // 
            txtMessage.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtMessage.Location = new Point(0, 0);
            txtMessage.Multiline = true;
            txtMessage.Name = "txtMessage";
            txtMessage.Size = new Size(525, 70);
            txtMessage.TabIndex = 0;
            // 
            // rtxtChatLog
            // 
            rtxtChatLog.Dock = DockStyle.Fill;
            rtxtChatLog.Location = new Point(0, 0);
            rtxtChatLog.Name = "rtxtChatLog";
            rtxtChatLog.ReadOnly = true;
            rtxtChatLog.Size = new Size(618, 553);
            rtxtChatLog.TabIndex = 0;
            rtxtChatLog.Text = "";
            // 
            // MainChatForms
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(932, 553);
            Controls.Add(splitContainer1);
            Name = "MainChatForms";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "MainChatForms";
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel1.PerformLayout();
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private SplitContainer splitContainer1;
        private Label label1;
        private ListBox lstOnlineUsers;
        private RichTextBox rtxtChatLog;
        private Panel panel1;
        private TextBox txtMessage;
        private Button btnSend;
    }
}