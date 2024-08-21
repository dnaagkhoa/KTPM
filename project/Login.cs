using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace project
{
    public partial class frmLogin : Form
    {
        string name;
        
        public DataProvider DataProvider;

        public string GetName()
        {
            return name;
        }
        public void SetUsername(string username)
        {
            txtUsername.Text = username;
        }
        public void SetPassword(string password)
        {
            txtPassword.Text = password;
        }
        public RadioButton GetAdminRadioButton()
        {
            return rdbAdmin;
        }
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult ms = MessageBox.Show("Bạn có muốn thoát không? ", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (ms == DialogResult.Yes)
            {
                this.Close();
            }
        }

        //Ham kiem tra Dang nhap
        public int CheckLogin(string username, string password, string type)
        {
            DataProvider provider = new DataProvider();
            DataTable table = provider.loadAccount();

            bool accountExists = false;

            for (int i = 0; i < table.Rows.Count; i++)
            {
                if (table.Rows[i][0].ToString() == username)
                {
                    accountExists = true; // The account exists in the database
                    if (table.Rows[i][2].ToString() == password)
                    {
                        if (table.Rows[i][3].ToString() == type)
                        {
                            name = table.Rows[i][1].ToString();
                            MessageBox.Show("Xin chào " + name + " :)", "Đăng nhập thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return 0; // Successful login
                        }
                        else
                        {
                            return 2; // Incorrect role
                        }
                    }
                }
            }

            if (!accountExists)
            {
                return 3; // Account does not exist
            }

            return 1; // Invalid credentials
        }




        //Su kien click btnLogin
        public void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                string user = txtUsername.Text;
                string pass = txtPassword.Text;
                string type = "CASHIER";

                if (rdbAdmin.Checked == true)
                {
                    type = "ADMIN";
                }

                // Check if username or password is empty
                if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(pass))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ tài khoản và mật khẩu.", "Lỗi...", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int loginResult = CheckLogin(user, pass, type);

                if (loginResult == 0)
                {
                    frmMain main = new frmMain(user, name, pass, type);
                    this.Hide();
                    main.ShowDialog();
                    this.Show();
                }
                else if (loginResult == 1)
                {
                    MessageBox.Show("Sai tài khoản hoặc mật khẩu", "Lỗi...", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (loginResult == 2)
                {
                    MessageBox.Show("Sai quyền truy cập. Vui lòng chọn đúng quyền đăng nhập", "Lỗi...", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (loginResult == 3)
                {
                    MessageBox.Show("Tài khoản không tồn tại trong cơ sở dữ liệu", "Lỗi...", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch
            {
                // Database does not exist
                MessageBox.Show("Cơ sở dữ liệu không tồn tại. Vui lòng tạo mới theo file hướng dẫn", "Lỗi...", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }




        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {

        }

        private void rdbCashier_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void lblMatKhau_Click(object sender, EventArgs e)
        {

        }

        private void frmLogin_Load(object sender, EventArgs e)
        {

        }

        public void btnThoat_Click(object value1, object value2)
        {
            throw new NotImplementedException();
        }
    }
}
