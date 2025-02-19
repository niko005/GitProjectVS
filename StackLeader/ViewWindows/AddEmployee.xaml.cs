using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Runtime.Remoting.Contexts;

namespace StackLeader.ViewWindows
{
    public partial class AddEmployee : Page
    {
        public AddEmployee()
        {
            InitializeComponent();
            displayEmployeeData();
        }

        private void Click_Clear_Employee(object sender, RoutedEventArgs e)
        {
            clearFields();
        }


        private void Click_Add_Employee(object sender, RoutedEventArgs e)
        {
            if (addEmployee_lastname.Text == "" || addEmployee_firstname.Text == "" || addEmployee_password.Text == "" || addEmployee_username.Text == "" || addEmployee_email.Text == "")
            {
                MessageBox.Show("Пожалуйста, заполните все пустые поля", "Error Message", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if (WindowsHelper.connect.State == ConnectionState.Closed)
                {
                    try
                    {
                        WindowsHelper.connect.Open();
                        string checkEmID = "SELECT COUNT(*) FROM users WHERE username = @Username AND delete_date IS NULL";
                        using (SqlCommand checkEm = new SqlCommand(checkEmID, WindowsHelper.connect))
                        {
                            checkEm.Parameters.AddWithValue("@Username", addEmployee_username.Text.Trim());
                            int count = (int)checkEm.ExecuteScalar();
                            if (count >= 1)
                            {
                                MessageBox.Show(addEmployee_username.Text.Trim() + " уже занято"
                                    , "Error Message", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                            else
                            {
                                DateTime today = DateTime.Today;
                                string insertData = "INSERT INTO users " +
                                    "(email, username, password, lastname, firstname, insert_date) " +
                                    "VALUES(@Email, @Username, @Password, @Lastname, @Firstname, @Date_register)";
                                using (SqlCommand cmd = new SqlCommand(insertData, WindowsHelper.connect))
                                {
                                    /*cmd.Parameters.AddWithValue("@EmployeeID", addEmployee_id.Text.Trim());*/
                                    cmd.Parameters.AddWithValue("@Email", addEmployee_email.Text.Trim());
                                    cmd.Parameters.AddWithValue("@Username", addEmployee_username.Text.Trim());
                                    cmd.Parameters.AddWithValue("@Password", WindowsHelper.HashPassword(addEmployee_password.Text.Trim()));
                                    cmd.Parameters.AddWithValue("@Lastname", addEmployee_lastname.Text.Trim());
                                    cmd.Parameters.AddWithValue("@Firstname", addEmployee_firstname.Text.Trim());
                                    cmd.Parameters.AddWithValue("@Date_register", today);
                                    cmd.ExecuteNonQuery();
                                    MessageBox.Show("Сотрудник успешно добавлен!", "Information Message", MessageBoxButton.OK, MessageBoxImage.Information);
                                    clearFields();
                                    displayEmployeeData();
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex
                    , "Error Message", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    finally
                    {
                        WindowsHelper.connect.Close();
                    }
                }
            }
        }

        public void clearFields()
        {
            addEmployee_id.Text = null;
            addEmployee_email.Text = null;
            addEmployee_username.Text = null;
            addEmployee_password.Text = null;
            addEmployee_lastname.Text = null;
            addEmployee_firstname.Text = null;
        }

        private void Click_Update_Employee(object sender, RoutedEventArgs e)
        {
            if (addEmployee_id.Text == "" || addEmployee_lastname.Text == "" || addEmployee_firstname.Text == "" || addEmployee_password.Text == "" || addEmployee_username.Text == "" || addEmployee_email.Text == "")
            {
                MessageBox.Show("Пожалуйста, заполните все пустые поля", "Error Message", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("Вы уверены, что хотите обновить? " + "Employee ID: " + addEmployee_id.Text.Trim() + "?", "Confirmation Message", MessageBoxButton.YesNo, MessageBoxImage.Information);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    try
                    {
                        WindowsHelper.connect.Open();
                        DateTime today = DateTime.Today;
                        bool UserNameOne = false;
                        string selectUsername = "SELECT COUNT(*) FROM users WHERE username = @user";
                        string selectUsername2 = "SELECT username FROM users WHERE EmployeeID = @EmpID";

                        using (SqlCommand checkUser = new SqlCommand(selectUsername, WindowsHelper.connect))
                        {
                            checkUser.Parameters.AddWithValue("@user", addEmployee_username.Text.Trim());
                            int count = (int)checkUser.ExecuteScalar();

                            using (SqlCommand UserOneCheck = new SqlCommand(selectUsername2, WindowsHelper.connect))
                            {
                                UserOneCheck.Parameters.AddWithValue("@EmpID", addEmployee_id.Text.Trim());
                                string CheckUserName = (string)UserOneCheck.ExecuteScalar();
                                if (CheckUserName == addEmployee_username.Text.Trim())
                                    UserNameOne = true;


                                if (count >= 1 && UserNameOne == false)
                                {
                                    MessageBox.Show(addEmployee_username.Text.Trim() + "Уже занято", "Error Message",
                                        MessageBoxButton.OK, MessageBoxImage.Error);
                                }
                                else
                                {
                                    string updateData = "UPDATE users SET email = @Email" +
                                    ", username = @Username, password = @Password" +
                                    ", lastname = @Lastname, firstname = @Firstname, update_date = @updateDate" +
                                    " WHERE EmployeeID = @employeeID";

                                    using (SqlCommand cmd = new SqlCommand(updateData, WindowsHelper.connect))
                                    {
                                        cmd.Parameters.AddWithValue("@Email", addEmployee_email.Text.Trim());
                                        cmd.Parameters.AddWithValue("@Username", addEmployee_username.Text.Trim());
                                        cmd.Parameters.AddWithValue("@Password", WindowsHelper.HashPassword(addEmployee_password.Text.Trim()));
                                        cmd.Parameters.AddWithValue("@Lastname", addEmployee_lastname.Text.Trim());
                                        cmd.Parameters.AddWithValue("@Firstname", addEmployee_firstname.Text.Trim());
                                        cmd.Parameters.AddWithValue("@updateDate", today);
                                        cmd.Parameters.AddWithValue("@employeeID", addEmployee_id.Text.Trim());

                                        cmd.ExecuteNonQuery();

                                        MessageBox.Show("Изменения применены!", "Information Message", MessageBoxButton.OK, MessageBoxImage.Information);


                                        displayEmployeeData();

                                        WindowsHelper.AddAssignments.LoadEmployees();
                                        WindowsHelper.AssignmentsListWindow.displayTasksData();

                                        clearFields();

                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex
                        , "Error Message", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    finally
                    {
                        WindowsHelper.connect.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Обновление отменено"
                        , "Information Message", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        private void Click_Back_Employee(object sender, RoutedEventArgs e)
        {
            WindowsHelper.MainWindow.Frame_W.Content = WindowsHelper.Employees;
            WindowsHelper.MainWindow.TitleText.Text = "Сотрудники";
            WindowsHelper.Employees.displayEmployeeData();
        }

        public void displayEmployeeData()
        {
            EmployeeData ed = new EmployeeData();
            List<EmployeeData> listData = ed.employeeListData();
            dataGridAddEmpl.ItemsSource = listData;
        }

        private void dataGridAddEmpl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dataGridAddEmpl.SelectedItem is EmployeeData selectedEmployee)
            {
                /*dataGridAddEmpl row = dataGridAddEmpl.Rows[e.RowIndex];*/
                addEmployee_id.Text = selectedEmployee.EmployeeID.ToString();
                addEmployee_lastname.Text = selectedEmployee.Lastname;
                addEmployee_firstname.Text = selectedEmployee.Firstname;
                addEmployee_email.Text = selectedEmployee.Email;
                addEmployee_username.Text = selectedEmployee.Username;
                addEmployee_password.Text = selectedEmployee.Password;
                e.Handled = true;
            }
        }

        private void Click_Delete_Employee(object sender, RoutedEventArgs e)
        {
            if (addEmployee_id.Text == "" || addEmployee_lastname.Text == "" || addEmployee_firstname.Text == "" || addEmployee_password.Text == "" || addEmployee_username.Text == "" || addEmployee_email.Text == "")
            {
                MessageBox.Show("Пожалуйста, заполните все пустые поля", "Error Message", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("Вы уверены, что хотите удалить? " + "Employee ID: " + addEmployee_id.Text.Trim() + "?", "Confirmation Message", MessageBoxButton.YesNo, MessageBoxImage.Information);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    try
                    {
                        WindowsHelper.connect.Open();
                        DateTime today = DateTime.Today;
                        string updateData = "UPDATE users SET delete_date = @deleteDate " + "WHERE EmployeeID = @employeeID";

                        using (SqlCommand cmd = new SqlCommand(updateData, WindowsHelper.connect))
                        {
                            cmd.Parameters.AddWithValue("@deleteDate", today);
                            cmd.Parameters.AddWithValue("@employeeID", addEmployee_id.Text.Trim());

                            cmd.ExecuteNonQuery();

                            displayEmployeeData();

                            MessageBox.Show("Изменения применены!", "Information Message", MessageBoxButton.OK, MessageBoxImage.Information);

                            clearFields();
                            WindowsHelper.CountDelEmployees++;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex, "Error Message", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    finally
                    {
                        WindowsHelper.connect.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Удаление отменено", "Information Message", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }
    }
}