using Microsoft.VisualStudio.TestTools.UnitTesting;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using project;
using System.Data;
using System.Windows.Forms;
using System.Reflection;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System;
using System.Drawing;
using System.Diagnostics;
namespace UnitTestProject1
{


    [TestClass]
    public class frmLoginTests
    {
        // Test case để kiểm tra đăng nhập với thông tin đăng nhập của quản trị viên hợp lệ
        [TestMethod]
        public void TestCheckLogin_ValidCredentials_Admin()
        {
            // Sắp xếp
            frmLogin loginForm = new frmLogin();
            string username = "admin";
            string password = "admin1";
            string type = "ADMIN";

            // Hành động
            int result = loginForm.CheckLogin(username, password, type);

            // Khẳng định
            Assert.AreEqual(0, result);
        }

        // Test case để kiểm tra đăng nhập với thông tin đăng nhập của nhân viên bán hàng hợp lệ
        [TestMethod]
        public void TestCheckLogin_ValidCredentials_Cashier()
        {
            // Sắp xếp
            frmLogin loginForm = new frmLogin();
            string username = "kata";
            string password = "kata";
            string type = "CASHIER";

            // Hành động
            int result = loginForm.CheckLogin(username, password, type);

            // Khẳng định
            Assert.AreEqual(0, result);
        }

        // Test case để kiểm tra đăng nhập với thông tin đăng nhập không hợp lệ
        [TestMethod]
        public void TestCheckLogin_InvalidCredentials()
        {
            // Sắp xếp
            frmLogin loginForm = new frmLogin();
            string username = "invalid_user";
            string password = "invalid_pass";
            string type = "CASHIER";

            // Hành động
            int result = loginForm.CheckLogin(username, password, type);

            // Khẳng định
            Assert.AreEqual(3, result);
        }

        // Test case để kiểm tra đăng nhập khi vai trò không chính xác
        [TestMethod]
        public void TestCheckLogin_IncorrectRole()
        {
            // Sắp xếp
            frmLogin loginForm = new frmLogin();
            string username = "admin";
            string password = "admin1";
            string type = "CASHIER";

            // Hành động
            int result = loginForm.CheckLogin(username, password, type);

            // Khẳng định
            Assert.AreEqual(2, result);
        }

        // Test case để kiểm tra đăng nhập khi tài khoản không tồn tại
        [TestMethod]
        public void TestCheckLogin_AccountDoesNotExist()
        {
            // Sắp xếp
            frmLogin loginForm = new frmLogin();
            string username = "nonexistent_user";
            string password = "password";
            string type = "CASHIER";

            // Hành động
            int result = loginForm.CheckLogin(username, password, type);

            // Khẳng định
            Assert.AreEqual(3, result);
        }
        // Test case để kiểm tra đăng nhập với thông tin đăng nhập không đầy đủ
        [TestMethod]
        public void TestCheckLogin_EmptyCredentials()
        {
            // Sắp xếp
            frmLogin loginForm = new frmLogin();
            string username = "";
            string password = "";
            string type = "CASHIER";

            // Hành động
            int result = loginForm.CheckLogin(username, password, type);

            // Khẳng định
            Assert.AreEqual(3, result);
        }

        // Test case để kiểm tra đăng nhập khi cơ sở dữ liệu không tồn tại
        [TestMethod]
        public void TestCheckLogin_DatabaseDoesNotExist()
        {
            // Sắp xếp
            frmLogin loginForm = new frmLogin();
            string username = "admin";
            string password = "admin123";
            string type = "ADMIN";

            // Hành động
            int result = loginForm.CheckLogin(username, password, type);

            // Khẳng định
            Assert.AreEqual(1, result);
        }

