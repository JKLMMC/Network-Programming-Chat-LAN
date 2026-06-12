using System;
using System.Net.Sockets;
using System.Text;

namespace Server_Chat
{
    public partial class Form1
    {
        private void HandlePrivateChat(string[] parts)
        {
            try
            {
                string sender = parts[1];
                string receiver = parts[2];
                string message = parts[3];
                lock (onlineClients)
                {
                    if (onlineClients.TryGetValue(receiver, out TcpClient receiverSocket))
                    {
                        string forwardPacket = $"PRIVATE|{sender}|{message}";
                        byte[] forwardBytes = Encoding.UTF8.GetBytes(forwardPacket);
                        receiverSocket.GetStream().Write(forwardBytes, 0, forwardBytes.Length);
                    }
                    else
                    {
                        
                    }
                }
            }
            catch (Exception ex)
            {
            
                Console.WriteLine("Lỗi định tuyến tin nhắn: " + ex.Message);
            }
        }
    }
}