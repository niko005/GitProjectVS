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
using System.Reflection.Emit;

namespace StackLeader.ViewWindows
{
    public partial class AuthorizationWindow : Window
    {
        public static string Firstname { get; private set; }
        public static string Username { get; private set; }
        public static string EmailReg { get; private set; }
        public static string SmtpEmail { get; private set; }
        public static int UserID { get; private set; }
        public static DataTable table = new DataTable();

        public AuthorizationWindow()
        {
            InitializeComponent();
        }
        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        //Метод перетаскивания окна при помощи мышки
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        public void exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
            WindowsHelper.connect.Close();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        // Получаем список администраторов
        public void SelectAdmin()
        {        
            SearchAdmin adminData = new SearchAdmin();
            WindowsHelper.admins = adminData.adminListData();
        }
        public void login_btn_Click(object sender, RoutedEventArgs e)
        {
            if (login_username.Text == "" || login_password.Password == "")
            { MessageBox.Show("Пожалуйста, заполните все пустые поля", "Error Message", MessageBoxButton.OK, MessageBoxImage.Error); }
            else
            {try
                {
                    string selectData = "SELECT * FROM users WHERE username = @username " +
                                "AND password = @password" + " AND delete_date IS NULL";
                    using (SqlCommand cmd = new SqlCommand(selectData, WindowsHelper.connect))
                    {
                        cmd.Parameters.AddWithValue("@username", login_username.Text.Trim());
                        cmd.Parameters.AddWithValue("@password", WindowsHelper.HashPassword(login_password.Password.Trim()));
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        adapter.Fill(table);
                        if (table.Rows.Count == 0)
                        {
                            MessageBox.Show("Ошибка Логина или Пароля", "Error Message", MessageBoxButton.OK, MessageBoxImage.Information);
                            return;
                        }
                        Firstname = table.Rows[0]["firstname"].ToString();
                        EmailReg = table.Rows[0]["email"].ToString();
                        Username = table.Rows[0]["username"].ToString();
                        UserID = Convert.ToInt32(table.Rows[0]["EmployeeID"]);
                        WindowsHelper.AutorizationUserID = UserID;
                        string[] parts = EmailReg.Split('@');
                        SmtpEmail = parts[1];
                        SelectAdmin();
                        WindowsHelper.IsAdmin = false;
                        foreach (var admin in WindowsHelper.admins)
                        {
                            if (admin.AdminID == UserID)
                            {
                                WindowsHelper.IsAdmin = true;
                                break;
                            }
                        }
                        if (table.Rows.Count >= 1)
                        {
                            WindowsHelper.AssignmentsListWindow.AllEmployeeTask_Check.IsChecked = true;
                            if (WindowsHelper.IsAdmin == false)
                            {
                                WindowsHelper.MainWindow.employee_btn.Visibility = Visibility.Collapsed;
                                WindowsHelper.MainWindow.home_btn.Visibility = Visibility.Collapsed;
                            }
                            else
                            {
                                WindowsHelper.MainWindow.personal_account_btn.Visibility = Visibility.Collapsed;
                            }
                            this.Hide();
                            WindowsHelper.MainWindow.Show();
                            WindowsHelper.MainWindow.displayNotificationsData();
                            WindowsHelper.Employees.displayEmployeeData();
                            WindowsHelper.MainWindow.UpdateUnreadNotificationsCount();
                            WindowsHelper.timernotification.Start();
                            if (WindowsHelper.MainWindow != null)
                                WindowsHelper.MainWindow.SetWelcomeMessage($"Добро пожаловать, {Firstname}!");
                        }
                        else
                        {
                            MessageBox.Show("Ошибка Логина и Пароля", "Error Message", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error:" + ex, "Error Message", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                finally
                {
                    WindowsHelper.connect.Close();
                }}
        }

        private void login_signupBtn_Click(object sender, RoutedEventArgs e)
        {
           WindowsHelper.RegWindow.Show();
            this.Hide();
        }
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                login_btn_Click(sender, e);
        }
    }
}