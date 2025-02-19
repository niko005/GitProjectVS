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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace StackLeader.ViewWindows
{
    /// <summary>
    /// Логика взаимодействия для PersonalAccount.xaml
    /// </summary>
    public partial class PersonalAccount : Page
    {
        public PersonalAccount()
        {
            InitializeComponent();
        }

        private void ViewMyListAssignmetsBtn(object sender, RoutedEventArgs e)
        {
            WindowsHelper.MainWindow.TitleText.Text = "Список поручения";
            WindowsHelper.MainWindow.Frame_W.Content = WindowsHelper.AssignmentsListWindow;
            WindowsHelper.AssignmentsListWindow.displayTasksData();
        }
    }
}
