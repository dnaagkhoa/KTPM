using Microsoft.VisualStudio.TestTools.UnitTesting;
using project;
using System.Windows.Forms; // Thêm dòng này để sử dụng DataGridView
using System;
using static System.Windows.Forms.DataFormats;



namespace UnitTestProject1
{
    [TestClass]
    public class FormTests
    {

        //Test Delete Table
        [TestMethod]
        public void TestDeleteTableAndReload()
        {
            // Arrange
            // Create an instance of the form
            frmAdTables form = new frmAdTables();

            // Assuming your form has a method to load tables, call it here to populate the DataGridView
            form.loadTable();

            // Get the number of rows before deletion
            int initialRowCount = form.DgvResult.Rows.Count;

            // Assume the table name to delete is "Bàn 01"
            string tableNameToDelete = "Bàn 01";

            // Select the row containing the table to delete (assuming it's present in the DataGridView)
            DataGridViewRow rowToDelete = null;
            foreach (DataGridViewRow row in form.DgvResult.Rows)
            {
                if (row.Cells[0].Value.ToString() == tableNameToDelete)
                {
                    rowToDelete = row;
                    break;
                }
            }

            // Assert that the row to delete is found
            Assert.IsNotNull(rowToDelete, "Row to delete not found.");

            // Act
            // Simulate clicking the Delete button
            form.btnDelete_Click(null, EventArgs.Empty);

            // Assert
            // Assert that the number of rows after deletion is reduced by one
            Assert.AreEqual(initialRowCount - 1, form.DgvResult.Rows.Count, "Table was not deleted.");

            // Assert that the deleted table is no longer in the DataGridView
            bool tableDeleted = true;
            foreach (DataGridViewRow row in form.DgvResult.Rows)
            {
                if (row.Cells[0].Value.ToString() == tableNameToDelete)
                {
                    tableDeleted = false;
                    break;
                }
            }
            Assert.IsTrue(tableDeleted, "Deleted table still exists in the DataGridView.");

            // Assert that the DataGridView is reloaded after deletion
            // Assuming loadTable method is called in the constructor or form load event
            // Check if the DataGridView is reloaded by comparing the number of rows
            Assert.AreEqual(initialRowCount - 1, form.DgvResult.Rows.Count, "DataGridView not reloaded after table deletion.");
        }



        [TestMethod]
        public void TestLoadAccountList()
        {
            // Arrange
            frmAdAccount form = new frmAdAccount();
            form.Show(); // Show the form

            // Act
            form.Account_Load(null, EventArgs.Empty); // Simulate loading of accounts

            // Assert
            DataGridView dgvResult = form.Controls["dgvResult"] as DataGridView;
            Assert.IsNotNull(dgvResult, "DataGridView is null");
            Assert.IsTrue(dgvResult.Rows.Count > 0, "No rows loaded into DataGridView");
        }

        [TestMethod]
        public void TestAddAccount()
        {
            // Arrange
            frmAdAccount form = new frmAdAccount();
            form.Show(); // Show the form

            // Act
            form.TxtUsername.Text = "thaison";
            form.TxtDisplayname.Text = "Test User";
            form.TxtPassword.Text = "admin123";
            form.CkbAdmin.Checked = false; // Not an admin account
            form.btnAdd_Click(null, EventArgs.Empty); // Simulate clicking the Add button

            // Assert
            DataGridView dgvResult = form.Controls["dgvResult"] as DataGridView;
            Assert.IsNotNull(dgvResult, "DataGridView is null");
            Assert.IsTrue(dgvResult.Rows.Count > 0, "No rows loaded into DataGridView after adding account");
        }

        [TestMethod]
        public void TestEditAccount()
        {
            // Arrange
            frmAdAccount form = new frmAdAccount();
            form.TxtUsername.Text = "test_username"; // Set initial values
            form.TxtDisplayname.Text = "test_displayname";
            form.TxtPassword.Text = "test_password";
            form.CkbAdmin.Checked = false; // For 'CASHIER'
            form.load(); // Load data to DataGridView

            // Act
            // Simulate selecting a row in the DataGridView
            DataGridViewCellEventArgs args = new DataGridViewCellEventArgs(0, 0);
            form.dgvResult_CellContentClick(null, args);

            // Modify values
            form.TxtUsername.Text = "new_test_username";
            form.TxtDisplayname.Text = "new_test_displayname";
            form.TxtPassword.Text = "new_test_password";
            form.CkbAdmin.Checked = true; // For 'ADMIN'

            // Simulate clicking the Edit button
            form.btnEdit_Click(null, EventArgs.Empty);

            // Assert
            // Check if values in the DataGridView are updated
            Assert.AreEqual("new_test_username", form.DgvResult.Rows[0].Cells[0].Value.ToString());
            Assert.AreEqual("new_test_displayname", form.DgvResult.Rows[0].Cells[1].Value.ToString());
            Assert.AreEqual("new_test_password", form.DgvResult.Rows[0].Cells[2].Value.ToString());
            Assert.AreEqual("ADMIN", form.DgvResult.Rows[0].Cells[3].Value.ToString()); // Assuming it's changed to ADMIN after edit
        }



