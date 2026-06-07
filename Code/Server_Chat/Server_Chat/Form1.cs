using System;
using System.Collections.Generic;
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
        private List<TcpClient> clientList = new List<TcpClient>();
        private object lockObj = new object();

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

        // Hàm xử lý tin nhắn (Đã chuyển thành Broadcast cho Giai đoạn 2)
        private void HandleClient(object obj)
        {
            TcpClient client = (TcpClient)obj;
            lock (lockObj)
            {
                clientList.Add(client); // Thêm client vào danh sách quản lý
            }
            
            NetworkStream stream = client.GetStream();
            byte[] bytes = new byte[1024]; // Tăng buffer lên 1024 giống Client
            int i;

            try
            {
                while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                {
                    // Dùng UTF8 để đọc tin nhắn
                    string data = Encoding.UTF8.GetString(bytes, 0, i);

                    // Đóng gói lại thành byte để Broadcast
                    byte[] msg = Encoding.UTF8.GetBytes(data);

                    // Gửi tin nhắn cho tất cả các Client trong danh sách
                    lock (lockObj)
                    {
                        foreach (TcpClient c in clientList)
                        {
                            try
                            {
                                NetworkStream cStream = c.GetStream();
                                cStream.Write(msg, 0, msg.Length);
                            }
                            catch
                            {
                                // Bỏ qua nếu có lỗi khi ghi đến 1 client nào đó
                            }
                        }
                    }
                }
            }
            catch
            {
                // Bỏ qua lỗi ngắt kết nối đột ngột
            }
            finally
            {
                lock (lockObj)
                {
                    clientList.Remove(client); // Xóa client khỏi danh sách khi thoát
                }
                client.Close();
            }
        }
    }
}