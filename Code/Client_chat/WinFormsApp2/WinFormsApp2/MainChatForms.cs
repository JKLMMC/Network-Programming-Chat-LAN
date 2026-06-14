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
        private System.IO.StreamReader reader;
        private System.IO.StreamWriter writer;

        private string username;

        public MainChatForms(string name)
        {
            InitializeComponent();
            
            // Lưu tên người dùng
            username = name;
            
            // Tự động kết nối Server khi mở Form Chat
            ConnectToServer();
        }

        private void ConnectToServer()
        {
            try
            {
                client = new TcpClient("127.0.0.1", 8888);
                stream = client.GetStream();
                reader = new System.IO.StreamReader(stream, Encoding.UTF8);
                writer = new System.IO.StreamWriter(stream, Encoding.UTF8) { AutoFlush = true };
                
                // 1. Gửi tên người dùng lên Server ngay khi kết nối thành công (Dùng chuẩn LOGIN)
                writer.WriteLine("LOGIN:" + username);

                // 2. Chạy luồng ngầm nhận tin nhắn liên tục
                Task.Run(() => ListenForMessages());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối đến Server: " + ex.Message, "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ListenForMessages()
        {
            while (client != null && client.Connected)
            {
                try
                {
                    string message = reader.ReadLine();
                    if (message != null)
                    {
                        // PHÂN TÍCH TIN NHẮN TỪ SERVER GỬI VỀ
                        if (message.StartsWith("LIST:"))
                        {
                            string[] users = message.Substring(5).Split(',');
                            this.Invoke(new Action(() => {
                                lstOnlineUsers.Items.Clear();
                                lstOnlineUsers.Items.Add("Tất cả"); // Mặc định là chat nhóm
                                foreach (string u in users)
                                {
                                    if (!string.IsNullOrEmpty(u) && u != username) // Không tự hiện mình
                                    {
                                        lstOnlineUsers.Items.Add(u);
                                    }
                                }
                                lstOnlineUsers.SelectedIndex = 0; // Chọn sẵn "Tất cả"
                            }));
                        }
                        else if (message.StartsWith("CHAT:"))
                        {
                            string chatMsg = message.Substring(5);
                            this.Invoke(new Action(() => {
                                rtxtChatLog.AppendText(chatMsg + Environment.NewLine);
                            }));
                        }
                        else if (message.StartsWith("PRIVATE:"))
                        {
                            string privateMsg = message.Substring(8);
                            this.Invoke(new Action(() => {
                                rtxtChatLog.SelectionColor = Color.Blue; // Đổi màu để dễ nhận biết
                                rtxtChatLog.AppendText(privateMsg + Environment.NewLine);
                                rtxtChatLog.SelectionColor = rtxtChatLog.ForeColor; // Đổi lại màu cũ
                            }));
                        }
                        else if (message.StartsWith("FILE:"))
                        {
                            // Cú pháp từ server: FILE:Sender:FileName:Base64
                            string[] parts = message.Split(new char[] { ':' }, 4);
                            if (parts.Length == 4)
                            {
                                string senderName = parts[1];
                                string fileName = parts[2];
                                string fileData = parts[3];

                                this.Invoke(new Action(() => {
                                    rtxtChatLog.SelectionColor = Color.Green;
                                    rtxtChatLog.AppendText($"[{senderName} đã gửi file: {fileName}]\n");
                                    rtxtChatLog.SelectionColor = rtxtChatLog.ForeColor;
                                    
                                    DialogResult res = MessageBox.Show($"Bạn nhận được file '{fileName}' từ {senderName}.\nBạn có muốn tải về máy không?", "Nhận File", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                                    if (res == DialogResult.Yes)
                                    {
                                        using (SaveFileDialog sfd = new SaveFileDialog())
                                        {
                                            sfd.FileName = fileName;
                                            if (sfd.ShowDialog() == DialogResult.OK)
                                            {
                                                byte[] fileBytes = Convert.FromBase64String(fileData);
                                                System.IO.File.WriteAllBytes(sfd.FileName, fileBytes);
                                                MessageBox.Show("Lưu file thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            }
                                        }
                                    }
                                }));
                            }
                        }
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
                    string target = "Tất cả";
                    if (lstOnlineUsers.SelectedItem != null)
                    {
                        target = lstOnlineUsers.SelectedItem.ToString();
                    }

                    if (target == "Tất cả")
                        writer.WriteLine("CHAT:" + txtMessage.Text);
                    else
                        writer.WriteLine("PRIVATE:" + target + ":" + txtMessage.Text);

                    txtMessage.Clear(); // Dọn trống ô nhập
                    txtMessage.Focus(); // Đưa con trỏ chuột về gõ tiếp
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể gửi tin nhắn: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // XỬ LÝ SỰ KIỆN BẤM NÚT "GỬI FILE"
        private void btnSendFile_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog ofd = new OpenFileDialog())
                {
                    ofd.Title = "Chọn file cần gửi";
                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        // Đọc file thành mảng byte
                        byte[] fileBytes = System.IO.File.ReadAllBytes(ofd.FileName);
                        
                        // Cảnh báo nếu file quá lớn (giới hạn sinh viên thường làm dưới 5MB)
                        if (fileBytes.Length > 5 * 1024 * 1024)
                        {
                            MessageBox.Show("Vui lòng gửi file dưới 5MB để đảm bảo kết nối!", "File quá lớn", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        // Mã hóa thành chuỗi Base64 để gửi qua luồng Text (StreamWriter)
                        string base64File = Convert.ToBase64String(fileBytes);
                        string fileName = System.IO.Path.GetFileName(ofd.FileName);

                        string target = "Tất cả";
                        if (lstOnlineUsers.SelectedItem != null)
                        {
                            target = lstOnlineUsers.SelectedItem.ToString();
                        }

                        // Gửi đi theo cú pháp: FILE:NgườiNhận:TênFile:Base64
                        writer.WriteLine("FILE:" + target + ":" + fileName + ":" + base64File);
                        
                        // Báo lên màn hình là mình đã gửi thành công
                        rtxtChatLog.SelectionColor = Color.Green;
                        rtxtChatLog.AppendText($"[Bạn đã gửi file: {fileName}]\n");
                        rtxtChatLog.SelectionColor = rtxtChatLog.ForeColor;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi gửi file: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}