using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using WMPLib;

namespace project
{
    public partial class frmMain : Form
    {
       
        //username va password
        private string username;
        private string password;
        //Bill
        string strBill;
        public Panel GetPnlTable()
        {
            return pnlTable;
        }
        private string selectedCategory;
        private string selectedFoodName;
        private string selectedFoodPrice;

        // Phương thức chọn một category
        public void SelectCategory(string categoryName)
        {
            selectedCategory = categoryName;
        }

        // Phương thức chọn một food trong category đã chọn trước đó
        public void SelectFood(string foodName, string foodPrice)
        {
            selectedFoodName = foodName;
            selectedFoodPrice = foodPrice;
        }

        // Phương thức lấy thông tin về food được chọn
        public (string foodName, string foodPrice) GetSelectedFoodInfo()
        {
            return (selectedFoodName, selectedFoodPrice);
        }
        public void SetTableStatus(string status)
        {
            txtSTT.Text = status;
        }
        public void SetTableName(string name)
        {
            txtNameTable.Text = name;
        }
        public string GetTotalAmount()
        {
            return txtTotal.Text;
        }

        public Label TxtNameTable => txtNameTable; // Modify the access modifier to public
        public PrintDialog GetPrintDialog()
        {
            return printDialog1;
        }

        public string messageBoxShownMessage;

        public string GetSTT()
        {
            return txtSTT.Text;
        }

        public void SetSTT(string value)
        {
            txtSTT.Text = value;
        }
        public frmMain()
        {
            InitializeComponent();
            wmpMedia = new AxWMPLib.AxWindowsMediaPlayer();
            //Load nhanh danh sach ban va thuc don
            loaddataTable();
            loaddataCategory();
        }
      
        public frmMain(string user, string name, string pass, string type) : this()
        {
            username = user;
            password = pass;
            lblName.Text = name;
            if (type == "ADMIN")
            {
                lblName.ForeColor = ColorTranslator.FromHtml("red");
                tmiAdmin.Visible = true;
            }
        }

        //Su kien thoat from
        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn thoát?", "Thoát", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != System.Windows.Forms.DialogResult.OK)
            {
                e.Cancel = true;
            }
        }

        //Ham Load TABLE 
        public void loaddataTable()
        {
            try
            {
                pnlTable.Controls.Clear();
                DataProvider provider = new DataProvider();
                DataTable table = provider.loadTableF();
                int x = 10;
                int y = 10;
                for (int i = 0; i < table.Rows.Count; i++)
                {                   
                    Button btn = new Button()
                    {
                        Name = "btnTable" + i,
                        Text = table.Rows[i][0].ToString(), //ten ban
                        Tag = table.Rows[i][2].ToString(), //tong tien
                        Width = 100,
                        Height = 50,
                        Location = new Point(x, y),
                    };
                    //Set trang thai ban
                    if (table.Rows[i][1].ToString() == "TRONG")
                    {
                        btn.BackColor = ColorTranslator.FromHtml("snow");
                        //Ban Trong contextmenu 2
                        btn.ContextMenuStrip = cmnSubTable2;
                    }
                    else if (table.Rows[i][1].ToString() == "ONLINE")
                    {
                        btn.BackColor = ColorTranslator.FromHtml("lime");
                        //Ban Trong contextmenu full option
                        btn.ContextMenuStrip = cmnSubTable;
                    }
                    else if (table.Rows[i][1].ToString() == "DATTRUOC")
                    {
                        btn.BackColor = ColorTranslator.FromHtml("red");
                        //Ban Trong contextmenu khoa
                        btn.ContextMenuStrip = cmnSubTable3;
                    }
                    //Xu ly vi tri button, rat cong phu :)
                    if (x < pnlTable.Width - 220)
                    {
                        x += 110;
                    }
                    else
                    {
                        x = 10;
                        y += 60;
                    }
                    btn.MouseClick += new MouseEventHandler(btnTable_MouseClick);
                    btn.MouseHover += new EventHandler(btnTable_MouseHover);
                    pnlTable.Controls.Add(btn);
                }
            }
            catch
            {
                MessageBox.Show("Không thể tải bàn!", "Lỗi...");
            }
        }

