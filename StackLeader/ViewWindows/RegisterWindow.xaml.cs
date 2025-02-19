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

namespace StackLeader.ViewWindows
{
    public partial class RegisterWindow : Window
    {
        private const string correctAdminKey = "5105"; // Замените на реальный привилегированный ключ
        public RegisterWindow()
        {
            InitializeComponent();
        }
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
        public void exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown(); 
            WindowsHelper.connect.Close();
        }

        private void signup_btn_Click(object sender, RoutedEventArgs e)
        {
            string username = signup_username.Text, password = signup_password.Password, email = signup_email.Text, lastname = signup_lastname.Text,firstname = signup_firstname.Text, adminKey = signup_adminKey.Password;
            bool isAdmin = signup_isadmin.IsChecked ?? false;
            SaveRegistrationUser(username,password,email,lastname,firstname,isAdmin,adminKey);
                // Вызов метода авторизации в окне авторизации
                WindowsHelper.AuthorizationWindow.login_btn_Click(this, new RoutedEventArgs());
                // Закрытие окна регистрации
                this.Close();
        }
        public void SaveRegistrationUser(string username, string password, string email, string lastname, string firstname, bool isAdmin, string adminKey)
        {
            try
            {
                if (WindowsHelper.connect.State != ConnectionState.Open)
                    WindowsHelper.connect.Open();

                if (isAdmin && adminKey != correctAdminKey)
                {MessageBox.Show("Неправильный привилегированный ключ!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                 return;
                }

                string query = "INSERT INTO users (username, password, email, lastname, firstname, isadmin, insert_date) " +
                    "VALUES (@Username, @Password, @Email, @Lastname, @Firstname, @IsAdmin, @InsertDate)";
                SqlCommand cmd = new SqlCommand(query, WindowsHelper.connect);
                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@Password", WindowsHelper.HashPassword(password));
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Lastname", lastname);
                cmd.Parameters.AddWithValue("@Firstname", firstname);
                cmd.Parameters.AddWithValue("@IsAdmin", isAdmin);
                cmd.Parameters.AddWithValue("@InsertDate", DateTime.Now);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Пользователь успешно зарегистрирован!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                WindowsHelper.AuthorizationWindow.login_username.Text = username;
                WindowsHelper.AuthorizationWindow.login_password.Password = password;
            }
            catch (Exception ex)
            {MessageBox.Show("Error: " + ex.Message, "Error Message", MessageBoxButton.OK, MessageBoxImage.Error);}
            finally
            {WindowsHelper.connect.Close();}
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void login_btn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void signup_loginBtn_Click(object sender, RoutedEventArgs e)
        {
            WindowsHelper.AuthorizationWindow.Show();
            this.Hide();
        }
        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
