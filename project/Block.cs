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
    public partial class Block : Form
    {

        public TextBox TxtMatKhau => txtMatKhau; // Change private to public

        public string password;
        public Block()
        {
            InitializeComponent();
        }
        public Block(string user, string name, string pass) : this()
        {
            lblName.Text = name;
            password = pass;
        }
        public PictureBox PbErr => pbErr;
        public Label LblName => lblName;

        //khi nhap vao mot ki tu thi goi ham kiem tra
        public void txtMatKhau_TextChanged(object sender, EventArgs e)
        {
            check();
        }

        public void check()
        {
            if (txtMatKhau.Text == password)
            {
                //nhap dung mat khaus
                this.Close();
            }
            else
            {
                txtMatKhau.Focus();
                pbErr.Visible = true;
            }
        }

        public void checkPassword(string enteredPassword)
        {
            if (enteredPassword == password)
            {
                // Nhập đúng mật khẩu
                this.Close();
            }
            else
            {
                txtMatKhau.Focus();
                pbErr.Visible = true;
            }
        }

        public void txtMatKhau_Leave(object sender, EventArgs e)
        {
            txtMatKhau.Focus();
        }

        public void Block_Leave(object sender, EventArgs e)
        {
            txtMatKhau.Focus();
        }

        private void lblName_Click(object sender, EventArgs e)
        {

        }
    }
}