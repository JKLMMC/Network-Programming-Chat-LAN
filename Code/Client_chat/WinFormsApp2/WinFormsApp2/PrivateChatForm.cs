using System;
using System.Drawing;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace WinFormsApp2
{
    public class PrivateChatForm : Form
    {
        private string myUsername;
        private string targetUsername;
        private NetworkStream stream;

        private RichTextBox rtxtChat;
        private TextBox txtInput;
        private Button btnSendMsg;

        public PrivateChatForm(string me, string target, NetworkStream netStream)
        {
            this.myUsername = me;
            this.targetUsername = target;
            this.stream = netStream;

            InitializePrivateComponent();
        }

        private void InitializePrivateComponent()
        {
            this.Text = $"Chat riêng với: {targetUsername}";
            this.Size = new Size(450, 420);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            
            rtxtChat = new RichTextBox { Dock = DockStyle.Top, Height = 290, ReadOnly = true, BackColor = Color.White, Font = new Font("Segoe UI", 10) };
            
            
            txtInput = new TextBox { Location = new Point(12, 315), Width = 310, Multiline = true, Height = 50, Font = new Font("Segoe UI", 10) };
            
            
            btnSendMsg = new Button { Location = new Point(332, 314), Width = 90, Height = 50, Text = "Gửi", BackColor = SystemColors.Highlight, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 10, FontStyle.Bold) };

            btnSendMsg.Click += BtnSendMsg_Click;
            this.AcceptButton = btnSendMsg; 
            this.Controls.Add(rtxtChat);
            this.Controls.Add(txtInput);
            this.Controls.Add(btnSendMsg);
        }

        private void BtnSendMsg_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtInput.Text) && stream != null)
            {
                try
                {
                    string msgText = txtInput.Text.Trim();
                    
                    
                    string packet = $"PRIVATE|{myUsername}|{targetUsername}|{msgText}";
                    byte[] data = Encoding.UTF8.GetBytes(packet);
                    stream.Write(data, 0, data.Length);

                    
                    AppendMessage("Tôi", msgText);
                    txtInput.Clear();
                    txtInput.Focus();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Không thể gửi tin nhắn riêng: " + ex.Message);
                }
            }
        }

        
        public void AppendMessage(string sender, string message)
        {
            if (rtxtChat.InvokeRequired)
            {
                rtxtChat.Invoke(new Action(() => AppendMessage(sender, message)));
            }
            else
            {
                rtxtChat.AppendText($"{sender}: {message}{Environment.NewLine}");
                rtxtChat.SelectionStart = rtxtChat.Text.Length;
                rtxtChat.ScrollToCaret(); 
            }
        }
    }
}