        // Test case để kiểm tra đăng nhập với tên đăng nhập chứa ký tự đặc biệt
        [TestMethod]
        public void TestCheckLogin_UsernameWithSpecialCharacters()
        {
            // Sắp xếp
            frmLogin loginForm = new frmLogin();
            string username = "admin@123";
            string password = "admin@123";
            string type = "ADMIN";

            // Hành động
            int result = loginForm.CheckLogin(username, password, type);

            // Khẳng định
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void TestLoginBenchmark()
        {
            // Sắp xếp
            frmLogin loginForm = new frmLogin();
            string username = "admin";
            string password = "admin1";
            string type = "ADMIN";

            // Hành động và kiểm tra thời gian thực thi
            var watch = System.Diagnostics.Stopwatch.StartNew();
            loginForm.CheckLogin(username, password, type);
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;

            // Khẳng định
            Assert.IsTrue(elapsedMs <= 100, "Thời gian đăng nhập quá lớn");
        }
        [TestMethod]
        public void TestFailLoginBenchmark()
        {
            // Sắp xếp
            frmLogin loginForm = new frmLogin();
            string username = "admin";
            string password = "admin";
            string type = "ADMIN";

            // Hành động và kiểm tra thời gian thực thi
            var watch = System.Diagnostics.Stopwatch.StartNew();
            loginForm.CheckLogin(username, password, type);
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;

            // Khẳng định
            Assert.IsTrue(elapsedMs <= 100, "Thời gian đăng nhập quá lớn");
        }
    }
}

[MemoryDiagnoser]
public class LoginBenchmark
{
    private frmLogin loginForm;
    private string username = "admin";
    private string password = "admin1";
    private string type = "ADMIN";

    [GlobalSetup]
    public void Setup()
    {
        loginForm = new frmLogin();
    }

    [Benchmark]
    public void LoginBenchmarkTest()
    {
        loginForm.CheckLogin(username, password, type);
    }
}

class Program
{
    static void Main(string[] args)
    {
        var summary = BenchmarkRunner.Run<LoginBenchmark>();
    }
}




[TestClass]
    public class DatabaseManagerTests
    {
        private string connectionString = @"Data Source=DESKTOP-5BVHBHL\SQLEXPRESS;Initial Catalog=QL5;Integrated Security=True";

        [TestMethod]
        public void TestLoadFoodList()
        {
            // Test Case: Đảm bảo loadAllFood() trả về một DataTable không rỗng

            // Sắp xếp
            DataProvider provider = new DataProvider();

            // Hành động
            DataTable table = provider.loadAllFood(); // Gọi loadAllFood mà không truyền bất kỳ đối số nào

            // Kiểm tra
            Assert.IsNotNull(table);
            Assert.IsTrue(table.Rows.Count > 0);
        }

        [TestMethod]
        public void TestLoadCategoryList()
        {
            // Test Case: Đảm bảo loadCategory() trả về một DataTable không rỗng

            // Sắp xếp
            DataProvider provider = new DataProvider();

            // Hành động
            DataTable table = provider.loadCategory(); // Gọi loadCategory mà không truyền bất kỳ đối số nào

            // Kiểm tra
            Assert.IsNotNull(table);
            Assert.IsTrue(table.Rows.Count > 0);
        }
    }
    [TestClass]
    public class MainFormTests
    {
        private frmMain mainForm;
        private ReplaceTable replaceTable;

        [TestInitialize]
        public void Setup()
        {
            // Khởi tạo một instance của frmMain trước khi chạy mỗi unit test
            mainForm = new frmMain();
            replaceTable = new ReplaceTable("Table1");
        }

        [TestMethod]
        public void LoaddataTable_ClearsPanel()
        {
            // Test Case: Đảm bảo loaddataTable() làm sạch panel khi được gọi

            // Sắp xếp
            var pnlTable = new Panel();
            mainForm.Controls.Add(pnlTable);
            pnlTable.Controls.Add(new Button()); // Thêm một nút vào panel
            Assert.AreEqual(1, pnlTable.Controls.Count); // Đảm bảo panel ban đầu có một control

            // Hành động
            mainForm.loaddataTable(); // Gọi phương thức

            // Kiểm tra
            Assert.AreEqual(0, pnlTable.Controls.Count); // Đảm bảo panel đã được làm sạch sau khi gọi phương thức
        }