        //Ham load BILL
        public void loaddataBill()
        {
            try
            {
                //Don rac
                pnlBill.Controls.Clear();
                strBill = "";
                DataProvider provider = new DataProvider();
                DataTable table = provider.loadBillInfo(txtNameTable.Text);
                //Load thong tin cac mon trong bill 
                int y = 10;
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    //Thêm vào bill
                    strBill += (i + 1) + ".     " + table.Rows[i][2].ToString() + "  X  " + table.Rows[i][3].ToString()+"\n";
                    Label lbl = new Label()
                    {
                        Name = "btnFB" + i,
                        //in ra man hinh tung mon nhu vay no moi dep :)
                        Text = (i + 1) + ".     " + table.Rows[i][2].ToString() + "  X  " + table.Rows[i][3].ToString(),
                        Width = pnlBill.Width - 20,
                        Height = 20,
                        Location = new Point(5, y)
                    };
                    y += 25;
                    pnlBill.Controls.Add(lbl);
                }
            }
            catch 
            {
            }
        }
        
        //Ham Load CATEGORY
        public void loaddataCategory()
        {
            try
            {
                //Don rac
                pnlCategory.Controls.Clear();
                DataProvider provider = new DataProvider();
                DataTable table = provider.loadCategory();
                int x = 10;
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    Button btn = new Button()
                    {
                        Name = "btnFood" + i,
                        Text = table.Rows[i][0].ToString(), //name category
                        Width = 120,
                        Height = pnlCategory.Height - 40,
                        Location = new Point(x, pnlCategory.Location.Y - 20),
                        BackColor = ColorTranslator.FromHtml("Snow"),
                    };
                    x += 130;
                    pnlCategory.Controls.Add(btn);
                    btn.Click += new EventHandler(btnCategory_Click);
                }
                //san tien mo luon 1 food dau tien
                loaddataFood(table.Rows[0][0].ToString());
            }
            catch
            {
            }
        }

        //Ham Load Food
        public void loaddataFood(string nameCategory)
        {
            try
            {
                //Don rac
                pnlFood.Controls.Clear();
                DataProvider provider = new DataProvider();
                DataTable table = provider.loadFood(nameCategory);
                txtNameFood.Text = table.Rows[0][0].ToString(); //ten cua thang food hien tai
                txtPriceFood.Text = table.Rows[0][2].ToString();  //gia cua thang food hien tai
                int y = 10;
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    Button btn = new Button()
                    {
                        Name = "btnFood" + i,
                        Text = table.Rows[i][0].ToString(), //name foods
                        Tag = table.Rows[i][2].ToString(), //Price food
                        Width = pnlFood.Width - 40,
                        Height = 50,
                        Location = new Point(pnlFood.Location.X, y),
                        BackColor = ColorTranslator.FromHtml("Snow"),
                    };
                    y += 60;
                    btn.Click += new EventHandler(btnFB_Click);
                    pnlFood.Controls.Add(btn);
                }
            }
            catch
            {
            }
        }

        private void btnTable_MouseHover(object sender, EventArgs e)
        {
            ClickTable(sender, e);
        }

        //Su kien Mouseclick vao btnTABLE
        private void btnTable_MouseClick(object sender, EventArgs e)
        {
            ClickTable(sender, e);
        }
        public void ClickTable(object sender, EventArgs e)
        {
            //tra ve trang thai ban theo mau sac cua btnTable
            if (((Button)sender).BackColor.ToString() == "Color [Snow]")
            {
                txtSTT.Text = "TRONG";
            }
            else if (((Button)sender).BackColor.ToString() == "Color [Lime]")
            {
                txtSTT.Text = "ONLINE";
            }
            else if (((Button)sender).BackColor.ToString() == "Color [Red]")
            {
                txtSTT.Text = "DATTRUOC";
            }
            //tra ve ten ban
            txtNameTable.Text = ((Button)sender).Text;
            //Tra ve tong tien
            txtTotal.Text = ((Button)sender).Tag.ToString();
            loaddataBill();
        }

        //Su kien click btnCATEGORY
        public void btnCategory_Click(object sender, EventArgs e)
        {
            string nameCate = ((Button)sender).Text;
            //Load mon theo yeu cau click cua Category
            loaddataFood(nameCate);
        }

        //Su kiem click btnFood
        public void btnFB_Click(object sender, EventArgs e)
        {
            //Gan text button mon an => text groupbox . cho no dep
            txtNameFood.Text = ((Button)sender).Text;
            txtPriceFood.Text = ((Button)sender).Tag.ToString();
        }

        //Ham xu ly Category
        public void tmiCategory_Click(object sender, EventArgs e)
        {
            try
            {
                frmAdCategory frm = new frmAdCategory();
                frm.ShowDialog();
                this.Show();
                loaddataCategory();
            }
            catch{}
        }

        //di den quan ly food
        public void tmiFood_Click(object sender, EventArgs e)
        {
            try
            {
                frmAdFood frm = new frmAdFood();
                frm.ShowDialog();
                this.Show();
                loaddataCategory();
            }
            catch{}
        }

        //di den quan ly table
        public void tmiTable_Click(object sender, EventArgs e)
        {
            try
            {
                frmAdTables frm = new frmAdTables();
                frm.ShowDialog();
                this.Show();
                loaddataTable();
            }
            catch{}
        }

        //di den thang Account
        public void tmiAccount_Click(object sender, EventArgs e)
        {
            try
            {
                frmAdAccount frm = new frmAdAccount();
                frm.ShowDialog();
                this.Show();
            }
            catch{}
        }

        //Neu kich thuoc table thay doi thi load lai table cho phu hop
        private void gpbTable_SizeChanged(object sender, EventArgs e)
        {
            loaddataTable();
        }


        //Su kien nhan button them mon
        private void btnAddFood_Click(object sender, EventArgs e)
        {
            AddFood();
        }
        private void AddFood()
        {
            try
            {
                if (txtSTT.Text == "ONLINE")
                {
                    frmAddFood addF = new frmAddFood(txtNameTable.Text, txtNameFood.Text, txtSTT.Text);
                    addF.ShowDialog();
                    this.Show();
                    loaddataTable();
                    loaddataBill();
                }
                else if (txtSTT.Text == "DATTRUOC")
                {
                    MessageBox.Show("Bàn đã được đặt", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (txtSTT.Text == "TRONG")
                {
                    DialogResult result = MessageBox.Show("Bàn này đang trống. Mở bàn nhé?", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        frmAddFood addF = new frmAddFood(txtNameTable.Text, txtNameFood.Text, txtSTT.Text);
                        addF.ShowDialog();
                        this.Show();
                        loaddataTable();
                        loaddataBill();
                    }
                }
            }
            catch { }
        }


        //Nhan nut thanh toan
        public void btnPay_Click(object sender, EventArgs e)
        {
            PayFood();
        }
        public void PayFood()
        {
            try
            {
                if (txtSTT.Text == "ONLINE")
                {
                    frmPay addF = new frmPay(txtNameTable.Text);
                    addF.ShowDialog();
                    this.Show();
                    loaddataTable();
                    loaddataBill();
                }
                else if (txtSTT.Text == "DATTRUOC")
                {
                    MessageBox.Show("Bàn đã được đặt", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (txtSTT.Text == "TRONG")
                {
                    MessageBox.Show("Bàn này đang trống", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch { }
        }

        //Nut chuyen ban
        private void btnReplaceTable_Click(object sender, EventArgs e)
        {
            ReplaceTable();
        }
        public void ReplaceTable()
        {
            try
            {
                if (txtSTT.Text == "ONLINE")
                {
                    ReplaceTable addF = new ReplaceTable(txtNameTable.Text);
                    addF.ShowDialog();
                    this.Show();
                    loaddataTable();
                    loaddataBill();
                }
                else if (txtSTT.Text == "DATTRUOC")
                {
                    MessageBox.Show("Bàn đã được đặt", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (txtSTT.Text == "TRONG")
                {
                    MessageBox.Show("Bàn này đang trống", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch { }
        }

        //Gop ban
       private void btnPlusTable_Click(object sender, EventArgs e)
        {
            PlusTable();
        }
        public void PlusTable()
        {
            try
            {
                if (txtSTT.Text == "ONLINE")
                {
                    PlusTable addF = new PlusTable(txtNameTable.Text);
                    addF.ShowDialog();
                    this.Show();
                    loaddataTable();
                    loaddataBill();
                }
                else if (txtSTT.Text == "DATTRUOC")
                {
                    MessageBox.Show("Bàn đã được đặt", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (txtSTT.Text == "TRONG")
                {
                    MessageBox.Show("Bàn này đang trống", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch { }
        }

        //Tra mon
        private void btnReturn_Click(object sender, EventArgs e)
        {
            ReturnFood();
        }
        public void ReturnFood()
        {
            try
            {
                if (txtSTT.Text == "ONLINE")
                {
                    ReFood addF = new ReFood(txtNameTable.Text);
                    addF.ShowDialog();
                    this.Show();
                    loaddataTable();
                    loaddataBill();
                }
                else if (txtSTT.Text == "DATTRUOC")
                {
                    MessageBox.Show("Bàn đã được đặt", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (txtSTT.Text == "TRONG")
                {
                    MessageBox.Show("Bàn này đang trống", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch { }
        }

        //Khoa man hinh
        public void btnBlock_Click(object sender, EventArgs e)
        {
            Block addF = new Block(username, lblName.Text, password);
            addF.ShowDialog();
            this.Show();
        }

        //In hoa don
        public void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                printDialog1.Document = printDocument1;
                if (printDialog1.ShowDialog() == DialogResult.OK)
                {
                    printDocument1.Print();
                }
            }
            catch
            {
                MessageBox.Show("Không thể in hóa đơn!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //PrintDocument
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            //Chuoi hoa don
            string HoaDon = "";
            HoaDon += "\n" + txtNameMan.Text + "\n";
            HoaDon += "\n" + txtAdress.Text + "\n\n\n";
            HoaDon += "\n" + "           HÓA ĐƠN " + txtNameTable.Text + "        \n\n\n";
            HoaDon += strBill;
            HoaDon += "\n\n\nThời gian: " + datetime.Value.ToShortTimeString() + ". " + datetime.Value.ToShortDateString() + "\n";
            HoaDon += "\nTổng cộng: " + txtTotal.Text + " VNĐ\n";
            e.Graphics.DrawString(HoaDon, new Font("Arial", 15, FontStyle.Bold), Brushes.Black, 100, 200);
        }

        //Sua tai khoan
        public void tmiChange_Click(object sender, EventArgs e)
        {
            try
            {
                ChangePersional addF = new ChangePersional(username, lblName.Text, password);
                addF.ShowDialog();
                this.Show();
                loaddataTable();
                loaddataBill();
            }
            catch
            {
                MessageBox.Show("Không thể thay đổi thông tin!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        //Logout
        public void tmiLogout_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        //load context menu ban online
        public void cmnSubTable_Opening(object sender, CancelEventArgs e)
        {
        }
        //menu context SubTable
        private void tsmThemMon_Click(object sender, EventArgs e)
        {
            AddFood();
        }
        private void tsmTraMon_Click(object sender, EventArgs e)
        {
            ReturnFood();
        }
        private void tsmThanhToan_Click(object sender, EventArgs e)
        {
            PayFood();
        }
        private void tsmChuyenBan_Click(object sender, EventArgs e)
        {
            ReplaceTable();
        }
        public void tsmGopBan_Click(object sender, EventArgs e)
        {
            PlusTable();
        }


        //Context Menu ban trong
        private void cmnSubTable2_Opening(object sender, CancelEventArgs e)
        {
        }
        private void tmsThemMon2_Click(object sender, EventArgs e)
        {
            AddFood();
        }
        //Dat ban
        private void tsmDatBan_Click(object sender, EventArgs e)
        {
            try
            {
                DataProvider provider = new DataProvider();
                provider.Datban("DATTRUOC", txtNameTable.Text);
                loaddataTable();
            }
            catch { }
        }


        //Context Menu ban trong
        public void cmnSubTable3_Opening(object sender, CancelEventArgs e)
        {
        }
        //Mo ban
        private void tsmMoBan_Click(object sender, EventArgs e)
        {
            try
            {
                DataProvider provider = new DataProvider();
                provider.Datban("TRONG",txtNameTable.Text);
                loaddataTable();
            }
            catch { }
        }

        public void tmiAdmin_Click(object sender, EventArgs e)
        {

        }
        public bool IsMessageBoxShown { get; set; }

        //     Media
        string[] filenames, filepaths;
        //public bool IsMessageBoxShown;

        public void btnAddMedia_Click(object sender, EventArgs e)
        {
            try
            {
                if (ofdMedia.ShowDialog() == DialogResult.OK)
                {
                    //Mang file name se hung tat ca ten
                    filenames = ofdMedia.SafeFileNames;
                    //Mang file path se hung tat ca duong dan file
                    filepaths = ofdMedia.FileNames;
                    //Them file name vao listview
                    lbMedia.Items.Clear();
                    foreach (String file in filenames)
                    {
                        lbMedia.Items.Add(file);
                    }
                }
            }
            catch { }
        }
        private void lbMedia_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                wmpMedia.URL = filepaths[lbMedia.SelectedIndex];
            }
            catch
            { }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {

        }

        private void pnlCategory_Paint(object sender, PaintEventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void txtNameMan_Click(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void pnlTable_Paint(object sender, PaintEventArgs e)
        {

        }

        private void gpbTable_Enter(object sender, EventArgs e)
        {

        }

        private void datetime_ValueChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void txtAdress_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
        
        private void pictureBox1_Click_1(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        //Dong mo danh sach nhac
        private void btnMedia_Click(object sender, EventArgs e)
        {
            try
            {
                if (lbMedia.Visible == true)
                {
                    lbMedia.Visible = false;
                }
                else
                {
                    lbMedia.Visible = true;
                }
            }
            catch { }
        }

    }
}
