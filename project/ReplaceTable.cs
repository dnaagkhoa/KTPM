﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace project
{
    public partial class ReplaceTable : Form
    {

        public ComboBox CbbTableFrom => cbbTableFrom;
        public ComboBox CbbTableTo => cbbTableTo;
        public DataGridView DgvTableFrom => dgvTableFrom;


        public DataTable tableBill;
        public bool condition = true;
        public ReplaceTable()
        {
            InitializeComponent();
        }
        public ReplaceTable(string nameTableFrom) : this()
        {
            loadDataTable();
            cbbTableFrom.Text = nameTableFrom;
        }

        //Load len cho nguoi dung chon ban
        public void loadDataTable()
        {
            DataProvider provider = new DataProvider();
            DataTable table = provider.loadTableF();
            for (int i = 0; i < table.Rows.Count; i++)
            {
                cbbTableFrom.Items.Add(table.Rows[i][0].ToString());
                cbbTableTo.Items.Add(table.Rows[i][0].ToString());
            }
        }

        //Nhan nut Chuyen ban
        public void btnAccept_Click(object sender, EventArgs e)
        {
            //Dieu kien chuyen ban
            if (condition == true && cbbTableFrom.Text != cbbTableTo.Text && cbbTableTo.Text != "")
            {
                DialogResult ms = MessageBox.Show("Bạn có muốn chuyển bàn " + cbbTableFrom.Text + " đến bàn " + cbbTableTo.Text + " không?", "Xác nhận", MessageBoxButtons.YesNoCancel, MessageBoxIcon.None);
                //chuyen ban
                if (ms == DialogResult.Yes)
                {
                    moveTable();
                    MessageBox.Show("Đã chuyển bàn thành công!", "Hoàn tất", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else if (ms == DialogResult.No)
                {
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Chuyển bàn không thành công!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        //Ham chuyen ban
        public void moveTable()
        {
            //Gan Food cho ban moi
            for (int i = 0; i < tableBill.Rows.Count; i++)
            {
                DataProvider provider = new DataProvider();
                string nameF = tableBill.Rows[i][0].ToString();
                int count = Int16.Parse(tableBill.Rows[i][1].ToString());
                provider.move_food(cbbTableTo.Text, nameF, count);
            }
            //Gan Data cho ban moi + Xoa Bill va Table cu
            DataProvider providerN = new DataProvider();
            providerN.move_table(float.Parse(txtTotal.Text), cbbTableTo.Text, cbbTableFrom.Text);
        }


        //Kiem tra ban A
        public void cbbTableFrom_TextChanged(object sender, EventArgs e)
        {
            loadCheckTable();
        }
        public void loadCheckTable()
        {
            try
            {
                //DK hop le
                DataProvider provider = new DataProvider();
                tableBill = provider.check_table(cbbTableFrom.Text);
                dgvTableFrom.DataSource = tableBill;
                txtTotal.Text = tableBill.Rows[0][2].ToString();
                condition = true;
            }
            catch
            {
                //DK khong hop le
                MessageBox.Show("Không thể chọn bàn này!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                condition = false;
            }
        }


        //Kiem tra ban B
        public void cbbTableTo_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadCheckTableB();
        }
        public void loadCheckTableB()
        {
            try
            {
                DataProvider provider = new DataProvider();
                DataTable tableBill = provider.check_table(cbbTableTo.Text);
                if (tableBill.Rows[0][2].ToString() != null)
                {
                    //DK khong hop le
                    MessageBox.Show("Không thể chọn bàn này!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    condition = false;
                }
                //Dieu kien hop le
                condition = true;
            }
            catch
            {
            }
        }





    }
}