using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;
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
        // Dùng Dictionary để ánh xạ Tên người dùng -> Kết nối TcpClient
        private Dictionary<string, TcpClient> clientList = new Dictionary<string, TcpClient>();
        private object lockObj = new object();

        public Form1()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            InitializeDatabase();
        }

        // Sự kiện khi bấm vào button1 trên giao diện Server
        private void button1_Click(object sender, EventArgs e)
        {
            if (!isRunning)
            {
                button1.Text = "Server đang chạy...";
                button1.Refresh(); // Cập nhật chữ trên nút ngay lập tức

                // Mở luồng ngầm để Server chạy mà không làm đơ giao diện
                listenThread = new Thread(StartServer);
                listenThread.IsBackground = true;
                listenThread.Start();

                Thread.Sleep(500); // Chờ nửa giây tạo cảm giác đang khởi động
                button1.Text = "Server đã bật"; // Cập nhật lại nút theo yêu cầu
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

        // Hàm xử lý tin nhắn (Đã nâng cấp StreamReader để đọc file lớn không bị vỡ)
        private void HandleClient(object obj)
        {
            TcpClient client = (TcpClient)obj;
            string clientName = ""; 
            NetworkStream stream = client.GetStream();
            
            // Dùng StreamReader để tự động đọc trọn vẹn 1 dòng (tránh đứt đoạn khi gửi file lớn)
            System.IO.StreamReader reader = new System.IO.StreamReader(stream, Encoding.UTF8);
            System.IO.StreamWriter writer = new System.IO.StreamWriter(stream, Encoding.UTF8) { AutoFlush = true };

            try
            {
                string data;
                while ((data = reader.ReadLine()) != null)
                {
                    // Xử lý theo giao thức đơn giản
                    if (data.StartsWith("AUTH:")) // Yêu cầu đăng nhập
                    {
                        string[] parts = data.Split(':');
                        if (parts.Length == 3 && CheckLogin(parts[1], parts[2]))
                            writer.WriteLine("AUTH_OK");
                        else
                            writer.WriteLine("AUTH_FAIL:Sai tài khoản hoặc mật khẩu");
                    }
                    else if (data.StartsWith("REG:")) // Yêu cầu đăng ký
                    {
                        string[] parts = data.Split(':');
                        if (parts.Length == 3 && RegisterUser(parts[1], parts[2]))
                            writer.WriteLine("REG_OK");
                        else
                            writer.WriteLine("REG_FAIL:Tài khoản đã tồn tại");
                    }
                    else if (data.StartsWith("LOGIN:"))
                    {
                        clientName = data.Substring(6); 
                        lock (lockObj)
                        {
                            clientList[clientName] = client; 
                        }
                        UpdateUserList(); 
                    }
                    else if (data.StartsWith("CHAT:"))
                    {
                        string message = data.Substring(5); 
                        Broadcast("CHAT:" + clientName + ": " + message);
                    }
                    else if (data.StartsWith("PRIVATE:"))
                    {
                        string[] parts = data.Split(new char[] { ':' }, 3);
                        if (parts.Length == 3)
                        {
                            SendPrivate(clientName, parts[1], parts[2]);
                        }
                    }
                    else if (data.StartsWith("TYPING:"))
                    {
                        // Broadcast TYPING (Broadcast đã tự thêm newline, không ghép thêm nữa)
                        Broadcast(data);
                    }
                    else if (data.StartsWith("FWD:"))
                    {
                        // FWD:Target:OldMsg
                        string[] parts = data.Split(new char[] { ':' }, 3);
                        if (parts.Length == 3)
                        {
                            string target = parts[1];
                            string msg = parts[2];
                            SendPrivate(clientName, target, "[Chuyển tiếp] " + msg);
                        }
                    }
                    else if (data.StartsWith("FILE:"))
                    {
                        // Cú pháp: FILE:NgườiNhận:TênFile:DữLiệuBase64
                        string[] parts = data.Split(new char[] { ':' }, 4);
                        if (parts.Length == 4)
                        {
                            string receiver = parts[1];
                            string fileName = parts[2];
                            // Lọc bỏ ký tự xuống dòng thừa trong fileData (phòng nguyần nhân desync giao thức)
                            string fileData = parts[3].Replace("\r", "").Replace("\n", "");

                            if (receiver == "Tất cả")
                                Broadcast("FILE:" + clientName + ":" + fileName + ":" + fileData);
                            else
                                SendPrivateFile(clientName, receiver, fileName, fileData);
                        }
                    }
                }
            }
            catch
            {
            }
            finally
            {
                if (clientName != "")
                {
                    lock (lockObj)
                    {
                        clientList.Remove(clientName); 
                    }
                    UpdateUserList(); 
                }
                client.Close();
            }
        }

        // --- CÁC HÀM XỬ LÝ CƠ SỞ DỮ LIỆU ---
        private void InitializeDatabase()
        {
            try
            {
                using (SqliteConnection conn = new SqliteConnection("Data Source=Chat.db;"))
                {
                    conn.Open();
                    string sql = @"CREATE TABLE IF NOT EXISTS Users (
                                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                    Username TEXT UNIQUE NOT NULL,
                                    Password TEXT NOT NULL
                                  );
                                  INSERT OR IGNORE INTO Users (Username, Password) VALUES ('test1', '123');
                                  INSERT OR IGNORE INTO Users (Username, Password) VALUES ('test2', '123');";
                    using (SqliteCommand cmd = new SqliteCommand(sql, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch { }
        }

        private bool CheckLogin(string username, string password)
        {
            try
            {
                using (SqliteConnection conn = new SqliteConnection("Data Source=Chat.db;"))
                {
                    conn.Open();
                    string sql = "SELECT COUNT(*) FROM Users WHERE Username=@u AND Password=@p";
                    using (SqliteCommand cmd = new SqliteCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@u", username);
                        cmd.Parameters.AddWithValue("@p", password);
                        return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
                    }
                }
            }
            catch { return false; }
        }

        private bool RegisterUser(string username, string password)
        {
            try
            {
                using (SqliteConnection conn = new SqliteConnection("Data Source=Chat.db;"))
                {
                    conn.Open();
                    string sql = "INSERT INTO Users (Username, Password) VALUES (@u, @p)";
                    using (SqliteCommand cmd = new SqliteCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@u", username);
                        cmd.Parameters.AddWithValue("@p", password);
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch { return false; } // Trùng tên
        }

        // --- CÁC HÀM ĐỊNH TUYẾN ---

        // Hàm gửi danh sách người dùng đang online
        private void UpdateUserList()
        {
            string userList = "LIST:";
            lock (lockObj)
            {
                foreach (string name in clientList.Keys)
                {
                    userList += name + ",";
                }
            }
            if (userList.EndsWith(",")) userList = userList.TrimEnd(',');
            Broadcast(userList);
        }

        // Hàm gửi tin nhắn cho tất cả mọi người
        private void Broadcast(string data)
        {
            data = data + Environment.NewLine;
            byte[] msg = Encoding.UTF8.GetBytes(data);
            lock (lockObj)
            {
                foreach (TcpClient c in clientList.Values)
                {
                    try { c.GetStream().Write(msg, 0, msg.Length); } catch { }
                }
            }
        }

        private void SendPrivate(string sender, string receiver, string msg)
        {
            string formattedMsg = "PRIVATE_CHAT:" + sender + ":" + msg + Environment.NewLine;
            byte[] bytes = Encoding.UTF8.GetBytes(formattedMsg);
            
            lock (lockObj)
            {
                if (clientList.ContainsKey(receiver))
                    try { clientList[receiver].GetStream().Write(bytes, 0, bytes.Length); } catch { }
            }
        }

        private void SendPrivateFile(string sender, string receiver, string fileName, string fileData)
        {
            string formattedMsg = "FILE:" + sender + ":" + fileName + ":" + fileData + Environment.NewLine;
            byte[] bytes = Encoding.UTF8.GetBytes(formattedMsg);
            
            lock (lockObj)
            {
                if (clientList.ContainsKey(receiver))
                    try { clientList[receiver].GetStream().Write(bytes, 0, bytes.Length); } catch { }
                if (clientList.ContainsKey(sender))
                    try { clientList[sender].GetStream().Write(bytes, 0, bytes.Length); } catch { }
            }
        }
    }
}