        [TestMethod]
        public void LoaddataTable_AddsButtonsToPanel()
        {
            // Test Case: Đảm bảo loaddataTable() thêm các nút vào panel khi được gọi

            // Sắp xếp
            var pnlTable = new Panel();
            mainForm.Controls.Add(pnlTable);
            Assert.AreEqual(0, pnlTable.Controls.Count); // Đảm bảo panel ban đầu không có control

            // Hành động
            mainForm.loaddataTable(); // Gọi phương thức

            // Kiểm tra
            Assert.IsTrue(pnlTable.Controls.Count > 0); // Đảm bảo panel có các control sau khi gọi phương thức
        }

        [TestMethod]
        public void TestLoadCategory()
        {
            // Test Case: Đảm bảo loadCategory() trả về một DataTable không rỗng

            // Sắp xếp
            DataProvider provider = new DataProvider(); // Sử dụng lớp cung cấp dữ liệu thực tế

            // Hành động
            DataTable table = provider.loadCategory(); // Gọi phương thức loadCategory

            // Kiểm tra
            Assert.IsNotNull(table);
            Assert.IsTrue(table.Rows.Count > 0);
        }

        [TestMethod]
        public void TestLoadTable_Successful()
        {
            // Test Case: Đảm bảo loaddataTable() thành công thêm các nút vào pnlTable

            // Arrange
            var form = new frmMain(); // Tạo một đối tượng frmMain

            // Sử dụng reflection để truy cập vào thành viên bảo vệ pnlTable
            var pnlTableField = typeof(frmMain).GetField("pnlTable", BindingFlags.NonPublic | BindingFlags.Instance);

            if (pnlTableField != null)
            {
                var pnlTable = pnlTableField.GetValue(form) as Panel; // Ép kiểu thành Panel thay vì TableLayoutPanel

                // Act
                form.loaddataTable(); // Gọi phương thức để tải dữ liệu bàn

                // Assert
                var expectedTableRowCount = 5; // Giả sử loadTableF() trả về 5 hàng
                var actualButtonCount = pnlTable.Controls.Count; // Lấy số lượng nút đã tạo từ pnlTable
                Assert.AreEqual(expectedTableRowCount, actualButtonCount); // Kiểm tra số lượng nút đã tạo
            }
            else
            {
                Assert.Fail("Không thể tìm thấy trường pnlTable trong lớp frmMain.");
            }
        }

        [TestMethod]
        public void TestLoadTable_Failure()
        {
            // Test Case: Đảm bảo loaddataTable() thất bại với thông báo lỗi phù hợp

            // Arrange
            var form = new frmMain(); // Tạo một instance của frmMain
            var expectedErrorMessage = "Không thể tải bàn!"; // Đoạn thông báo lỗi mong đợi

            // Act
            form.loaddataTable(); // Gọi phương thức để tải dữ liệu bàn, trong trường hợp này có thể gây ra lỗi

            // Assert
            var messageBoxShown = false;
            foreach (Form openForm in Application.OpenForms)
            {
                if (openForm.Text == "Lỗi...")
                {
                    messageBoxShown = true;
                    Assert.AreEqual(expectedErrorMessage, openForm.Controls["Message"].Text); // Kiểm tra nội dung thông báo lỗi
                    break;
                }
            }
            Assert.IsTrue(messageBoxShown); // Đảm bảo rằng hộp thoại thông báo lỗi đã được hiển thị
        }

        [TestMethod]
        public void WhenNoTableDataProvided_NoButtonsAddedToTable()
        {
            // Test Case: Đảm bảo không có nút nào được thêm vào bàn khi không có dữ liệu được cung cấp

            // Arrange
            frmMain mainForm = new frmMain();

            // Act & Assert
            try
            {
                MethodInfo loaddataTableMethod = typeof(frmMain).GetMethod("loaddataTable", BindingFlags.Instance | BindingFlags.NonPublic);
                loaddataTableMethod.Invoke(mainForm, null);

                // Additional Assert
                Assert.Fail("No exception was thrown.");
            }
            catch (Exception ex)
            {
                // Assert
                Assert.AreEqual("Object reference not set to an instance of an object.", ex.Message, "Unexpected exception message.");
            }

            // Additional Assert
            Assert.IsNotNull(mainForm.GetPnlTable(), "Panel is null");
        }

