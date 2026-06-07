using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace ChatServer
{
    class Program
    {
        // Danh sách quản lý tất cả các Client kết nối để xử lý BROADCAST
        private static List<TcpClient> connectedClients = new List<TcpClient>();
        private static object lockObject = new object();

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            
            // Khởi tạo Server lắng nghe tại Port 8888
            TcpListener server = new TcpListener(IPAddress.Any, 8888);
            server.Start();
            Console.WriteLine("=== SERVER CHAT ĐÃ SẴN SÀNG TRÊN CỔNG 8888 ===");

            while (true)
            {
                // Chờ đợi Client kết nối vào
                TcpClient client = server.AcceptTcpClient();
                
                lock (lockObject)
                {
                    connectedClients.Add(client);
                }
                Console.WriteLine($"[KẾT NỐI] Có 1 client mới tham gia. Tổng số: {connectedClients.Count}");

                // Tạo một Thread riêng để xử lý nhận tin nhắn cho từng Client độc lập
                Thread clientThread = new Thread(HandleClient);
                clientThread.Start(client);
            }
        }

        private static void HandleClient(object obj)
        {
            TcpClient client = (TcpClient)obj;
            NetworkStream stream = client.GetStream();
            byte[] buffer = new byte[1024];
            int bytesRead;

            try
            {
                while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    Console.WriteLine($"[SERVER NHẬN]: {message}");

                    // THỰC HIỆN BROADCAST: Gửi tin nhắn này cho TẤT CẢ các client trong phòng
                    BroadcastMessage(message);
                }
            }
            catch
            {
                // Xử lý khi Client ngắt kết nối đột ngột
            }
            finally
            {
                lock (lockObject)
                {
                    connectedClients.Remove(client);
                }
                client.Close();
                Console.WriteLine($"[NGẮT KẾT NỐI] 1 client đã rời phòng. Còn lại: {connectedClients.Count}");
            }
        }

        // Hàm Broadcast đẩy dữ liệu đi toàn phòng chat
        private static void BroadcastMessage(string message)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(message);

            lock (lockObject)
            {
                foreach (TcpClient client in connectedClients)
                {
                    try
                    {
                        if (client.Connected)
                        {
                            NetworkStream stream = client.GetStream();
                            stream.Write(buffer, 0, buffer.Length);
                        }
                    }
                    catch
                    {
                        // Bỏ qua nếu có client bị lỗi kết nối
                    }
                }
            }
        }
    }
}
