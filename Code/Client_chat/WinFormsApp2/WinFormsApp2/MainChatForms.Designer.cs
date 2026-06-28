namespace WinFormsApp2
{
    partial class MainChatForms
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
            components = new System.ComponentModel.Container();
            splitContainer1 = new SplitContainer();
            picAvatar = new PictureBox();
            lstOnlineUsers = new ListBox();
            panel1 = new Panel();
            lblTyping = new Label();
            btnColor = new Button();
            pnlEmoji = new Panel();
            btnEmoji1 = new Button();
            btnEmoji2 = new Button();
            btnEmoji3 = new Button();
            btnEmoji4 = new Button();
            btnEmoji5 = new Button();
            btnSendFile = new Button();
            btnSend = new Button();
            txtMessage = new TextBox();
            rtxtChatLog = new RichTextBox();
            ctxMenu = new ContextMenuStrip(components);
            itemReply = new ToolStripMenuItem();
            itemFwd = new ToolStripMenuItem();
            sharedColorDialog = new ColorDialog();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picAvatar).BeginInit();
            panel1.SuspendLayout();
            pnlEmoji.SuspendLayout();
            ctxMenu.SuspendLayout();
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
            splitContainer1.Panel1.Controls.Add(picAvatar);
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
            // picAvatar
            // 
            picAvatar.Location = new Point(10, 10);
            picAvatar.Name = "picAvatar";
            picAvatar.Size = new Size(50, 50);
            picAvatar.SizeMode = PictureBoxSizeMode.Zoom;
            picAvatar.TabIndex = 1;
            picAvatar.TabStop = false;
            // 
            // lstOnlineUsers
            // 
            lstOnlineUsers.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lstOnlineUsers.FormattingEnabled = true;
            lstOnlineUsers.Location = new Point(0, 70);
            lstOnlineUsers.Name = "lstOnlineUsers";
            lstOnlineUsers.Size = new Size(310, 464);
            lstOnlineUsers.TabIndex = 0;
            lstOnlineUsers.SelectedIndexChanged += lstOnlineUsers_SelectedIndexChanged;
            lstOnlineUsers.DoubleClick += lstOnlineUsers_DoubleClick;
            // 
            // panel1
            // 
            panel1.Controls.Add(lblTyping);
            panel1.Controls.Add(btnColor);
            panel1.Controls.Add(pnlEmoji);
            panel1.Controls.Add(btnSendFile);
            panel1.Controls.Add(btnSend);
            panel1.Controls.Add(txtMessage);
            panel1.Dock = DockStyle.Bottom;
            panel1.Location = new Point(0, 443);
            panel1.Name = "panel1";
            panel1.Size = new Size(618, 110);
            panel1.TabIndex = 1;
            // 
            // lblTyping
            // 
            lblTyping.AutoSize = true;
            lblTyping.ForeColor = Color.Gray;
            lblTyping.Location = new Point(340, 10);
            lblTyping.Name = "lblTyping";
            lblTyping.Size = new Size(114, 20);
            lblTyping.TabIndex = 5;
            lblTyping.Text = "Ai đó đang gõ...";
            lblTyping.Visible = false;
            // 
            // btnColor
            // 
            btnColor.BackColor = Color.LightGray;
            btnColor.FlatStyle = FlatStyle.Flat;
            btnColor.Location = new Point(270, 5);
            btnColor.Name = "btnColor";
            btnColor.Size = new Size(60, 30);
            btnColor.TabIndex = 4;
            btnColor.Text = "Màu";
            btnColor.UseVisualStyleBackColor = false;
            btnColor.Click += btnColor_Click;
            // 
            // pnlEmoji
            // 
            pnlEmoji.Controls.Add(btnEmoji1);
            pnlEmoji.Controls.Add(btnEmoji2);
            pnlEmoji.Controls.Add(btnEmoji3);
            pnlEmoji.Controls.Add(btnEmoji4);
            pnlEmoji.Controls.Add(btnEmoji5);
            pnlEmoji.Location = new Point(10, 5);
            pnlEmoji.Name = "pnlEmoji";
            pnlEmoji.Size = new Size(250, 30);
            pnlEmoji.TabIndex = 3;
            // 
            // btnEmoji1
            // 
            btnEmoji1.FlatStyle = FlatStyle.Flat;
            btnEmoji1.Location = new Point(0, 0);
            btnEmoji1.Name = "btnEmoji1";
            btnEmoji1.Size = new Size(30, 30);
            btnEmoji1.TabIndex = 0;
            btnEmoji1.Text = "👍";
            btnEmoji1.UseVisualStyleBackColor = true;
            btnEmoji1.Click += btnEmoji_Click;
            // 
            // btnEmoji2
            // 
            btnEmoji2.FlatStyle = FlatStyle.Flat;
            btnEmoji2.Location = new Point(35, 0);
            btnEmoji2.Name = "btnEmoji2";
            btnEmoji2.Size = new Size(30, 30);
            btnEmoji2.TabIndex = 1;
            btnEmoji2.Text = "😂";
            btnEmoji2.UseVisualStyleBackColor = true;
            btnEmoji2.Click += btnEmoji_Click;
            // 
            // btnEmoji3
            // 
            btnEmoji3.FlatStyle = FlatStyle.Flat;
            btnEmoji3.Location = new Point(70, 0);
            btnEmoji3.Name = "btnEmoji3";
            btnEmoji3.Size = new Size(30, 30);
            btnEmoji3.TabIndex = 2;
            btnEmoji3.Text = "❤️";
            btnEmoji3.UseVisualStyleBackColor = true;
            btnEmoji3.Click += btnEmoji_Click;
            // 
            // btnEmoji4
            // 
            btnEmoji4.FlatStyle = FlatStyle.Flat;
            btnEmoji4.Location = new Point(105, 0);
            btnEmoji4.Name = "btnEmoji4";
            btnEmoji4.Size = new Size(30, 30);
            btnEmoji4.TabIndex = 3;
            btnEmoji4.Text = "😡";
            btnEmoji4.UseVisualStyleBackColor = true;
            btnEmoji4.Click += btnEmoji_Click;
            // 
            // btnEmoji5
            // 
            btnEmoji5.FlatStyle = FlatStyle.Flat;
            btnEmoji5.Location = new Point(140, 0);
            btnEmoji5.Name = "btnEmoji5";
            btnEmoji5.Size = new Size(30, 30);
            btnEmoji5.TabIndex = 4;
            btnEmoji5.Text = "😭";
            btnEmoji5.UseVisualStyleBackColor = true;
            btnEmoji5.Click += btnEmoji_Click;
            // 
            // btnSendFile
            // 
            btnSendFile.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnSendFile.BackColor = Color.ForestGreen;
            btnSendFile.FlatStyle = FlatStyle.Flat;
            btnSendFile.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnSendFile.ForeColor = SystemColors.ButtonHighlight;
            btnSendFile.Location = new Point(420, 43);
            btnSendFile.Name = "btnSendFile";
            btnSendFile.Size = new Size(98, 64);
            btnSendFile.TabIndex = 2;
            btnSendFile.Text = "Gửi File";
            btnSendFile.UseVisualStyleBackColor = false;
            btnSendFile.Click += btnSendFile_Click;
            // 
            // btnSend
            // 
            btnSend.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnSend.BackColor = SystemColors.Highlight;
            btnSend.FlatStyle = FlatStyle.Flat;
            btnSend.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnSend.ForeColor = SystemColors.ButtonHighlight;
            btnSend.Location = new Point(524, 43);
            btnSend.Name = "btnSend";
            btnSend.Size = new Size(94, 64);
            btnSend.TabIndex = 1;
            btnSend.Text = "Gửi";
            btnSend.UseVisualStyleBackColor = false;
            btnSend.Click += btnSend_Click;
            // 
            // txtMessage
            // 
            txtMessage.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtMessage.Location = new Point(10, 41);
            txtMessage.MaxLength = 500;
            txtMessage.Multiline = true;
            txtMessage.Name = "txtMessage";
            txtMessage.Size = new Size(404, 64);
            txtMessage.TabIndex = 0;
            // 
            // rtxtChatLog
            // 
            rtxtChatLog.ContextMenuStrip = ctxMenu;
            rtxtChatLog.Location = new Point(10, 10);
            rtxtChatLog.Name = "rtxtChatLog";
            rtxtChatLog.ReadOnly = true;
            rtxtChatLog.Size = new Size(596, 427);
            rtxtChatLog.TabIndex = 0;
            rtxtChatLog.Text = "";
            rtxtChatLog.TextChanged += rtxtChatLog_TextChanged;
            // 
            // ctxMenu
            // 
            ctxMenu.ImageScalingSize = new Size(20, 20);
            ctxMenu.Items.AddRange(new ToolStripItem[] { itemReply, itemFwd });
            ctxMenu.Name = "ctxMenu";
            ctxMenu.Size = new Size(157, 52);
            // 
            // itemReply
            // 
            itemReply.Name = "itemReply";
            itemReply.Size = new Size(156, 24);
            itemReply.Text = "Trả lời";
            itemReply.Click += ItemReply_Click;
            // 
            // itemFwd
            // 
            itemFwd.Name = "itemFwd";
            itemFwd.Size = new Size(156, 24);
            itemFwd.Text = "Chuyển tiếp";
            itemFwd.Click += ItemFwd_Click;
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
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)picAvatar).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            pnlEmoji.ResumeLayout(false);
            ctxMenu.ResumeLayout(false);
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListBox lstOnlineUsers;
        private System.Windows.Forms.RichTextBox rtxtChatLog;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtMessage;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Button btnSendFile;
        private System.Windows.Forms.PictureBox picAvatar;
        private System.Windows.Forms.Panel pnlEmoji;
        private System.Windows.Forms.Button btnEmoji1;
        private System.Windows.Forms.Button btnEmoji2;
        private System.Windows.Forms.Button btnEmoji3;
        private System.Windows.Forms.Button btnEmoji4;
        private System.Windows.Forms.Button btnEmoji5;
        private System.Windows.Forms.Label lblTyping;
        private System.Windows.Forms.Button btnColor;
        private System.Windows.Forms.ContextMenuStrip ctxMenu;
        private System.Windows.Forms.ToolStripMenuItem itemReply;
        private System.Windows.Forms.ToolStripMenuItem itemFwd;
        private System.Windows.Forms.ColorDialog sharedColorDialog;
    }
}