using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackLeader;
using System.Threading;
using StackLeader.ViewWindows;
using System.Windows.Media;
using System.Data.SqlClient;
using System.Data;
using System.Security.Cryptography;

namespace StackLeader
{
    public class WindowsHelper
    {
        public static int CountDelEmployees=1;
        //public static SqlConnection connect = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=StackLeaderDB;User ID=sa;Password=89285144800;Connect Timeout=30");
        public static SqlConnection connect = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=StackLeaderDB;Integrated Security=True");
        public static Timer timer;
        public static System.Windows.Threading.DispatcherTimer timernotification;
        public static HomePage HomeWindow = new HomePage();
        public static RegisterWindow RegWindow = new RegisterWindow();
        public static AuthorizationWindow AuthorizationWindow = new AuthorizationWindow();
        public static Employees Employees = new Employees();
        public static PersonalAccount PersonalAccount = new PersonalAccount();
        public static MainWindow MainWindow = new MainWindow();
        public static AddEmployee AddEmployee = new AddEmployee();      
        public static AddAssignments AddAssignments = new AddAssignments();
        public static AssignmentsListWindow AssignmentsListWindow = new AssignmentsListWindow();
        public static ReportPage ReportPage = new ReportPage();
        public static NotificationWindow NotificationWindow = new NotificationWindow();
        public static int AutorizationUserID;
        public static bool IsAdmin;
        public static List<SearchAdmin> admins;

        static WindowsHelper()
        {
            timernotification = new System.Windows.Threading.DispatcherTimer();
            timernotification.Interval = TimeSpan.FromSeconds(10); // Устанавливаем интервал обновления в 10 секунд
            timernotification.Tick += Timer_Tick;
        }

        private static void Timer_Tick(object sender, EventArgs e)
        {
            if (connect.State != ConnectionState.Open)
                connect.Open();
            MainWindow?.UpdateUnreadNotificationsCount();
        }

        public static string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {// Преобразуем строку в массив байтов
                byte[] inputBytes = Encoding.ASCII.GetBytes(password);
                // Вычисляем хэш
                byte[] hashBytes = sha256.ComputeHash(inputBytes);
                // Преобразуем байты хэша в шестнадцатеричное представление
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }

    }
}
