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
            this.components = new System.ComponentModel.Container();
            this.rtxtChat = new System.Windows.Forms.RichTextBox();
            this.txtInput = new System.Windows.Forms.TextBox();
            this.btnSendFile = new System.Windows.Forms.Button();
            this.btnSendMsg = new System.Windows.Forms.Button();
            this.picAvatar = new System.Windows.Forms.PictureBox();
            this.lblName = new System.Windows.Forms.Label();
            this.pnlEmoji = new System.Windows.Forms.Panel();
            this.btnEmoji1 = new System.Windows.Forms.Button();
            this.btnEmoji2 = new System.Windows.Forms.Button();
            this.btnEmoji3 = new System.Windows.Forms.Button();
            this.btnEmoji4 = new System.Windows.Forms.Button();
            this.btnEmoji5 = new System.Windows.Forms.Button();
            this.btnColor = new System.Windows.Forms.Button();
            this.lblTyping = new System.Windows.Forms.Label();
            this.ctxMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.itemReply = new System.Windows.Forms.ToolStripMenuItem();
            this.itemFwd = new System.Windows.Forms.ToolStripMenuItem();
            this.sharedColorDialog = new System.Windows.Forms.ColorDialog();
            ((System.ComponentModel.ISupportInitialize)(this.picAvatar)).BeginInit();
            this.pnlEmoji.SuspendLayout();
            this.ctxMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // rtxtChat
            // 
            this.rtxtChat.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtxtChat.BackColor = System.Drawing.Color.White;
            this.rtxtChat.ContextMenuStrip = this.ctxMenu;
            this.rtxtChat.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.rtxtChat.Location = new System.Drawing.Point(0, 60);
            this.rtxtChat.Name = "rtxtChat";
            this.rtxtChat.ReadOnly = true;
            this.rtxtChat.Size = new System.Drawing.Size(434, 215);
            this.rtxtChat.TabIndex = 0;
            this.rtxtChat.Text = "";
            // 
            // txtInput
            // 
            this.txtInput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtInput.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtInput.Location = new System.Drawing.Point(12, 315);
            this.txtInput.Multiline = true;
            this.txtInput.Name = "txtInput";
            this.txtInput.Size = new System.Drawing.Size(230, 50);
            this.txtInput.TabIndex = 1;
            this.txtInput.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtInput_KeyDown);
            // 
            // btnSendFile
            // 
            this.btnSendFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSendFile.BackColor = System.Drawing.Color.LightGray;
            this.btnSendFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSendFile.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnSendFile.Location = new System.Drawing.Point(248, 315);
            this.btnSendFile.Name = "btnSendFile";
            this.btnSendFile.Size = new System.Drawing.Size(80, 50);
            this.btnSendFile.TabIndex = 2;
            this.btnSendFile.Text = "Gửi file";
            this.btnSendFile.UseVisualStyleBackColor = false;
            this.btnSendFile.Click += new System.EventHandler(this.BtnSendFile_Click);
            // 
            // btnSendMsg
            // 
            this.btnSendMsg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSendMsg.BackColor = System.Drawing.SystemColors.Highlight;
            this.btnSendMsg.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSendMsg.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnSendMsg.ForeColor = System.Drawing.Color.White;
            this.btnSendMsg.Location = new System.Drawing.Point(334, 315);
            this.btnSendMsg.Name = "btnSendMsg";
            this.btnSendMsg.Size = new System.Drawing.Size(88, 50);
            this.btnSendMsg.TabIndex = 3;
            this.btnSendMsg.Text = "Gửi";
            this.btnSendMsg.UseVisualStyleBackColor = false;
            this.btnSendMsg.Click += new System.EventHandler(this.BtnSendMsg_Click);
            // 
            // picAvatar
            // 
            this.picAvatar.Location = new System.Drawing.Point(12, 5);
            this.picAvatar.Name = "picAvatar";
            this.picAvatar.Size = new System.Drawing.Size(50, 50);
            this.picAvatar.TabIndex = 7;
            this.picAvatar.TabStop = false;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblName.Location = new System.Drawing.Point(68, 15);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(100, 32);
            this.lblName.TabIndex = 8;
            this.lblName.Text = "Tên";
            // 
            // pnlEmoji
            // 
            this.pnlEmoji.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pnlEmoji.Controls.Add(this.btnEmoji1);
            this.pnlEmoji.Controls.Add(this.btnEmoji2);
            this.pnlEmoji.Controls.Add(this.btnEmoji3);
            this.pnlEmoji.Controls.Add(this.btnEmoji4);
            this.pnlEmoji.Controls.Add(this.btnEmoji5);
            this.pnlEmoji.Location = new System.Drawing.Point(12, 280);
            this.pnlEmoji.Name = "pnlEmoji";
            this.pnlEmoji.Size = new System.Drawing.Size(180, 30);
            this.pnlEmoji.TabIndex = 4;
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
            this.btnColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnColor.BackColor = System.Drawing.Color.LightGray;
            this.btnColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnColor.Location = new System.Drawing.Point(198, 280);
            this.btnColor.Name = "btnColor";
            this.btnColor.Size = new System.Drawing.Size(60, 30);
            this.btnColor.TabIndex = 5;
            this.btnColor.Text = "Màu";
            this.btnColor.UseVisualStyleBackColor = false;
            this.btnColor.Click += new System.EventHandler(this.btnColor_Click);
            // 
            // lblTyping
            // 
            this.lblTyping.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTyping.AutoSize = true;
            this.lblTyping.ForeColor = System.Drawing.Color.Gray;
            this.lblTyping.Location = new System.Drawing.Point(265, 285);
            this.lblTyping.Name = "lblTyping";
            this.lblTyping.Size = new System.Drawing.Size(121, 20);
            this.lblTyping.TabIndex = 6;
            this.lblTyping.Text = "Ai đó đang gõ...";
            this.lblTyping.Visible = false;
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
            // PrivateChatForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(434, 381);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.picAvatar);
            this.Controls.Add(this.lblTyping);
            this.Controls.Add(this.btnColor);
            this.Controls.Add(this.pnlEmoji);
            this.Controls.Add(this.btnSendMsg);
            this.Controls.Add(this.btnSendFile);
            this.Controls.Add(this.txtInput);
            this.Controls.Add(this.rtxtChat);
            this.MaximizeBox = false;
            this.Name = "PrivateChatForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chat riêng";
            ((System.ComponentModel.ISupportInitialize)(this.picAvatar)).EndInit();
            this.pnlEmoji.ResumeLayout(false);
            this.ctxMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

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