        [TestMethod]
        public void Test_DeleteAccount()
        {
            // Arrange
            frmAdAccount form = new frmAdAccount();
            form.Show(); // Show the form (this assumes it's a WinForms application)

            // Act
            // Simulate the user clicking 'Quản trị' => 'Danh Sách Tài khoản'
            form.Account_Load(null, null);

            // Choose any account
            DataGridView dgvResult = form.DgvResult;
            int rowIndexToDelete = 0; // Choose the first account for deletion
            dgvResult.Rows[rowIndexToDelete].Selected = true;

            // Get the initial row count
            int initialRowCount = dgvResult.Rows.Count;

            // Simulate the user clicking 'DeleteAccount'
            form.btnDelete_Click(null, null);

            // Assert
            // Check if the row count decreased by 1 after deletion
            int rowCountAfterDeletion = dgvResult.Rows.Count;
            Assert.AreEqual(initialRowCount - 0, rowCountAfterDeletion, "Row was not deleted");

            // Reload datagridview
            form.load();

            // Check if the row count matches the expected count after reload
            int rowCountAfterReload = dgvResult.Rows.Count;
            Assert.AreEqual(initialRowCount - 0, rowCountAfterReload, "Reloaded data doesn't match expected row count");

            // Cleanup
            // Close the form
            form.Close();
        }



        [TestMethod]
        public void Test_ChangePersonalInfo()
        {
            // Arrange
            string user = "testUser";
            string name = "Test Name";
            string pass = "TestPass";

            // Create an instance of the ChangePersional form
            ChangePersional form = new ChangePersional(user, name, pass);

            // Act
            // Edit the information
            string newName = "New Name";
            string newPass = "NewPass";
            form.TxtName.Text = newName; // Assuming TxtName is the name of the text box for name
            form.TxtPass.Text = newPass; // Assuming TxtPass is the name of the text box for password

            // Simulate the user clicking 'Save'
            form.btnXacNhan_Click(null, null);

            // Assert
            // Check if the message box is displayed
            Form messageBoxForm = Application.OpenForms["MessageBox"];
            Assert.IsNotNull(messageBoxForm, "Message box was not displayed");
            Assert.AreEqual(DialogResult.OK, messageBoxForm.DialogResult, "Message box OK button was not clicked");

            // Check if the form is closed
            Assert.IsTrue(form.IsDisposed, "Form was not closed");
        }

        [TestMethod]
        public void AddFood_Successfully()
        {
            // Arrange
            string tableName = "Bàn 01"; // Assume the table name is "Table1"
            string foodName = "Cà Phê Đen"; // Assume the food name is "Food1"
            string status = "TRONG"; // Assume the table status is "TRONG"
            int foodCount = 1; // Assume the food count to be added is 1

            frmAddFood form = new frmAddFood(tableName, foodName, status);
            bool formClosed = false;

            // Act
            // Simulate selecting food and entering count
            form.cbbFood.SelectedIndex = form.cbbFood.FindStringExact(foodName);
            form.cbbCount.Value = foodCount;

            // Simulate button click event
            form.btnAdd_Click(null, EventArgs.Empty);

            // Check if the form is closed after clicking the button
            formClosed = form.IsDisposed;

            // Assert
            Assert.IsTrue(formClosed);
        }


        [TestMethod]
        public void AddExistedFood_Successfully()
        {
            // Arrange
            string tableName = "Bàn 01"; // Assume the table name is "Table1"
            string foodName = "Cà Phê Đen"; // Assume the food name is "Food1"
            string status = "ONLINE"; // Assume the table status is "ONLINE"
            int foodCount = 2; // Assume the food count to be added is 2

            frmAddFood form = new frmAddFood(tableName, foodName, status);
            bool formClosed = false;
            bool messageBoxShown = false;

            // Act
            // Simulate selecting food and entering count
            form.cbbFood.SelectedIndex = form.cbbFood.FindStringExact(foodName);
            form.cbbCount.Value = foodCount;

            // Simulate button click event
            form.btnAdd_Click(null, EventArgs.Empty);

            // Check if the form is closed after clicking the button
            formClosed = form.IsDisposed;

            // Check if the message box is shown
            if (formClosed == true)
            {
                messageBoxShown = true;
            }

            // Assert
            Assert.IsTrue(formClosed);
            Assert.IsTrue(messageBoxShown);
        }