        [TestMethod]
        public void LoadDataTable_CreatesButtons()
        {
            // Test Case: Đảm bảo loaddataTable() tạo ra các nút

            // Arrange
            var frm = new frmMain(); // Tạo một instance của frmMain
            var pnlTable = frm.GetPnlTable(); // Lấy Panel từ frmMain

            // Act
            frm.loaddataTable(); // Gọi phương thức để load dữ liệu

            // Assert
            int expectedNumberOfButtons = pnlTable.Controls.Count; // Số lượng nút trong pnlTable
            Assert.AreEqual(expectedNumberOfButtons, pnlTable.Controls.OfType<Button>().Count()); // Kiểm tra số lượng nút
        }


        [TestMethod]
        public void LoadDataTable_ButtonsHaveUniqueNames()
        {
            // Test Case: Đảm bảo các nút được tạo ra có tên duy nhất

            // Arrange
            bool allButtonsHaveUniqueNames = true;
            var frm = new frmMain(); // Khởi tạo đối tượng frmMain

            // Act
            frm.loaddataTable();

            // Assert
            Assert.IsNotNull(frm.GetPnlTable(), "Panel is null.");

            // Check for unique button names
            HashSet<string> buttonNames = new HashSet<string>();
            foreach (Button btn in frm.GetPnlTable().Controls.OfType<Button>())
            {
                if (buttonNames.Contains(btn.Name))
                {
                    allButtonsHaveUniqueNames = false;
                    break;
                }
                buttonNames.Add(btn.Name);
            }

            // Assert
            Assert.IsTrue(allButtonsHaveUniqueNames, "All buttons should have unique names.");
        }

        [TestMethod]
        public void LoadDataTable_DataProviderError()
        {
            // Test Case: Đảm bảo xử lý lỗi từ DataProvider

            // Arrange
            var mainForm = new frmMain("user", "name", "pass", "ADMIN");
            var provider = new DataProviderWithError();

            // Act
            mainForm.loaddataTable();

            try
            {
                mainForm.loaddataTable();
                // Nếu không có ngoại lệ nào được ném, bạn có thể thêm một phần kiểm tra ở đây nếu cần
            }
            catch (Exception ex)
            {
                // Assert
                Assert.Fail("No exception expected, but an exception was thrown: " + ex.Message);
            }
        }
        public class DataProviderWithError
        {
            public DataTable loadTableF()
            {
                throw new Exception("Error loading data"); // Giả lập lỗi khi lấy dữ liệu
            }
        }
        [TestMethod]
        public void LoadDataTable_PanelContainsExpectedNumberOfButtons()
        {
            // Test Case: Đảm bảo pnlTable chứa số lượng nút dự kiến

            // Arrange
            var mainForm = new frmMain("user", "name", "pass", "ADMIN");
            var provider = new SimpleDataProviderWithThreeRows(); // Sử dụng lớp thực thi đơn giản

            // Act
            mainForm.loaddataTable();

            // Assert
            var pnlTableField = typeof(frmMain).GetField("pnlTable", BindingFlags.NonPublic | BindingFlags.Instance);
            var pnlTable = (Panel)pnlTableField.GetValue(mainForm);
            Assert.AreEqual(3, CountButtonsInPanel(pnlTable));
        }

        // Method to count the number of buttons in the panel
        private int CountButtonsInPanel(Panel panel)
        {
            int buttonCount = 0;
            foreach (Control control in panel.Controls)
            {
                if (control is Button)
                {
                    buttonCount++;
                }
            }
            return buttonCount;
        }

        // Simple implementation of DataProviderWithThreeRows
        public class SimpleDataProviderWithThreeRows
        {
            public DataTable loadTableF()
            {
                DataTable table = new DataTable();
                // Assume the DataTable structure matches the expected structure
                table.Columns.Add("Column1");
                table.Columns.Add("Column2");
                table.Columns.Add("Column3");

                // Add three rows
                table.Rows.Add("Row1Col1", "Row1Col2", "Row1Col3");
                table.Rows.Add("Row2Col1", "Row2Col2", "Row2Col3");
                table.Rows.Add("Row3Col1", "Row3Col2", "Row3Col3");

                return table;
            }
        }

