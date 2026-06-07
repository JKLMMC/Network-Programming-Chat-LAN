using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Net.Sockets; 
using System.Text;        
using System.Threading.Tasks; 
using System.Windows.Forms;

namespace WinFormsApp2
{
    public partial class MainChatForms : Form
    {
        private TcpClient client;
        private NetworkStream stream;

        public MainChatForms()
        {
            InitializeComponent();
            
            // Tự động kết nối Server khi mở Form Chat
            ConnectToServer();
        }

        private void ConnectToServer()
        {
            try
            {
                client = new TcpClient("127.0.0.1", 8888);
                stream = client.GetStream();
                
                // Chạy luồng ngầm nhận tin nhắn liên tục
                Task.Run(() => ListenForMessages());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối đến Server: " + ex.Message, "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ListenForMessages()
        {
            byte[] buffer = new byte[1024];
            while (client != null && client.Connected)
            {
                try
                {
                    int bytesRead = stream.Read(buffer, 0, buffer.Length);
                    if (bytesRead > 0)
                    {
                        string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                        // ĐỒNG BỘ GIỮA SOCKET VÀ GIAO DIỆN CHAT (Đã map chuẩn biến rtxtChatLog)
                        this.Invoke(new Action(() => {
                            rtxtChatLog.AppendText(message + Environment.NewLine);
                        }));
                    }
                }
                catch
                {
                    break;
                }
            }
        }

        // XỬ LÝ SỰ KIỆN BẤM NÚT "GỬI" (Đã map chuẩn biến txtMessage và stream)
        public void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(txtMessage.Text) && stream != null)
                {
                    byte[] buffer = Encoding.UTF8.GetBytes(txtMessage.Text);
                    stream.Write(buffer, 0, buffer.Length); // Gửi mảng byte lên Server

                    txtMessage.Clear(); // Dọn trống ô nhập
                    txtMessage.Focus(); // Đưa con trỏ chuột về gõ tiếp
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể gửi tin nhắn: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}