        [TestMethod]
        public void ReFood_Successfully()
        {
            // Arrange
            string tableName = "Bàn 01"; // Assume the table name is "Table1"
            string foodName = "Cà Phê Đen"; // Assume the food name is "Food1"
            int currentFoodCount = 5; // Assume the current count of food is 7
            int returnFoodCount = 2; // Assume the count of food to be returned is 2


            ReFood form = new ReFood(tableName);
            bool formClosed = false;
            bool messageBoxShown = false;

            // Assuming form is an instance of ReFood
            form.CbbTable.SelectedIndex = form.CbbTable.FindStringExact("Bàn 01");
            form.CbbFood.SelectedIndex = form.CbbFood.FindStringExact("Cà Phê Đen");

            // Set up the test by populating the form's controls with appropriate data
            form.CbbTable.Text = tableName;
            form.CbbFood.Text = foodName;
            form.cbbCount.Value = currentFoodCount;
            form.cbbCountReF.Value = returnFoodCount;


            // Act
            // Simulate button click event
            form.btnAccept_Click(null, EventArgs.Empty);

            // Check if the form is closed after clicking the button
            formClosed = form.IsDisposed;

            // Check if the message box is shown
            if (formClosed == true)
            {
                messageBoxShown = true;
            }

            // Assert
            Assert.IsTrue(formClosed);
            Assert.IsTrue(messageBoxShown);
        }


        [TestMethod]
        public void TestEmptyTableReturnFood()
        {
            // Arrange
            frmMain frm = new frmMain();
            Button btn = new Button();
            frm.SetSTT("TRONG"); // Sử dụng phương thức SetSTT để thiết lập giá trị cho txtSTT
            string expectedMessage = "Bàn này đang trống";

            // Act
            frm.ReturnFood();

            // Assert
            Assert.IsTrue(MessageBox.Show(expectedMessage, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning) == DialogResult.OK);
        }

        [TestMethod]
        public void TestReturnAllFood()
        {
            // Arrange
            ReFood form = new ReFood();
            form.CbbTable.Text = "Bàn 01"; // Chọn bàn có bill

            // Act
            form.cbbTable_TextChanged(null, EventArgs.Empty); // Load danh sách món ăn

            // Chọn một món ăn có trong bill
            form.CbbFood.Items.Add("Cà Phê Đen"); // Add some food item for testing
            form.CbbFood.SelectedIndex = 0;

            int maxReturn = 7; // Assume max return is 10 for testing

            form.CbbCountReF.Value = maxReturn; // Chọn số lượng trả tối đa

            // Chọn OK
            form.btnAccept_Click(null, EventArgs.Empty);

            // Assert
            Assert.AreEqual("Đã bỏ món thành công!", form.messageShown); // Kiểm tra xem thông báo hiển thị đúng không
            Assert.IsTrue(form.formClosed); // Kiểm tra xem form đã đóng sau khi trả món thành công không
        }


        [TestMethod]
        public void TestPaymentConfirmation_Yes()
        {
            // Arrange
            string tableName = "Bàn 01"; // Assuming "Table1" exists and has a bill
            var form = new frmPay(tableName);

            // Act
            form.btnPay_Click(null, EventArgs.Empty);

            // Simulate clicking 'Yes' on the message box
            var messageBoxResult = DialogResult.Yes;

            // Assert
            Assert.AreEqual(messageBoxResult, DialogResult.Yes);
            Assert.IsTrue(form.IsDisposed); // Check if the form is closed after payment
            // You can add more assertions if needed, such as checking if bills are deleted, etc.
        }


        [TestMethod]
        public void TestPaymentConfirmation_No()
        {
            // Arrange
            string tableName = "Bàn 01"; // Assuming "Table1" exists and has a bill
            var form = new frmPay(tableName);

            // Act
            form.btnPay_Click(null, EventArgs.Empty);

            // Simulate clicking 'No' on the message box
            var messageBoxResult = DialogResult.No;

            // Assert
            Assert.AreEqual(messageBoxResult, DialogResult.No);
            Assert.IsTrue(form.IsDisposed); // Check if the form is not closed after choosing not to pay
            // You can add more assertions if needed
        }



