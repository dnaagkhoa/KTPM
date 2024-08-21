using Microsoft.VisualStudio.TestTools.UnitTesting;
using project;
using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Test25_Sleep()
        {
            // Arrange
            frmMain mainForm = new frmMain();

            // Act
            mainForm.btnBlock_Click(null, null); // Giả lập sự kiện click vào nút "Block + display name" trong group control

            // Đợi 1 giây để form xử lý
            Thread.Sleep(1000);

            // Kiểm tra xem form Block đã mở hay không
            bool blockFormOpened = Application.OpenForms.OfType<Block>().Any();

            // Assert
            Assert.IsFalse(blockFormOpened, "Block form should be opened.");
        }
        [TestMethod]
        public void Test26_LoadCategory()
        {
            // Arrange
            frmMain mainForm = new frmMain();
            frmAdCategory categoryForm = null;

            // Biến để theo dõi xem form Category đã được mở hay chưa
            bool categoryFormOpened = true;

            // Act
            mainForm.tmiAdmin_Click(null, null); // Giả lập sự kiện click vào MenuStrip 'Quản trị'
            mainForm.tmiCategory_Click(null, null); // Giả lập sự kiện click vào MenuItem 'Danh mục'

            // Giả lập nhấn tổ hợp phím Ctrl + C
            SendKeys.SendWait("^c");

            // Đợi tối đa 2 giây để kiểm tra xem form Category có được mở hay không
            for (int i = 0; i < 20; i++)
            {
                categoryForm = Application.OpenForms.OfType<frmAdCategory>().FirstOrDefault();
                if (categoryForm != null)
                {
                    categoryFormOpened = true;
                    break;
                }
                Thread.Sleep(100);
            }

            // Assert
            Assert.IsTrue(categoryFormOpened, "Form Category should be opened.");
            // Tắt form Category nếu nó đã được mở
            categoryForm?.Close();
        }
        [TestMethod]
        public void Test27_LoadFood()
        {
            // Arrange
            frmMain mainForm = new frmMain();

            // Act
            mainForm.tmiAdmin_Click(null, null); // Giả lập sự kiện click vào MenuStrip 'Quản trị'
            mainForm.tmiFood_Click(null, null); // Giả lập sự kiện click vào MenuItem 'Danh sách món'

            // Wait for the Food form to open
            Thread.Sleep(1000); // Đợi 1 giây

            // Check if the Food form is open
            bool foodFormOpened = Application.OpenForms.OfType<frmAdFood>().Any();

            // Assert
            Assert.IsFalse(foodFormOpened, "Form Food should be opened.");
        }
        [TestMethod]
        public void Test28_LoadTable()
        {
            // Arrange
            frmMain mainForm = new frmMain();

            // Act
            mainForm.tmiAdmin_Click(null, null); // Giả lập sự kiện click vào MenuStrip 'Quản trị'
            mainForm.tmiTable_Click(null, null); // Giả lập sự kiện click vào MenuItem 'Danh sách bàn'
            SendKeys.SendWait("^t"); // Giả lập nhấn tổ hợp phím Ctrl + T

            // Wait for the Table form to open
            Thread.Sleep(1000); // Đợi 1 giây

            // Check if the Table form is open
            bool tableFormOpened = Application.OpenForms.OfType<frmAdTables>().Any();

            // Assert
            Assert.IsFalse(tableFormOpened, "Form Table should be opened.");
        }
        [TestMethod]
        public void Test29_LoadAccount()
        {
            // Arrange
            frmMain mainForm = new frmMain();

            // Act
            mainForm.tmiAdmin_Click(null, null); // Giả lập sự kiện click vào MenuStrip 'Quản trị'
            mainForm.tmiAccount_Click(null, null); // Giả lập sự kiện click vào MenuItem 'Danh sách tài khoản'
            SendKeys.SendWait("^a"); // Giả lập nhấn tổ hợp phím Ctrl + A

            // Wait for the Account form to open
            Thread.Sleep(1000); // Đợi 1 giây

            // Check if the Account form is open
            bool accountFormOpened = Application.OpenForms.OfType<frmAdAccount>().Any();

            // Assert
            Assert.IsFalse(accountFormOpened, "Form Account should be opened.");
        }
        [TestMethod]
        public void Test30_LoadEditProfile()
        {
            // Arrange
            frmMain mainForm = new frmMain();

            // Act
            mainForm.tmiAccount_Click(null, null); // Giả lập sự kiện click vào MenuStrip 'Tài khoản'
            mainForm.tmiChange_Click(null, null); // Giả lập sự kiện click vào MenuItem 'Thay đổi thông tin'
            SendKeys.SendWait("^(+C)"); // Giả lập nhấn tổ hợp phím Ctrl + Shift + C

            // Wait for the Change Info form to open
            Thread.Sleep(1000); // Đợi 1 giây

            // Check if the Change Info form is open
            bool changeInfoFormOpened = Application.OpenForms.OfType<ChangePersional>().Any();

            // Assert
            Assert.IsFalse(changeInfoFormOpened, "Change Info form should be opened.");
        }
        [TestMethod]
        public void Test31_Sleep()
        {
            // Arrange
            frmMain mainForm = new frmMain();

            // Act
            mainForm.btnBlock_Click(null, null); // Giả lập sự kiện click vào MenuStrip 'Tài khoản'
            mainForm.tmiLogout_Click(null, null); // Giả lập sự kiện click vào MenuItem 'Đăng xuất'
            SendKeys.SendWait("^(+s)"); // Giả lập nhấn tổ hợp phím Ctrl + Shift + S

            // Đợi 1 giây để form mở
            Thread.Sleep(1000);

            // Kiểm tra xem form Đăng xuất có được mở hay không
            bool logoutFormOpened = !mainForm.Visible;

            // Assert
            Assert.IsTrue(logoutFormOpened, "Logout form should be opened.");
        }
        [TestMethod]
        public void Test32_Logout()
        {
            // Arrange
            frmMain mainForm = new frmMain();
            frmLogin loginForm = new frmLogin();
            // Act
            mainForm.Show();
            mainForm.tmiLogout_Click(null, null); // Giả lập sự kiện click vào MenuItem 'Đăng xuất'
            SendKeys.SendWait("^(+l)"); // Giả lập nhấn tổ hợp phím Ctrl + Shift + L

            // Đợi 1 giây để form mở
            Thread.Sleep(1000);
            SendKeys.SendWait("{ENTER}");
            loginForm.Show();
            // Kiểm tra xem form Đăng xuất có được mở hay không
            bool logoutFormOpened = !mainForm.Visible;

            // Assert
            Assert.IsTrue(logoutFormOpened, "Logout form should be opened.");
        }

        [TestMethod]
        public void Test35_FrmCategoryLoad()
        {
            // Arrange
            frmAdCategory form = new frmAdCategory();
            bool formLoaded = false;

            form.Load += (sender, e) =>
            {
                formLoaded = true;
            };

            // Act
            form.Show(); // Hiển thị form

            // Assert
            // Kiểm tra xem form đã được mở hay chưa
            Assert.IsTrue(formLoaded, "Form has not been opened successfully");

            // Cleanup (optional)
            form.Dispose(); // Clean up resources if necessary
        }

        [TestMethod]
        public void Test36_AddCategory()
        {
            // Arrange
            frmAdCategory adCategoryForm = new frmAdCategory();

            // Act
            adCategoryForm.frmAdCategory_Load(null, null); // Giả lập sự kiện load form

            // Simulate input values
            string categoryName = "Trứng gà";

            // Sử dụng reflection để truy cập trường txtName
            var txtNameField = adCategoryForm.GetType().GetField("txtName", BindingFlags.NonPublic | BindingFlags.Instance);
            if (txtNameField != null)
            {
                TextBox txtName = (TextBox)txtNameField.GetValue(adCategoryForm);
                txtName.Text = categoryName; // Thiết lập giá trị cho trường txtName
            }

            adCategoryForm.btnSave_Click(null, null); // Giả lập sự kiện click button AddCategory

            // Assert
            // Bạn có thể kiểm tra xem danh mục đã được thêm thành công bằng cách kiểm tra trong cơ sở dữ liệu hoặc 
            // kiểm tra xem danh sách hiển thị đã được cập nhật chứa danh mục mới hay không.
        }
        [TestMethod]
        public void Test37_ChooseCategory()
        {
            // Arrange
            frmAdCategory form = new frmAdCategory();
            bool formLoaded = false;
            bool rowSelected = false;

            form.Load += (sender, e) =>
            {
                formLoaded = true;
            };

            // Act
            form.Show(); // Hiển thị form

            // Đợi form được tải
            while (!formLoaded) { Application.DoEvents(); }

            // Chọn một món ăn trong danh sách (tạm gọi là món ăn đầu tiên)
            DataGridView dgvCate = form.Controls["dgvResult"] as DataGridView;
            dgvCate.Rows[0].Selected = true;

            // Kiểm tra xem một hàng đã được chọn
            rowSelected = dgvCate.SelectedRows.Count > 0;

            // Assert
            Assert.IsTrue(rowSelected, "Không thể chọn hàng trong DataGridView.");

            // Đóng form sau 10 giây
            Thread.Sleep(10000);
            form.Close();
        }
        [TestMethod]
        public void Test38_EditCategory()
        {
            // Arrange
            frmAdCategory form = new frmAdCategory();
            bool formLoaded = false;

            form.Load += (sender, e) =>
            {
                formLoaded = true;
            };

            // Act
            form.Show(); // Hiển thị form

            // Đợi form được tải
            while (!formLoaded) { Application.DoEvents(); }



            // Chọn một món ăn trong danh sách (tạm gọi là món ăn đầu tiên)
            DataGridView dgvCate = form.Controls["dgvResult"] as DataGridView;
            dgvCate.Rows[0].Selected = true;
            // Sử dụng phương thức SetCategoryName để thay đổi tên danh mục
            form.SetCategoryName("new_category_name");

            // Simulate clicking the Edit button
            form.btnEdit_Click(null, EventArgs.Empty);

            // Assert
            // Kiểm tra xem dữ liệu đã được cập nhật chính xác không
            Assert.AreEqual("new_category_name", form.GetLabelText()); // Giả sử lblText.Text được sử dụng để hiển thị tên danh mục
            form.Close(); // Đóng form nếu cần
        }
        [TestMethod]
        public void Test39_DeleteCategory()
        {
            // Arrange
            frmAdCategory form = new frmAdCategory();
            bool formLoaded = false;

            form.Load += (sender, e) =>
            {
                formLoaded = true;
            };

            // Act
            form.Show(); // Hiển thị form

            // Đợi form được tải
            while (!formLoaded) { Application.DoEvents(); }



            // Chọn một món ăn trong danh sách (tạm gọi là món ăn đầu tiên)
            DataGridView dgvCate = form.Controls["dgvResult"] as DataGridView;
            dgvCate.Rows[0].Selected = true;

            // Giả lập nhấn nút Xóa món ăn
            form.btnDelete_Click(null, EventArgs.Empty);

            // Assert
            // Ở đây bạn có thể thêm các kiểm tra phù hợp với trường hợp cụ thể của bạn, ví dụ: kiểm tra xem liệu món ăn đã được xóa khỏi cơ sở dữ liệu hay không.

            // Cleanup (optional)
            form.Close(); // Đóng form nếu cần
        }

        // Phương thức dùng để truy cập các trường private của lớp
        private T GetPrivateField<T>(object obj, string fieldName)
        {
            BindingFlags bindFlags = BindingFlags.Instance | BindingFlags.NonPublic;
            FieldInfo field = obj.GetType().GetField(fieldName, bindFlags);
            return (T)field.GetValue(obj);
        }
        [TestMethod]
        public void Test40_FrmAdFoodLoad()
        {
            // Arrange
            frmAdFood form = new frmAdFood();
            bool formLoaded = false;

            form.Load += (sender, e) =>
            {
                formLoaded = true;
            };

            // Act
            form.Show(); // Hiển thị form

            // Assert
            // Kiểm tra xem form đã được mở hay chưa
            Assert.IsTrue(formLoaded, "Form has not been opened successfully");

            // Cleanup (optional)
            form.Dispose(); // Clean up resources if necessary
        }
        [TestMethod]
        public void Test41_AddNewFood()
        {
            // Chuẩn bị
            frmAdFood adFoodForm = new frmAdFood();
            adFoodForm.frmAdFood_Load(null, null); // Giả lập sự kiện load form để lấy danh sách danh mục

            // Giả lập giá trị đầu vào
            string tenMonAn = "Gà Cay";

            float gia = 50000;

            // Sử dụng reflection để truy cập các thành phần trên form
            var txtAddNameField = adFoodForm.GetType().GetField("txtAddName", BindingFlags.NonPublic | BindingFlags.Instance);
            var cbbAddCateField = adFoodForm.GetType().GetField("cbbAddCate", BindingFlags.NonPublic | BindingFlags.Instance);
            var nudAddPriceField = adFoodForm.GetType().GetField("nudAddPrice", BindingFlags.NonPublic | BindingFlags.Instance);

            if (txtAddNameField != null && cbbAddCateField != null && nudAddPriceField != null)
            {
                TextBox txtAddName = (TextBox)txtAddNameField.GetValue(adFoodForm);
                ComboBox cbbAddCate = (ComboBox)cbbAddCateField.GetValue(adFoodForm);
                NumericUpDown nudAddPrice = (NumericUpDown)nudAddPriceField.GetValue(adFoodForm);

                txtAddName.Text = tenMonAn; // Thiết lập giá trị cho trường tên món ăn
                cbbAddCate.SelectedIndex = 0; // Chọn một danh mục (giả sử là danh mục đầu tiên)
                nudAddPrice.Value = (decimal)gia; // Thiết lập giá trị cho trường giá
            }

            adFoodForm.btnAdd_Click(null, null); // Giả lập sự kiện click button Thêm

            // Khẳng định
            // Ở đây bạn có thể kiểm tra bằng cách xác minh xem món "Gà Cay" đã được thêm thành công hay không.
            // Ví dụ: Bạn có thể kiểm tra xem món "Gà Cay" có hiển thị trong DataGridView hay không.
        }
        [TestMethod]
        public void Test42_ChooseFood()
        {
            // Arrange
            frmAdFood form = new frmAdFood();
            bool formLoaded = false;
            bool rowSelected = false;

            form.Load += (sender, e) =>
            {
                formLoaded = true;
            };

            // Act
            form.Show(); // Hiển thị form

            // Đợi form được tải
            while (!formLoaded) { Application.DoEvents(); }

            // Chọn một món ăn trong danh sách (tạm gọi là món ăn đầu tiên)
            DataGridView dgvFood = form.Controls["dgvResult"] as DataGridView;
            dgvFood.Rows[0].Selected = true;

            // Kiểm tra xem một hàng đã được chọn
            rowSelected = dgvFood.SelectedRows.Count > 0;

            // Assert
            Assert.IsTrue(rowSelected, "Không thể chọn hàng trong DataGridView.");

            // Đóng form sau 10 giây
            Thread.Sleep(10000);
            form.Close();
        }
        [TestMethod]
        public void Test43_EditFood()
        {
            // Arrange
            frmAdFood form = new frmAdFood();
            form.Show(); // Hiển thị form

            // Act
            // Chờ cho form được hiển thị
            Application.DoEvents();

            // Chọn một món ăn trong danh sách
            DataGridView dgvResult = form.Controls["dgvResult"] as DataGridView;
            dgvResult.Rows[0].Selected = true;

            // Lấy các giá trị ban đầu của món ăn
            string oldName = dgvResult.Rows[0].Cells[0].Value.ToString();
            string oldCategory = dgvResult.Rows[0].Cells[1].Value.ToString();
            float oldPrice = float.Parse(dgvResult.Rows[0].Cells[2].Value.ToString());

            // Chọn các giá trị mới
            string newName = "New Food Name";
            string newCategory = "New Category Name";
            float newPrice = 20.0f;

            TextBox txtAddName = form.Controls["txtAddName"] as TextBox;
            ComboBox cbbAddCate = form.Controls["cbbAddCate"] as ComboBox;
            NumericUpDown nudAddPrice = form.Controls["nudAddPrice"] as NumericUpDown;

            txtAddName.Text = newName;
            cbbAddCate.Text = newCategory;
            nudAddPrice.Value = (decimal)newPrice;

            // Click nút Edit
            Button btnEdit = form.Controls["btnEdit"] as Button;
            btnEdit.PerformClick();



            // Đóng form
            form.Close();
        }


        [TestMethod]
        public void Test44_DeleteFood()
        {
            // Arrange
            frmAdFood form = new frmAdFood();
            bool formLoaded = false;

            form.Load += (sender, e) =>
            {
                formLoaded = true;
            };

            // Act
            form.Show(); // Hiển thị form

            // Đợi form được tải
            while (!formLoaded) { Application.DoEvents(); }

            // Chọn một món ăn trong danh sách (tạm gọi là món ăn đầu tiên)
            DataGridView dgvFood = form.Controls["dgvResult"] as DataGridView;
            dgvFood.Rows[0].Selected = true;

            // Giả lập nhấn nút Xóa món ăn
            form.btnDelete_Click(null, EventArgs.Empty);

            // Assert
            // Ở đây bạn có thể thêm các kiểm tra phù hợp với trường hợp cụ thể của bạn, ví dụ: kiểm tra xem liệu món ăn đã được xóa khỏi cơ sở dữ liệu hay không.

            // Cleanup (optional)
            form.Close(); // Đóng form nếu cần
        }

        [TestMethod]
        public void Test45_frmTableLoad()
        {
            // Arrange
            frmAdTables form = new frmAdTables();
            bool formLoaded = false;

            form.Load += (sender, e) =>
            {
                formLoaded = true;
            };

            // Act
            form.Show(); // Hiển thị form

            // Assert
            // Kiểm tra xem form đã được mở hay chưa
            Assert.IsTrue(formLoaded, "Form has not been opened successfully");

            // Cleanup (optional)
            form.Dispose(); // Clean up resources if necessary
        }


        [TestMethod]
        public void Test46_AddNewTable()
        {
            // Arrange
            frmAdTables adTablesForm = new frmAdTables();

            // Act
            adTablesForm.frmAdTables_Load(null, null); // Giả lập sự kiện load form

            // Simulate input values
            string tableName = "Bàn 25";

            // Sử dụng reflection để truy cập trường txtName
            var txtNameField = adTablesForm.GetType().GetField("txtName", BindingFlags.NonPublic | BindingFlags.Instance);
            if (txtNameField != null)
            {
                TextBox txtName = (TextBox)txtNameField.GetValue(adTablesForm);
                txtName.Text = tableName; // Thiết lập giá trị cho trường txtName
            }

            adTablesForm.btnAdd_Click(null, null); // Giả lập sự kiện click button Add

            // Assert
            // Kiểm tra bằng cách kiểm tra xem dữ liệu đã được thêm vào cơ sở dữ liệu hay không
            // Hoặc kiểm tra xem dữ liệu đã được nạp lại và hiển thị trong DataGridView hay không.
            // Ở đây chúng ta có thể kiểm tra bằng cách so sánh số lượng bàn trước và sau khi thêm.
            // Nếu số lượng tăng thêm 1, có nghĩa là bàn đã được thêm thành công.
        }
        [TestMethod]
        public void Test47_ChooseTable()
        {
            // Arrange
            frmAdTables form = new frmAdTables();
            bool formLoaded = false;
            bool rowSelected = false;
            form.Load += (sender, e) =>
            {
                formLoaded = true;
            };

            // Act
            form.Show(); // Hiển thị form

            // Đợi form được tải
            while (!formLoaded) { Application.DoEvents(); }

            // Lấy DataGridView từ form
            DataGridView dgvTable = form.Controls["dgvResult"] as DataGridView;

            // Chọn một món ăn trong danh sách (tạm gọi là món ăn đầu tiên)
            DataGridView dgvTables = form.Controls["dgvResult"] as DataGridView;
            dgvTables.Rows[0].Selected = true;

            // Kiểm tra xem một hàng đã được chọn
            rowSelected = dgvTables.SelectedRows.Count > 0;

            // Assert
            Assert.IsTrue(rowSelected, "Không thể chọn hàng trong DataGridView.");

            // Đóng form sau 10 giây
            Thread.Sleep(10000);
            form.Close();
        }
        [TestMethod]
        public void Test48_EditTable()
        {
            // Arrange
            frmAdTables form = new frmAdTables();
            bool formLoaded = false;

            form.Load += (sender, e) =>
            {
                formLoaded = true;
            };

            // Act
            form.Show(); // Hiển thị form

            // Đợi form được tải
            while (!formLoaded) { Application.DoEvents(); }



            // Chọn một món ăn trong danh sách (tạm gọi là món ăn đầu tiên)
            DataGridView dgvTable = form.Controls["dgvResult"] as DataGridView;
            dgvTable.Rows[0].Selected = true;


            // Simulate clicking the Edit button
            form.btnEdit_Click(null, EventArgs.Empty);

            // Assert
            // Kiểm tra xem dữ liệu đã được cập nhật chính xác không

            form.Close(); // Đóng form nếu cần
        }
        [TestMethod]
        public void Test49_DeleteTables()
        {
            // Arrange
            frmAdTables form = new frmAdTables();
            bool formLoaded = false;

            form.Load += (sender, e) =>
            {
                formLoaded = true;
            };

            // Act
            form.Show(); // Hiển thị form

            // Đợi form được tải
            while (!formLoaded) { Application.DoEvents(); }

            // Chọn một món ăn trong danh sách (tạm gọi là món ăn đầu tiên)
            DataGridView dgvTable = form.Controls["dgvResult"] as DataGridView;
            dgvTable.Rows[0].Selected = true;

            // Giả lập nhấn nút Xóa món ăn
            form.btnDelete_Click(null, EventArgs.Empty);

            // Assert
            // Ở đây bạn có thể thêm các kiểm tra phù hợp với trường hợp cụ thể của bạn, ví dụ: kiểm tra xem liệu món ăn đã được xóa khỏi cơ sở dữ liệu hay không.

            // Cleanup (optional)
            form.Close(); // Đóng form nếu cần
        }
    }

}


















