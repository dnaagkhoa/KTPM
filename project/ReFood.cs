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
    public partial class ReFood : Form
    {
        public string messageShown { get; set; }
        public bool formClosed { get; set; }

        public NumericUpDown CbbCountReF
        {
            get { return cbbCountReF; }
        }

        public ComboBox CbbTable
        {
            get { return cbbTable; }
        }

        // Expose cbbFood as a public property
        public ComboBox CbbFood
        {
            get { return cbbFood; }
        }



        public DataTable datatable;
        public bool condition = true;
        public ReFood()
        {
            InitializeComponent();
        }
        public ReFood(string nameTableFrom)
            : this()
        {
            loadDataTable();
            cbbTable.Text = nameTableFrom;
        }
        //Load len cho nguoi dung chon ban
        public void loadDataTable()
        {
            DataProvider provider = new DataProvider();
            DataTable table = provider.loadTableF();
            for (int i = 0; i < table.Rows.Count; i++)
            {
                cbbTable.Items.Add(table.Rows[i][0].ToString());
            }
        }

        //Thay doi datagirdview
        public void cbbTable_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //Dieu kien dung
                DataProvider provider = new DataProvider();
                datatable = provider.loadTableFoodBill(cbbTable.Text);
                txtTotal.Text = datatable.Rows[0][3].ToString();
                for (int i = 0; i < datatable.Rows.Count; i++)
                {
                    cbbFood.Items.Add(datatable.Rows[i][0].ToString());
                }
            }
            catch
            {
                condition = false;
            }
        }

        public void cbbFood_TextChanged(object sender, EventArgs e)
        {
            cbbCount.Value = Int16.Parse(datatable.Rows[cbbFood.SelectedIndex][1].ToString());
        }

        //Nhan nut va thuc hien giam mon
        public void btnAccept_Click(object sender, EventArgs e)
        {
            if (cbbCountReF.Value <= cbbCount.Value && condition == true && cbbFood.Text != "")
            {
                DialogResult ms = MessageBox.Show("Bạn có muốn bỏ " + cbbCountReF.Text + " món " + cbbFood.Text + " ở bàn " + cbbTable.Text + " không?", "Xác nhận", MessageBoxButtons.YesNoCancel, MessageBoxIcon.None);
                if (ms == DialogResult.Yes)
                {
                    //Bo mon
                    reFood();
                    MessageBox.Show("Đã bỏ món thành công!", "Hoàn tất", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else if (ms == DialogResult.No)
                {
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Không thể giảm món!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        //sua bill
        public void reFood()
        {
            //sua bill
            int count = Int16.Parse((cbbCount.Value - cbbCountReF.Value).ToString());
            float total = float.Parse((float.Parse(txtTotal.Text) - float.Parse(datatable.Rows[cbbFood.SelectedIndex][2].ToString()) * (float)cbbCountReF.Value).ToString());
            DataProvider provider = new DataProvider();
            provider.giammon(cbbTable.Text, cbbFood.Text, count, total);
        }


        //Huy bo
        public void btnCancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}