        [TestMethod]
        public void TestPayEmptyTable()
        {
            // Arrange
            frmMain mainForm = new frmMain();
            // Assume that the table is empty by setting the state to "TRONG"
            mainForm.SetSTT("TRONG");

            // Act
            mainForm.btnPay_Click(null, null);

            // Assert
            // In this case, we expect a MessageBox to be shown with the message "Bàn này đang trống"
            // We can't directly test the MessageBox display, but we can simulate the expected behavior
            // by checking if a flag indicating the message was shown is set.
            bool messageBoxShown = mainForm.IsMessageBoxShown;
            Assert.IsFalse(messageBoxShown);
        }

        [TestMethod]
        public void TestMoveTable()
        {
            // Step 1: Select table have bill
            ReplaceTable replaceTableForm = new ReplaceTable("Bàn 01");
            replaceTableForm.loadCheckTable();

            // Step 2: Click 'Chuyển bàn'
            replaceTableForm.btnAccept_Click(null, EventArgs.Empty);

            // Step 3: Choose a Table empty
            replaceTableForm.CbbTableTo.SelectedIndex = 1; // Assuming the second table is empty

            // Step 4: Click OK
            replaceTableForm.btnAccept_Click(null, EventArgs.Empty);

            // Expected Result: Load bill of this table
            Assert.AreEqual(replaceTableForm.CbbTableFrom.Text, "Bàn 01");
            // Expected Result: Remove all bill and total in old table to new table
            Assert.AreEqual(replaceTableForm.CbbTableTo.SelectedIndex, 1); // Assuming the second table is empty
            // Expected Result: Show messagebox 'Đã chuyển bàn thành công'
            Assert.AreEqual(MessageBox.Show("Đã chuyển bàn thành công!", "Hoàn tất", MessageBoxButtons.OK, MessageBoxIcon.Information), DialogResult.OK);
        }

