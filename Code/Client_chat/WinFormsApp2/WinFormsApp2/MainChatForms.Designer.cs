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
            this.components = new System.ComponentModel.Container();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.picAvatar = new System.Windows.Forms.PictureBox();
            this.lstOnlineUsers = new System.Windows.Forms.ListBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblTyping = new System.Windows.Forms.Label();
            this.btnColor = new System.Windows.Forms.Button();
            this.pnlEmoji = new System.Windows.Forms.Panel();
            this.btnEmoji1 = new System.Windows.Forms.Button();
            this.btnEmoji2 = new System.Windows.Forms.Button();
            this.btnEmoji3 = new System.Windows.Forms.Button();
            this.btnEmoji4 = new System.Windows.Forms.Button();
            this.btnEmoji5 = new System.Windows.Forms.Button();
            this.btnSendFile = new System.Windows.Forms.Button();
            this.btnSend = new System.Windows.Forms.Button();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.rtxtChatLog = new System.Windows.Forms.RichTextBox();
            this.ctxMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.itemReply = new System.Windows.Forms.ToolStripMenuItem();
            this.itemFwd = new System.Windows.Forms.ToolStripMenuItem();
            this.sharedColorDialog = new System.Windows.Forms.ColorDialog();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picAvatar)).BeginInit();
            this.panel1.SuspendLayout();
            this.pnlEmoji.SuspendLayout();
            this.ctxMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.picAvatar);
            this.splitContainer1.Panel1.Controls.Add(this.lstOnlineUsers);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.panel1);
            this.splitContainer1.Panel2.Controls.Add(this.rtxtChatLog);
            this.splitContainer1.Size = new System.Drawing.Size(932, 553);
            this.splitContainer1.SplitterDistance = 310;
            this.splitContainer1.TabIndex = 0;
            // 
            // lstOnlineUsers
            // 
            this.lstOnlineUsers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstOnlineUsers.FormattingEnabled = true;
            this.lstOnlineUsers.Location = new System.Drawing.Point(0, 70);
            this.lstOnlineUsers.Name = "lstOnlineUsers";
            this.lstOnlineUsers.Size = new System.Drawing.Size(310, 483);
            this.lstOnlineUsers.TabIndex = 0;
            this.lstOnlineUsers.SelectedIndexChanged += new System.EventHandler(this.lstOnlineUsers_SelectedIndexChanged);
            this.lstOnlineUsers.DoubleClick += new System.EventHandler(this.lstOnlineUsers_DoubleClick);
            // 
            // picAvatar
            // 
            this.picAvatar.Location = new System.Drawing.Point(10, 10);
            this.picAvatar.Name = "picAvatar";
            this.picAvatar.Size = new System.Drawing.Size(50, 50);
            this.picAvatar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picAvatar.TabIndex = 1;
            this.picAvatar.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblTyping);
            this.panel1.Controls.Add(this.btnColor);
            this.panel1.Controls.Add(this.pnlEmoji);
            this.panel1.Controls.Add(this.btnSendFile);
            this.panel1.Controls.Add(this.btnSend);
            this.panel1.Controls.Add(this.txtMessage);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 443);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(618, 110);
            this.panel1.TabIndex = 1;
            // 
            // btnSendFile
            // 
            this.btnSendFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSendFile.BackColor = System.Drawing.Color.ForestGreen;
            this.btnSendFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSendFile.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSendFile.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnSendFile.Location = new System.Drawing.Point(420, 43);
            this.btnSendFile.Name = "btnSendFile";
            this.btnSendFile.Size = new System.Drawing.Size(98, 64);
            this.btnSendFile.TabIndex = 2;
            this.btnSendFile.Text = "Gửi File";
            this.btnSendFile.UseVisualStyleBackColor = false;
            this.btnSendFile.Click += new System.EventHandler(this.btnSendFile_Click);
            // 
            // btnSend
            // 
            this.btnSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSend.BackColor = System.Drawing.SystemColors.Highlight;
            this.btnSend.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSend.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSend.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnSend.Location = new System.Drawing.Point(524, 43);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(94, 64);
            this.btnSend.TabIndex = 1;
            this.btnSend.Text = "Gửi";
            this.btnSend.UseVisualStyleBackColor = false;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // txtMessage
            // 
            this.txtMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMessage.Location = new System.Drawing.Point(10, 40);
            this.txtMessage.Multiline = true;
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.Size = new System.Drawing.Size(404, 64);
            this.txtMessage.TabIndex = 0;
            this.txtMessage.MaxLength = 500;
            // 
            // pnlEmoji
            // 
            this.pnlEmoji.Controls.Add(this.btnEmoji1);
            this.pnlEmoji.Controls.Add(this.btnEmoji2);
            this.pnlEmoji.Controls.Add(this.btnEmoji3);
            this.pnlEmoji.Controls.Add(this.btnEmoji4);
            this.pnlEmoji.Controls.Add(this.btnEmoji5);
            this.pnlEmoji.Location = new System.Drawing.Point(10, 5);
            this.pnlEmoji.Name = "pnlEmoji";
            this.pnlEmoji.Size = new System.Drawing.Size(250, 30);
            this.pnlEmoji.TabIndex = 3;
            // 
            // btnEmoji1
            // 
            this.btnEmoji1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEmoji1.Location = new System.Drawing.Point(0, 0);
            this.btnEmoji1.Name = "btnEmoji1";
            this.btnEmoji1.Size = new System.Drawing.Size(30, 30);
            this.btnEmoji1.TabIndex = 0;
            this.btnEmoji1.Text = "👍";
            this.btnEmoji1.UseVisualStyleBackColor = true;
            this.btnEmoji1.Click += new System.EventHandler(this.btnEmoji_Click);
            // 
            // btnEmoji2
            // 
            this.btnEmoji2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEmoji2.Location = new System.Drawing.Point(35, 0);
            this.btnEmoji2.Name = "btnEmoji2";
            this.btnEmoji2.Size = new System.Drawing.Size(30, 30);
            this.btnEmoji2.TabIndex = 1;
            this.btnEmoji2.Text = "😂";
            this.btnEmoji2.UseVisualStyleBackColor = true;
            this.btnEmoji2.Click += new System.EventHandler(this.btnEmoji_Click);
            // 
            // btnEmoji3
            // 
            this.btnEmoji3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEmoji3.Location = new System.Drawing.Point(70, 0);
            this.btnEmoji3.Name = "btnEmoji3";
            this.btnEmoji3.Size = new System.Drawing.Size(30, 30);
            this.btnEmoji3.TabIndex = 2;
            this.btnEmoji3.Text = "❤️";
            this.btnEmoji3.UseVisualStyleBackColor = true;
            this.btnEmoji3.Click += new System.EventHandler(this.btnEmoji_Click);
            // 
            // btnEmoji4
            // 
            this.btnEmoji4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEmoji4.Location = new System.Drawing.Point(105, 0);
            this.btnEmoji4.Name = "btnEmoji4";
            this.btnEmoji4.Size = new System.Drawing.Size(30, 30);
            this.btnEmoji4.TabIndex = 3;
            this.btnEmoji4.Text = "😡";
            this.btnEmoji4.UseVisualStyleBackColor = true;
            this.btnEmoji4.Click += new System.EventHandler(this.btnEmoji_Click);
            // 
            // btnEmoji5
            // 
            this.btnEmoji5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEmoji5.Location = new System.Drawing.Point(140, 0);
            this.btnEmoji5.Name = "btnEmoji5";
            this.btnEmoji5.Size = new System.Drawing.Size(30, 30);
            this.btnEmoji5.TabIndex = 4;
            this.btnEmoji5.Text = "😭";
            this.btnEmoji5.UseVisualStyleBackColor = true;
            this.btnEmoji5.Click += new System.EventHandler(this.btnEmoji_Click);
            // 
            // btnColor
            // 
            this.btnColor.BackColor = System.Drawing.Color.LightGray;
            this.btnColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnColor.Location = new System.Drawing.Point(270, 5);
            this.btnColor.Name = "btnColor";
            this.btnColor.Size = new System.Drawing.Size(60, 30);
            this.btnColor.TabIndex = 4;
            this.btnColor.Text = "Màu";
            this.btnColor.UseVisualStyleBackColor = false;
            this.btnColor.Click += new System.EventHandler(this.btnColor_Click);
            // 
            // lblTyping
            // 
            this.lblTyping.AutoSize = true;
            this.lblTyping.ForeColor = System.Drawing.Color.Gray;
            this.lblTyping.Location = new System.Drawing.Point(340, 10);
            this.lblTyping.Name = "lblTyping";
            this.lblTyping.Size = new System.Drawing.Size(121, 20);
            this.lblTyping.TabIndex = 5;
            this.lblTyping.Text = "Ai đó đang gõ...";
            this.lblTyping.Visible = false;
            // 
            // rtxtChatLog
            // 
            this.rtxtChatLog.ContextMenuStrip = this.ctxMenu;
            this.rtxtChatLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtxtChatLog.Location = new System.Drawing.Point(0, 0);
            this.rtxtChatLog.Name = "rtxtChatLog";
            this.rtxtChatLog.ReadOnly = true;
            this.rtxtChatLog.Size = new System.Drawing.Size(618, 443);
            this.rtxtChatLog.TabIndex = 0;
            this.rtxtChatLog.Text = "";
            // 
            // ctxMenu
            // 
            this.ctxMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itemReply,
            this.itemFwd});
            this.ctxMenu.Name = "ctxMenu";
            this.ctxMenu.Size = new System.Drawing.Size(142, 52);
            // 
            // itemReply
            // 
            this.itemReply.Name = "itemReply";
            this.itemReply.Size = new System.Drawing.Size(141, 24);
            this.itemReply.Text = "Trả lời";
            this.itemReply.Click += new System.EventHandler(this.ItemReply_Click);
            // 
            // itemFwd
            // 
            this.itemFwd.Name = "itemFwd";
            this.itemFwd.Size = new System.Drawing.Size(141, 24);
            this.itemFwd.Text = "Chuyển tiếp";
            this.itemFwd.Click += new System.EventHandler(this.ItemFwd_Click);
            // 
            // MainChatForms
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(932, 553);
            this.Controls.Add(this.splitContainer1);
            this.Name = "MainChatForms";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MainChatForms";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picAvatar)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pnlEmoji.ResumeLayout(false);
            this.ctxMenu.ResumeLayout(false);
            this.ResumeLayout(false);

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