namespace WinFormsApp2
{
    partial class PrivateChatForm
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
            rtxtChat = new RichTextBox();
            ctxMenu = new ContextMenuStrip(components);
            itemReply = new ToolStripMenuItem();
            itemFwd = new ToolStripMenuItem();
            txtInput = new TextBox();
            btnSendFile = new Button();
            btnSendMsg = new Button();
            picAvatar = new PictureBox();
            lblName = new Label();
            pnlEmoji = new Panel();
            btnEmoji1 = new Button();
            btnEmoji2 = new Button();
            btnEmoji3 = new Button();
            btnEmoji4 = new Button();
            btnEmoji5 = new Button();
            btnColor = new Button();
            lblTyping = new Label();
            sharedColorDialog = new ColorDialog();
            ctxMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picAvatar).BeginInit();
            pnlEmoji.SuspendLayout();
            SuspendLayout();
            // 
            // rtxtChat
            // 
            rtxtChat.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            rtxtChat.BackColor = Color.White;
            rtxtChat.ContextMenuStrip = ctxMenu;
            rtxtChat.Font = new Font("Segoe UI", 10F);
            rtxtChat.Location = new Point(0, 60);
            rtxtChat.Name = "rtxtChat";
            rtxtChat.ReadOnly = true;
            rtxtChat.Size = new Size(434, 215);
            rtxtChat.TabIndex = 0;
            rtxtChat.Text = "";
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
            // txtInput
            // 
            txtInput.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtInput.Font = new Font("Segoe UI", 10F);
            txtInput.Location = new Point(12, 315);
            txtInput.MaxLength = 500;
            txtInput.Multiline = true;
            txtInput.Name = "txtInput";
            txtInput.Size = new Size(230, 50);
            txtInput.TabIndex = 1;
            txtInput.KeyDown += txtInput_KeyDown;
            // 
            // btnSendFile
            // 
            btnSendFile.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnSendFile.BackColor = Color.LightGray;
            btnSendFile.FlatStyle = FlatStyle.Flat;
            btnSendFile.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnSendFile.Location = new Point(248, 315);
            btnSendFile.Name = "btnSendFile";
            btnSendFile.Size = new Size(80, 50);
            btnSendFile.TabIndex = 2;
            btnSendFile.Text = "Gửi file";
            btnSendFile.UseVisualStyleBackColor = false;
            btnSendFile.Click += BtnSendFile_Click;
            // 
            // btnSendMsg
            // 
            btnSendMsg.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnSendMsg.BackColor = SystemColors.Highlight;
            btnSendMsg.FlatStyle = FlatStyle.Flat;
            btnSendMsg.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnSendMsg.ForeColor = Color.White;
            btnSendMsg.Location = new Point(334, 315);
            btnSendMsg.Name = "btnSendMsg";
            btnSendMsg.Size = new Size(88, 50);
            btnSendMsg.TabIndex = 3;
            btnSendMsg.Text = "Gửi";
            btnSendMsg.UseVisualStyleBackColor = false;
            btnSendMsg.Click += BtnSendMsg_Click;
            // 
            // picAvatar
            // 
            picAvatar.Location = new Point(12, 5);
            picAvatar.Name = "picAvatar";
            picAvatar.Size = new Size(50, 50);
            picAvatar.TabIndex = 7;
            picAvatar.TabStop = false;
            picAvatar.Click += picAvatar_Click;
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblName.Location = new Point(68, 15);
            lblName.Name = "lblName";
            lblName.Size = new Size(54, 32);
            lblName.TabIndex = 8;
            lblName.Text = "Tên";
            // 
            // pnlEmoji
            // 
            pnlEmoji.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            pnlEmoji.Controls.Add(btnEmoji1);
            pnlEmoji.Controls.Add(btnEmoji2);
            pnlEmoji.Controls.Add(btnEmoji3);
            pnlEmoji.Controls.Add(btnEmoji4);
            pnlEmoji.Controls.Add(btnEmoji5);
            pnlEmoji.Location = new Point(12, 280);
            pnlEmoji.Name = "pnlEmoji";
            pnlEmoji.Size = new Size(180, 30);
            pnlEmoji.TabIndex = 4;
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
            // btnColor
            // 
            btnColor.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnColor.BackColor = Color.LightGray;
            btnColor.FlatStyle = FlatStyle.Flat;
            btnColor.Location = new Point(198, 280);
            btnColor.Name = "btnColor";
            btnColor.Size = new Size(60, 30);
            btnColor.TabIndex = 5;
            btnColor.Text = "Màu";
            btnColor.UseVisualStyleBackColor = false;
            btnColor.Click += btnColor_Click;
            // 
            // lblTyping
            // 
            lblTyping.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblTyping.AutoSize = true;
            lblTyping.ForeColor = Color.Gray;
            lblTyping.Location = new Point(265, 285);
            lblTyping.Name = "lblTyping";
            lblTyping.Size = new Size(114, 20);
            lblTyping.TabIndex = 6;
            lblTyping.Text = "Ai đó đang gõ...";
            lblTyping.Visible = false;
            // 
            // PrivateChatForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(434, 381);
            Controls.Add(lblName);
            Controls.Add(picAvatar);
            Controls.Add(lblTyping);
            Controls.Add(btnColor);
            Controls.Add(pnlEmoji);
            Controls.Add(btnSendMsg);
            Controls.Add(btnSendFile);
            Controls.Add(txtInput);
            Controls.Add(rtxtChat);
            MaximizeBox = false;
            Name = "PrivateChatForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Chat riêng";
            ctxMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)picAvatar).EndInit();
            pnlEmoji.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtxtChat;
        private System.Windows.Forms.TextBox txtInput;
        private System.Windows.Forms.Button btnSendFile;
        private System.Windows.Forms.Button btnSendMsg;
        private System.Windows.Forms.PictureBox picAvatar;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Panel pnlEmoji;
        private System.Windows.Forms.Button btnEmoji1;
        private System.Windows.Forms.Button btnEmoji2;
        private System.Windows.Forms.Button btnEmoji3;
        private System.Windows.Forms.Button btnEmoji4;
        private System.Windows.Forms.Button btnEmoji5;
        private System.Windows.Forms.Button btnColor;
        private System.Windows.Forms.Label lblTyping;
        private System.Windows.Forms.ContextMenuStrip ctxMenu;
        private System.Windows.Forms.ToolStripMenuItem itemReply;
        private System.Windows.Forms.ToolStripMenuItem itemFwd;
        private System.Windows.Forms.ColorDialog sharedColorDialog;
    }
}
