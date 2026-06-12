    using System;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace WinFormsApp2 
{
    public partial class MainChatForms
    {
        public void SendPrivateMessage(string receiver, string message)
        {
            if (string.IsNullOrWhiteSpace(receiver) || string.IsNullOrWhiteSpace(message))
                return;

            try
            {
                string packet = $"PRIVATE|{myUsername}|{receiver}|{message}";
                byte[] data = Encoding.UTF8.GetBytes(packet);
                stream.Write(data, 0, data.Length);
                txtChatBox.AppendText($"[Bạn -> {receiver}]: {message}" + Environment.NewLine);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể gửi tin nhắn, lỗi luồng TCP: " + ex.Message);
            }
        }
        private void ProcessIncomingPrivateMessage(string[] parts)
        {
            string senderName = parts[1];
            string msgContent = parts[2]; 
            this.Invoke(new Action(() => {
                txtChatBox.AppendText($"[{senderName} nhắn riêng]: {msgContent}" + Environment.NewLine);
            }));
        }
    }
}