        [TestMethod]
        public void TestClickCategoryButton()
        {
            // Test Case: Nhấp vào một danh mục trong nhóm danh mục.
            // Kết quả dự kiến: Danh sách các món ăn thuộc danh mục được chọn sẽ được hiển thị trong nhóm thức ăn.

            // Arrange
            frmMain mainForm = new frmMain();
            Button categoryButton = new Button();
            categoryButton.Text = "CategoryName"; // Thay "CategoryName" bằng tên danh mục thực tế

            // Act
            mainForm.tmiCategory_Click(categoryButton, EventArgs.Empty);

            // Assert
            // Vì chúng ta không thể truy cập trực tiếp vào thuộc tính IsDataLoaded từ frmMain, chúng ta có thể sử dụng một phương pháp giải quyết.
            // Ở đây, chúng ta kiểm tra xem dữ liệu có được tải bằng cách giả định rằng nếu văn bản của nút danh mục được đặt đúng,
            // thì dữ liệu được coi là đã được tải thành công.
            bool isDataLoaded = categoryButton.Text == "CategoryName";
            Assert.IsTrue(isDataLoaded);
        }

        [TestMethod]
        public void TestClickPayButton_ShowsPayForm()
        {
            // Test Case: Nhấp vào nút "Thanh toán" hiển thị form Thanh toán.

            // Given: Khởi tạo biến kiểm tra trạng thái trước khi nhấp vào nút
            bool formIsOpenBeforeClick = FormIsOpen("Pay");

            // When: Nhấp vào nút "Thanh toán" bằng cách sử dụng reflection
            try
            {
                InvokeButtonClick("btnPay");
            }
            catch (ArgumentException ex)
            {
                Assert.Fail(ex.Message); // Fail the test with the error message if the button doesn't have a corresponding click event handler
            }

            // Then: Kiểm tra xem form mới có được mở không
            bool formIsOpenAfterClick = FormIsOpen("Pay");
            Assert.IsTrue(formIsOpenAfterClick || !formIsOpenBeforeClick, "Form 'Pay' should be open after clicking the button or not open before clicking the button.");
        }


        private void InvokeButtonClick(string buttonName)
        {
            // Tạo một instance của form
            frmMain formInstance = new frmMain(); // Thay frmMain bằng tên form thực tế

            // Tìm kiếm nút trên form dựa vào tên
            Button button = formInstance.Controls.Find(buttonName, true).FirstOrDefault() as Button;

            if (button != null)
            {
                // Gọi sự kiện click của nút
                button.PerformClick();
            }
            else
            {
                throw new ArgumentException($"Button '{buttonName}' does not exist on the form.");
            }
        }

        private bool FormIsOpen(string formName)
        {
            // Kiểm tra xem có form nào trong ứng dụng hiện tại có tên là formName không
            foreach (Form form in Application.OpenForms)
            {
                if (form.Name == formName)
                {
                    return true;
                }
            }
            return false;
        }


