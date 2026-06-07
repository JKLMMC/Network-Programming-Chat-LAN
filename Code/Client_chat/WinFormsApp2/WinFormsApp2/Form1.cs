using System;
using System.Windows.Forms;

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
            // Kiểm tra nếu người dùng đã nhập tên
            if (!string.IsNullOrWhiteSpace(txtUsername.Text) && txtUsername.Text != "Nhập tên người dùng...")
            {
                // 1. Ẩn Form đăng nhập hiện tại đi
                this.Hide();

                // 2. Tạo và hiển thị Form Chat chính (Đã sửa thêm chữ s)
                MainChatForms chatForm = new MainChatForms();
                chatForm.ShowDialog();

                // 3. Sau khi đóng Form Chat thì đóng toàn bộ ứng dụng
                this.Close();
            }
            else
            {
                MessageBox.Show("Vui lòng nhập tên trước khi vào phòng chat!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}