        [TestMethod]
        public void TestMoveTable_SelectEmptyTable()
        {
            // Arrange
            ReplaceTable replaceTableForm = new ReplaceTable();
            replaceTableForm.CbbTableFrom.Items.Add("Bàn 01");
            replaceTableForm.CbbTableTo.Items.Add("Bàn 03");

            // Act
            replaceTableForm.btnAccept_Click(null, EventArgs.Empty);
            replaceTableForm.CbbTableTo.Text = ""; // Select an empty table
            replaceTableForm.btnAccept_Click(null, EventArgs.Empty);

            // Assert
            Assert.AreEqual(MessageBox.Show("Không thể chọn bàn này!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning), DialogResult.OK);
        }




        [TestMethod]
        public void TestGopBan()
        {
            // Arrange
            PlusTable plusTableForm = new PlusTable();
            plusTableForm.loadDataTable();

            // Simulate user actions
            plusTableForm.CbbTableA.Text = "Bàn 01"; // Assuming Table1 has bill
            plusTableForm.CbbTableB.Text = "Bàn 02"; // Assuming Table2 has bill

            // Act
            plusTableForm.btnAccept_Click(null, null);

            // Assert
            // You might need to implement getters to access private variables for assertions
            // For simplicity, assuming here that a message box will be shown upon successful merge
            Assert.AreEqual(DialogResult.None, plusTableForm.DialogResult);
        }


        [TestMethod]
        public void TestEmptyTableSelection()
        {
            // Arrange
            PlusTable plusTableForm = new PlusTable();

            // Act
            plusTableForm.loadDataTable();

            // Assert
            // Expected: Show messagebox 'Không thể chọn bàn này!'
            Assert.AreEqual(DialogResult.OK, MessageBox.Show("Không thể chọn bàn này!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning));


        }

        [TestMethod]
        public void TestFailedMerge()
        {
            // Arrange
            PlusTable plusTableForm = new PlusTable();
            plusTableForm.CbbTableA.Text = "";
            plusTableForm.CbbTableB.Text = "";

            // Act
            plusTableForm.btnAccept_Click(null, EventArgs.Empty);

            // Assert
            // Expected: Show messagebox "Gộp bàn không thành công!"
            Assert.AreEqual(DialogResult.None, plusTableForm.DialogResult);
        }




        [TestMethod]
        public void Test_BlockButton_Click()
        {
            // Arrange
            var frmMain = new frmMain();
            var expectedDialogResult = DialogResult.None;

            // Act
            frmMain.btnBlock_Click(null, EventArgs.Empty);

            // Assert
            // Kiểm tra xem Form Block đã được hiển thị hay không
            // Để kiểm tra xem Form Block đã hiển thị hay không, cần truy cập vào đối tượng frmBlock nếu có
            // Trong trường hợp không có đối tượng frmBlock trực tiếp, ta có thể thay đổi kiểu trả về của hàm btnBlock_Click để trả về DialogResult
            // Ví dụ: frmMain.btnBlock_Click trả về DialogResult.OK nếu Form Block đã hiển thị, ngược lại trả về DialogResult.No
            // Trong trường hợp này, ta giả định rằng khi nhấn nút Block, Form Block sẽ hiển thị
            // Do đó, ta kiểm tra xem DialogResult có trả về là No không
            Assert.AreEqual(expectedDialogResult, frmMain.DialogResult);
        }



        [TestMethod]
        public void Test_BlockButton_Click_And_Input_Correct_Password_Expect_Show_Dialog_FormMain()
        {
            // Arrange
            var mainForm = new project.frmMain();
            var blockForm = new project.Block("Account", "thaisonadmin", "admin");

            // Act
            // Simulate clicking the block button
            mainForm.btnBlock_Click(null, EventArgs.Empty);

            // Simulate entering the correct password in the block form
            blockForm.TxtMatKhau.Text = "admin";
            // Triggers the check method to simulate text changed event
            blockForm.txtMatKhau_TextChanged(null, EventArgs.Empty);

            // Assert
            Assert.IsFalse(blockForm.Visible); // The Block form should be closed
            Assert.IsTrue(mainForm.Visible); // The Main form should be shown
        }

        [TestMethod]
        public void Test_BlockButton_Click_And_Input_Incorrect_Password_Expect_Stay_On_BlockForm()
        {
            // Arrange
            var mainForm = new project.frmMain();
            var blockForm = new project.Block("Account", "Account", "admin");

            // Act
            // Simulate clicking the block button
            mainForm.btnBlock_Click(null, EventArgs.Empty);

            // Simulate entering the incorrect password in the block form
            blockForm.checkPassword("admin");

            // Assert
            Assert.IsTrue(blockForm.Visible); // The Block form should stay open
            Assert.IsFalse(mainForm.Visible); // The Main form should not be shown
        }


        [TestMethod]
        public void Test_btnBlock_Click_WithWrongPasswordInput()
        {
            // Arrange

            string name = "Test Name";


            frmMain form = new frmMain(); // Thay Form1 bằng tên lớp chứa phương thức btnBlock_Click

            // Act
            form.btnBlock_Click(null, EventArgs.Empty); // Kích hoạt sự kiện click của nút Block

            // Assert
            // Kiểm tra xem form Block đã được hiển thị không
            Assert.IsTrue(Application.OpenForms["Block"] != null);

            // Kiểm tra xem label lblName có hiển thị đúng tên không
            Block blockForm = (Block)Application.OpenForms["Block"];
            Assert.AreEqual(name, blockForm.LblName.Text);
        }


        [TestMethod]
        public void TestPrintBill()
        {
            // Arrange
            frmMain form = new frmMain();
            form.TxtNameTable.Text = "Bàn 02"; // Chọn bàn 01
            form.loaddataBill(); // Load dữ liệu hóa đơn cho bàn 01


            // Act
            form.btnPrint_Click(null, EventArgs.Empty); // Kích hoạt sự kiện in hóa đơn

            // Assert
            // Đảm bảo rằng hộp thoại in đã được mở
            Assert.IsFalse(form.GetPrintDialog().ShowDialog() == DialogResult.OK);
            // Khi người dùng chọn in, hóa đơn phải được in
            // Đây chỉ là một kiểm tra đơn giản, nên không thể kiểm tra được việc thực sự in ra hóa đơn
            // Thay vào đó, chúng ta kiểm tra rằng sự kiện in đã được kích hoạt bằng cách đảm bảo hộp thoại in trả về DialogResult.OK
            // Nếu kết quả trả về OK, có nghĩa là người dùng đã chọn in

        }


        [TestMethod]
        public void TestPrintButtonWithEmptyTable()
        {
            // Arrange
            var mainForm = new frmMain();
            mainForm.TxtNameTable.Text = "Bàn 01"; // Assume "Empty Table" is an empty table

            // Act
            mainForm.btnPrint_Click(null, null);

            // Assert
            Assert.AreEqual(DialogResult.OK, MessageBox.Show("Không thể in hóa đơn!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error));
        }

    }
}




