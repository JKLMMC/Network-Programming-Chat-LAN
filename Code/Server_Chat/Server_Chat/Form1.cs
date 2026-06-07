using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Server_Chat
{
    public partial class Form1 : Form
    {
        private TcpListener server;
        private Thread listenThread;
        private bool isRunning = false;

        public Form1()
        {
            InitializeComponent();
        }

        // Sự kiện khi bấm vào button1 trên giao diện Server
        private void button1_Click(object sender, EventArgs e)
        {
            if (!isRunning)
            {
                // Mở luồng ngầm để Server chạy mà không làm đơ giao diện
                listenThread = new Thread(StartServer);
                listenThread.IsBackground = true;
                listenThread.Start();

                button1.Text = "Server đang chạy...";
                button1.Enabled = false;
            }
        }

        // Lõi TCP Server (đã chuyển port 8888 khớp với Client)
        private void StartServer()
        {
            try
            {
                server = new TcpListener(IPAddress.Any, 8888);
                server.Start();
                isRunning = true;

                while (isRunning)
                {
                    TcpClient client = server.AcceptTcpClient();
                    Thread clientThread = new Thread(HandleClient);
                    clientThread.IsBackground = true;
                    clientThread.Start(client);
                }
            }
            catch
            {
                // Xử lý an toàn khi tắt form
            }
        }

        // Hàm xử lý Eco tin nhắn của Giai đoạn 1
        private void HandleClient(object obj)
        {
            TcpClient client = (TcpClient)obj;
            NetworkStream stream = client.GetStream();
            byte[] bytes = new byte[256];
            int i;

            try
            {
                while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                {
                    // Dùng UTF8
                    string data = Encoding.UTF8.GetString(bytes, 0, i);

                    byte[] msg = Encoding.UTF8.GetBytes(data.ToUpper());
                    stream.Write(msg, 0, msg.Length);
                }
            }
            catch
            {
                // Bỏ qua lỗi ngắt kết nối đột ngột
            }
            finally
            {
                client.Close();
            }
        }
    }
}