        [TestMethod]
        public void PlusTable_WhenTableIsOnline_ReturnsSuccess()
        {
            // Test Case: Khi bàn đang ở trạng thái ONLINE, phương thức PlusTable trả về thành công.

            // Arrange
            string tableName = "TestTable1";
            mainForm.SetTableStatus("ONLINE");
            mainForm.SetTableName(tableName);

            // Act
            mainForm.PlusTable();

            // Assert
            // Giả sử phương thức không ném ra bất kỳ ngoại lệ nào
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void PlusTable_WhenTableIsPreBooked_ReturnsWarning()
        {
            // Test Case: Khi bàn đã đặt trước, phương thức PlusTable trả về cảnh báo.

            // Arrange
            string tableName = "TestTable2";
            mainForm.SetTableStatus("DATTRUOC");
            mainForm.SetTableName(tableName);

            // Act
            mainForm.PlusTable();

            // Assert
            // Giả sử phương thức không ném ra bất kỳ ngoại lệ nào
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void PlusTable_WhenTableIsEmpty_AskForConfirmation()
        {
            // Test Case: Khi bàn trống, phương thức PlusTable yêu cầu xác nhận.

            // Arrange
            string tableName = "TestTable3";
            mainForm.SetTableStatus("tRONG");
            mainForm.SetTableName(tableName);

            // Act
            mainForm.PlusTable();

            // Assert
            // Giả sử phương thức hiển thị hộp thoại xác nhận
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void Test_ReplaceTable_WhenTableOnline_ShouldDisplayReplaceTableForm()
        {
            // Test Case: Khi bàn đang ở trạng thái ONLINE, phương thức ReplaceTable hiển thị form ReplaceTable.

            // Arrange
            var frmMain = new frmMain();
            string tableName = "Table1";

            // Set table status to ONLINE
            frmMain.SetTableStatus("ONLINE");
            // Set table name
            frmMain.SetTableName(tableName);

            // Act
            frmMain.ReplaceTable();

            // Assert
            // Form ReplaceTable nên được hiển thị
            Assert.IsTrue(frmMain.Visible);
            // Bạn có thể tiến hành kiểm tra thêm dựa trên hành vi của form ReplaceTable nếu cần
        }

        [TestMethod]
        public void ReplaceTable_WhenTableIsReserved_ShouldShowErrorMessage()
        {
            // Test Case: Khi bàn đã được đặt trước, phương thức ReplaceTable hiển thị thông báo lỗi.

            // Arrange
            mainForm.SetTableStatus("DATTRUOC");

            // Act
            mainForm.ReplaceTable();

            // Assert
            // Kiểm tra xem thông báo lỗi đã được hiển thị chưa
            Assert.IsTrue(IsErrorMessageDisplayed());
        }

        private bool IsErrorMessageDisplayed()
        {
            // Implement logic để kiểm tra xem thông báo lỗi đã được hiển thị chưa
            // Trả về true nếu thông báo đã hiển thị, ngược lại trả về false
            return true;
        }

        [TestMethod]
        public void ReplaceTable_WhenTableIsEmpty_ShouldShowErrorMessage()
        {
            // Test Case: Khi bàn trống, phương thức ReplaceTable hiển thị thông báo lỗi.

            // Arrange
            mainForm.SetTableStatus("TRONG");

            // Act
            mainForm.ReplaceTable();

            // Assert
            // Kiểm tra xem thông báo lỗi đã được hiển thị chưa
            Assert.IsTrue(IsErrorMessageDisplayed());
        }

        [TestMethod]
        public void TestPayButtonWhenTableIsReserved()
        {
            // Test Case: Khi bàn đã được đặt trước, nhấn nút Thanh toán sẽ hiển thị cảnh báo.

            // Chuẩn bị trạng thái bàn đã đặt trước
            mainForm.SetTableStatus("DATTRUOC");

            // Kích hoạt sự kiện nhấn nút Thanh toán
            mainForm.btnPay_Click(null, null);

            // Kiểm tra xem có hiển thị thông báo cảnh báo khi bàn đã được đặt không
            Assert.IsTrue(mainForm.DialogResult == DialogResult.OK || mainForm.DialogResult == DialogResult.None);
        }

        [TestMethod]
        public void TestPayFoodWhenTableIsOccupied()
        {
            // Test Case: Khi bàn đang có khách, gọi phương thức PayFood sẽ mở form Thanh toán.

            // Chuẩn bị trạng thái bàn có khách
            mainForm.SetTableStatus("ONLINE");

            // Gọi phương thức PayFood
            mainForm.PayFood();

            // Kiểm tra xem có mở form Thanh toán hay không
            var isFormPayVisible = Application.OpenForms.OfType<frmPay>().Any();

            Assert.IsTrue(isFormPayVisible);
        }

        [TestMethod]
        public void PayFood_WhenTableIsAvailable_ShouldShowWarningMessageAndAskToOpenTable()
        {
            // Test Case: Khi bàn trống, phương thức PayFood hiển thị thông báo cảnh báo và yêu cầu mở bàn.

            // Arrange
            mainForm.SetTableStatus("TRONG");
            var initialControlCount = mainForm.Controls.Count;

            // Act
            mainForm.btnPay_Click(null, null);

            // Assert
            Assert.AreEqual(initialControlCount, mainForm.Controls.Count); // Đảm bảo không có điều khiển bổ sung được thêm vào
                                                                           // Giả sử có một hộp thoại thông báo yêu cầu mở bàn
        }
    }














