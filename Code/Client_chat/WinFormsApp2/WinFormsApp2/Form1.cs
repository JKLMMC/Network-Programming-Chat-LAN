#nullable disable
using System;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace WinFormsApp2
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ tên và mật khẩu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!Regex.IsMatch(txtUsername.Text, @"^[a-zA-Z0-9_]{3,20}$"))
            {
                MessageBox.Show("Tên đăng nhập chỉ được chứa chữ cái, số, dấu gạch dưới và từ 3-20 ký tự.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Gửi yêu cầu đăng nhập lên Server
            if (SendAuthCommand("AUTH:" + txtUsername.Text + ":" + txtPassword.Text, out string response))
            {
                if (response == "AUTH_OK")
                {
                    this.Hide();
                    MainChatForms chatForm = new MainChatForms(txtUsername.Text);
                    chatForm.ShowDialog();
                    this.Close();
                }
                else if (response.StartsWith("AUTH_FAIL:"))
                {
                    MessageBox.Show(response.Substring(10), "Đăng nhập thất bại", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public void lnkRegister_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Vui lòng nhập tài khoản và mật khẩu muốn tạo vào các ô trống trước!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!Regex.IsMatch(txtUsername.Text, @"^[a-zA-Z0-9_]{3,20}$"))
            {
                MessageBox.Show("Tên đăng nhập chỉ được chứa chữ cái, số, dấu gạch dưới và từ 3-20 ký tự.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string passPattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&._\-+])[A-Za-z\d@$!%*?&._\-+]{6,}$";
            if (!Regex.IsMatch(txtPassword.Text, passPattern))
            {
                MessageBox.Show("Mật khẩu phải dài ít nhất 6 ký tự, bao gồm chữ hoa, chữ thường, số và ký tự đặc biệt.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Gửi yêu cầu đăng ký lên Server
            if (SendAuthCommand("REG:" + txtUsername.Text + ":" + txtPassword.Text, out string response))
            {
                if (response == "REG_OK")
                {
                    MessageBox.Show("Đăng ký thành công! Bạn có thể nhấn Đăng nhập ngay.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (response.StartsWith("REG_FAIL:"))
                {
                    MessageBox.Show(response.Substring(9), "Đăng ký thất bại", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void lnkForgot_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Vui lòng nhập tài khoản và mật khẩu mới vào các ô trống trước để đặt lại mật khẩu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (SendAuthCommand("FORGOT:" + txtUsername.Text + ":" + txtPassword.Text, out string response))
            {
                if (response == "FORGOT_OK")
                {
                    MessageBox.Show("Đặt lại mật khẩu thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (response.StartsWith("FORGOT_FAIL:"))
                {
                    MessageBox.Show(response.Substring(12), "Thất bại", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private bool SendAuthCommand(string command, out string response)
        {
            response = "";
            try
            {
                System.Net.Sockets.TcpClient tempClient = new System.Net.Sockets.TcpClient("127.0.0.1", 8888);
                System.IO.StreamReader reader = new System.IO.StreamReader(tempClient.GetStream(), System.Text.Encoding.UTF8);
                System.IO.StreamWriter writer = new System.IO.StreamWriter(tempClient.GetStream(), System.Text.Encoding.UTF8) { AutoFlush = true };

                writer.WriteLine(command);
                response = reader.ReadLine();
                tempClient.Close();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể kết nối tới Server: " + ex.Message, "Lỗi mạng", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
    }
}