namespace WinFormsApp3
{
    public partial class FrmForgotPassword : Form
    {
        public FrmForgotPassword()
        {
            InitializeComponent();
        }

        private void FrmForgotPassword_Load(object sender, EventArgs e)
        {

        }
        private void btnReset_Click(object sender, EventArgs e)
        {
            if (txtUserName.Text.Trim() == "" ||
                txtNewPassword.Text.Trim() == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
                return;
            }

            MessageBox.Show("Đặt lại mật khẩu thành công!");
        }
    }
}
