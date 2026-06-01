using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

class Server
{
    static void Main()
    {
        TcpListener server = new TcpListener(IPAddress.Any, 13000);
        server.Start();
        Console.WriteLine("Server dang chay...");

        while (true)
        {
            // Chờ client
            TcpClient client = server.AcceptTcpClient();
            Console.WriteLine("Co client ket noi!");

            // Tạo luồng riêng
            Thread clientThread = new Thread(HandleClient);
            clientThread.Start(client);
        }
    }

    // Hàm song song hàm main
    static void HandleClient(object obj)
    {
        TcpClient client = (TcpClient)obj;
        NetworkStream stream = client.GetStream();
        byte[] bytes = new byte[256];
        int i;

        // Chờ tin nhắn 
        while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
        {
            string data = Encoding.ASCII.GetString(bytes, 0, i);
            Console.WriteLine("Nhan duoc: " + data);

            byte[] msg = Encoding.ASCII.GetBytes(data.ToUpper());
            stream.Write(msg, 0, msg.Length);
        }

        client.Close();
    }
}