using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.Data.Sqlite;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Server_Chat
{
    public partial class FrmServer : Form
    {
        private TcpListener server;
        private Thread listenThread;
        private bool isRunning = false;
        private Dictionary<string, TcpClient> clientList = new Dictionary<string, TcpClient>();
        private object lockObj = new object();

        public FrmServer()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            InitializeDatabase();
        }

        private void FrmServer_Load(object sender, EventArgs e)
        {
            btnStart.Click += btnStart_Click;
            btnStop.Click += btnStop_Click;
            btnStop.Enabled = false;
        }

        private void Log(string message)
        {
            if (rtbLog.InvokeRequired)
            {
                rtbLog.Invoke(new Action(() => Log(message)));
                return;
            }
            rtbLog.AppendText($"[{DateTime.Now:HH:mm:ss}] {message}\n");
            rtbLog.ScrollToCaret();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (!isRunning)
            {
                listenThread = new Thread(StartServer);
                listenThread.IsBackground = true;
                listenThread.Start();
                btnStart.Enabled = false;
                btnStop.Enabled = true;
                Log("Server đã khởi động trên cổng 8888.");
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (isRunning)
            {
                isRunning = false;
                server?.Stop();
                lock (lockObj)
                {
                    foreach (var client in clientList.Values)
                    {
                        client.Close();
                    }
                    clientList.Clear();
                }
                UpdateUserListUI();
                btnStart.Enabled = true;
                btnStop.Enabled = false;
                Log("Server đã dừng.");
            }
        }

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
                // Stopped
            }
        }

        private void HandleClient(object obj)
        {
            TcpClient client = (TcpClient)obj;
            string clientName = ""; 
            NetworkStream stream = client.GetStream();
            System.IO.StreamReader reader = new System.IO.StreamReader(stream, Encoding.UTF8);
            System.IO.StreamWriter writer = new System.IO.StreamWriter(stream, Encoding.UTF8) { AutoFlush = true };

            try
            {
                string data;
                while ((data = reader.ReadLine()) != null)
                {
                    if (data.StartsWith("AUTH:"))
                    {
                        string[] parts = data.Split(':');
                        if (parts.Length == 3 && CheckLogin(parts[1], parts[2]))
                        {
                            writer.WriteLine("AUTH_OK");
                            Log($"User {parts[1]} đăng nhập thành công.");
                        }
                        else
                        {
                            writer.WriteLine("AUTH_FAIL:Sai tài khoản hoặc mật khẩu");
                            Log($"User {parts[1]} đăng nhập thất bại.");
                        }
                    }
                    else if (data.StartsWith("REG:"))
                    {
                        string[] parts = data.Split(':');
                        if (parts.Length == 3 && RegisterUser(parts[1], parts[2]))
                        {
                            writer.WriteLine("REG_OK");
                            Log($"User {parts[1]} đăng ký thành công.");
                        }
                        else
                            writer.WriteLine("REG_FAIL:Tài khoản đã tồn tại");
                    }
                    else if (data.StartsWith("FORGOT:"))
                    {
                        string[] parts = data.Split(':');
                        if (parts.Length == 3)
                        {
                            if (ResetPassword(parts[1], parts[2]))
                            {
                                writer.WriteLine("FORGOT_OK");
                                Log($"User {parts[1]} đã đổi mật khẩu.");
                            }
                            else
                            {
                                writer.WriteLine("FORGOT_FAIL:Tài khoản không tồn tại");
                            }
                        }
                    }
                    else if (data.StartsWith("LOGIN:"))
                    {
                        clientName = data.Substring(6); 
                        lock (lockObj)
                        {
                            clientList[clientName] = client; 
                        }
                        UpdateUserListUI();
                        UpdateUserList(); 
                    }
                    else if (data.StartsWith("CHAT:"))
                    {
                        string message = data.Substring(5); 
                        Broadcast("CHAT:" + clientName + ": " + message);
                        Log($"[Public] {clientName}: {message}");
                    }
                    else if (data.StartsWith("PRIVATE:"))
                    {
                        string[] parts = data.Split(new char[] { ':' }, 3);
                        if (parts.Length == 3)
                        {
                            SendPrivate(clientName, parts[1], parts[2]);
                            Log($"[Private] {clientName} -> {parts[1]}: {parts[2]}");
                        }
                    }
                    else if (data.StartsWith("TYPING:"))
                    {
                        Broadcast(data);
                    }
                    else if (data.StartsWith("FWD:"))
                    {
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
                        string[] parts = data.Split(new char[] { ':' }, 4);
                        if (parts.Length == 4)
                        {
                            string receiver = parts[1];
                            string fileName = parts[2];
                            string fileData = parts[3].Replace("\r", "").Replace("\n", "");

                            if (receiver == "Tất cả")
                                Broadcast("FILE:" + clientName + ":" + fileName + ":" + fileData);
                            else
                                SendPrivateFile(clientName, receiver, fileName, fileData);
                            Log($"[File] {clientName} -> {receiver}: {fileName}");
                        }
                    }
                }
            }
            catch { }
            finally
            {
                if (clientName != "")
                {
                    lock (lockObj)
                    {
                        clientList.Remove(clientName); 
                    }
                    UpdateUserListUI();
                    UpdateUserList(); 
                    Log($"User {clientName} đã ngắt kết nối.");
                }
                client.Close();
            }
        }

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
                                  );";
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
            catch { return false; } 
        }

        private bool ResetPassword(string username, string newPassword)
        {
            try
            {
                using (SqliteConnection conn = new SqliteConnection("Data Source=Chat.db;"))
                {
                    conn.Open();
                    // Check if exists
                    string checkSql = "SELECT COUNT(*) FROM Users WHERE Username=@u";
                    using (SqliteCommand checkCmd = new SqliteCommand(checkSql, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@u", username);
                        if (Convert.ToInt32(checkCmd.ExecuteScalar()) == 0)
                            return false; // User not found
                    }

                    string sql = "UPDATE Users SET Password=@p WHERE Username=@u";
                    using (SqliteCommand cmd = new SqliteCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@u", username);
                        cmd.Parameters.AddWithValue("@p", newPassword);
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch { return false; }
        }

        private void UpdateUserListUI()
        {
            if (lstOnlineUsers.InvokeRequired)
            {
                lstOnlineUsers.Invoke(new Action(UpdateUserListUI));
                return;
            }
            lstOnlineUsers.Items.Clear();
            lock (lockObj)
            {
                foreach (string name in clientList.Keys)
                {
                    lstOnlineUsers.Items.Add(name);
                }
            }
        }

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
