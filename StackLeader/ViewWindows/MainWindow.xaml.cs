using System.Threading;
using System.Threading.Tasks;
using StackLeader.ViewWindows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.Remoting.Contexts;

namespace StackLeader
{
    public partial class MainWindow : Window
    {
        public int unreadCount; 
        public MainWindow()
        {
            InitializeComponent();
            WindowsHelper.timer = new Timer(DisplayCurrentTime, null, 0, 10000);
            /*Frame_W.Content = WindowsHelper.HomeWindow;
            Frame_WBottom.Background = new SolidColorBrush(WindowsHelper.MyFrameBottomColor);*/
        }

        private void Click_Notifications(object sender, RoutedEventArgs e)
        {
            displayNotificationsData();
            MarkNotificationsAsRead();
            UpdateUnreadNotificationsCount();
            WindowsHelper.NotificationWindow.Show();
            WindowsHelper.NotificationWindow.Deactivated += NotificationsWindow_Deactivated;
            WindowsHelper.NotificationWindow.Owner = this;
            this.Topmost = false;
        }
        private void NotificationsWindow_Deactivated(object sender, EventArgs e)
        {
            if (WindowsHelper.NotificationWindow != null)
            {
                WindowsHelper.NotificationWindow.Hide();
                this.Topmost = true;
                this.Topmost = false;
            }
        }
        //Изменяет сообщение на прочитанное
        public void MarkNotificationsAsRead()
        {if (WindowsHelper.connect.State != ConnectionState.Open)
                WindowsHelper.connect.Open();
            try
            {string updateIsRead = "UPDATE Notifications SET IsRead = 1 " +
                    "WHERE NotificationsToEmployeeID=@UserID AND IsRead = 0";
                using (SqlCommand cmd = new SqlCommand(updateIsRead, WindowsHelper.connect))
                {cmd.Parameters.AddWithValue("@UserID", AuthorizationWindow.UserID);
                    cmd.ExecuteNonQuery();}}
            catch (Exception ex)
            {Console.WriteLine("Error: " + ex.Message);}
            finally
            {WindowsHelper.connect.Close();}}
        //Метод для текста вывода количества уведомлений 
        public void UpdateUnreadNotificationsCount()
        {unreadCount = 0;
            if (WindowsHelper.connect.State != ConnectionState.Open)
                WindowsHelper.connect.Open();
            try
            {string selectUnreadCount = "SELECT COUNT(*) FROM Notifications " +
                    "WHERE NotificationsToEmployeeID=@UserID AND IsRead=0";
                using (SqlCommand cmd = new SqlCommand(selectUnreadCount, WindowsHelper.connect))
                {cmd.Parameters.AddWithValue("@UserID", AuthorizationWindow.UserID);
                    unreadCount = (int)cmd.ExecuteScalar();}}
            catch (Exception ex)
            {Console.WriteLine("Error: " + ex.Message);}
            finally
            {WindowsHelper.connect.Close();}
            UnreadNotificationsCount.Text = $"{unreadCount}";}
        //Метод для создания экземпляра класса вывода данных с БД
        public void displayNotificationsData()
        {NotificationData nd = new NotificationData();
            List<NotificationData> notificationsData = nd.notificationListData();
            var notificationsPage = WindowsHelper.NotificationWindow as NotificationWindow;
            if (notificationsPage != null)
                notificationsPage.NotificationsListView.ItemsSource = notificationsData;
            UpdateUnreadNotificationsCount();}

        public void SetWelcomeMessage(string message)
        {
            HelloUserText.Text = message;
        }
        
        public void DisplayCurrentTime(object state)
        {

            // Получаем текущее время
            DateTime currentTime = DateTime.Now;

            // Обновляем TextBlock в UI-потоке
            Dispatcher.Invoke(() =>
            {
                // Проверяем, что элемент управления доступен и не равен null
                if (dataText != null)
                {
                    // Устанавливаем текст в элемент управления
                    dataText.Text = currentTime.ToString("HH:mm");
                }
            });
        }

        //Метод перетаскивания окна при помощи мышки
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void Click_Home(object sender, RoutedEventArgs e)
        {
            TitleText.Text = "Главная";
            /*TitleText.Background = "#";*/
            Frame_W.Content = WindowsHelper.HomeWindow;
        }
        private void Click_Personal_Account(object sender, RoutedEventArgs e)
        {
            TitleText.Text = "Личный кабинет";
            Frame_W.Content = WindowsHelper.PersonalAccount;
        }
        
        private void Click_Employee(object sender, RoutedEventArgs e)
        {
            TitleText.Text = "Сотрудники";
            Frame_W.Content = WindowsHelper.Employees;
            WindowsHelper.Employees.displayEmployeeData();

        }
        private void Click_Exit(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
           WindowsHelper.connect.Close();
            /*this.Close();*/
        }
    }
}