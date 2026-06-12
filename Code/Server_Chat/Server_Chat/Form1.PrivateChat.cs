using System;
using System.Net.Sockets;
using System.Text;

namespace Server_Chat 
{
    public partial class Form1
    {
        
        public void HandlePrivateChat(string[] parts)
        {
            if (parts.Length < 4) return;

            string sender = parts[1];
            string receiver = parts[2];
            string message = parts[3];

            lock (onlineClients)
            {
                
                if (onlineClients.ContainsKey(receiver))
                {
                    TcpClient receiverSocket = onlineClients[receiver];
                    try
                    {
                        
                        string packet = $"PRIVATE|{sender}|{message}";
                        byte[] data = Encoding.UTF8.GetBytes(packet);
                        receiverSocket.GetStream().Write(data, 0, data.Length);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Lỗi chuyển tiếp tin nhắn từ {sender} đến {receiver}: {ex.Message}");
                    }
                }
                else
                {
                    
                    if (onlineClients.ContainsKey(sender))
                    {
                        TcpClient senderSocket = onlineClients[sender];
                        string errorPacket = $"PRIVATE|Hệ thống|Người dùng {receiver} hiện đã offline.";
                        byte[] data = Encoding.UTF8.GetBytes(errorPacket);
                        senderSocket.GetStream().Write(data, 0, data.Length);
                    }
                }
            }
        }
    }
}
