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
    public partial class PlusTable : Form
    {

        public ComboBox CbbTableA => cbbTableA;
        public ComboBox CbbTableB => cbbTableB;


        public DataTable datatableA;
        public DataTable datatableB;
        public bool conditionA = true;
        public bool conditionB = false;
        public PlusTable()
        {
            InitializeComponent();
        }
        public PlusTable(string nameTableFrom)
            : this()
        {
            loadDataTable();
            cbbTableA.Text = nameTableFrom;
        }

        //Load len cho nguoi dung xem thoi
        public void loadDataTable()
        {
            DataProvider provider = new DataProvider();
            DataTable table = provider.loadTableF();
            for (int i = 0; i < table.Rows.Count; i++)
            {
                cbbTableA.Items.Add(table.Rows[i][0].ToString());
                cbbTableB.Items.Add(table.Rows[i][0].ToString());
            }
        }

        //Huy bo
        public void btnCancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //Ham load cac mon ban A -- ban chuyen
        public void cbbTableA_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //Dieu kien dung
                DataProvider provider = new DataProvider();
                datatableA = provider.loadFoodTable(cbbTableA.Text);
                dgvTableA.DataSource = datatableA;
                txtTotalA.Text = datatableA.Rows[0][2].ToString();
                conditionA = true;
            }
            catch
            {
                //Dieu kien sai
                MessageBox.Show("Không thể chọn bàn này!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                conditionA = false;
            }
        }
        //Ham load cac mon ban B --ban den
        public void cbbTableB_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //DK dung
                DataProvider provider = new DataProvider();
                datatableB = provider.loadFoodTable(cbbTableB.Text);
                dgvTableB.DataSource = datatableB;
                txtTotalB.Text = datatableB.Rows[0][2].ToString();
                conditionB = true;
            }
            catch
            {
                //DK sai
                MessageBox.Show("Không thể chọn bàn này!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                conditionB = false;
            }
        }

        public void btnAccept_Click(object sender, EventArgs e)
        {
            //Kiem tra dieu kien truoc
            if (cbbTableA.Text != cbbTableB.Text && conditionA == true && conditionB == true)
            {
                DialogResult ms = MessageBox.Show("Bạn có muốn gộp bàn " + cbbTableA.Text + " vào bàn " + cbbTableB.Text + " không?", "Xác nhận", MessageBoxButtons.YesNoCancel, MessageBoxIcon.None);
                if (ms == DialogResult.Yes)
                {
                    //gop ban
                    plussTable();
                    MessageBox.Show("Đã chuyển bàn gộp thành công ", "Hoàn tất", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else if (ms == DialogResult.No)
                {
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Gộp bàn không thành công!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Ham gộp ban
        public void plussTable()
        {
            //Gan Food cho ban den
            for (int i = 0; i < datatableA.Rows.Count; i++)
            {
                DataProvider provider = new DataProvider();
                provider.move_food(cbbTableB.Text, datatableA.Rows[0][0].ToString(), Int16.Parse(datatableA.Rows[0][1].ToString()));
            }

            //Xoa Bill va Table cu + Gan Data cho ban moi
            DataProvider providerD = new DataProvider();
            providerD.GopTable(cbbTableA.Text, cbbTableB.Text, float.Parse(txtTotalA.Text));
        }
    }
}