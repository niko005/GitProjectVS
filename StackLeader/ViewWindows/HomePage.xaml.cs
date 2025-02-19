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
    public partial class HomePage : Page
    {
        public HomePage()
        {
            InitializeComponent();
        }
        private void AddAssignmentsBtn(object sender, RoutedEventArgs e)
        {
            WindowsHelper.AddAssignments.LoadEmployees();
            WindowsHelper.MainWindow.TitleText.Text = "Добавить поручение";
            WindowsHelper.MainWindow.Frame_W.Content = WindowsHelper.AddAssignments;
        }

        private void ViewListAssignmetsBtn(object sender, RoutedEventArgs e)
        {
            WindowsHelper.MainWindow.TitleText.Text = "Список поручения";
            WindowsHelper.MainWindow.Frame_W.Content = WindowsHelper.AssignmentsListWindow;
            WindowsHelper.AssignmentsListWindow.LoadEmployees();
            WindowsHelper.AssignmentsListWindow.displayTasksData();
            WindowsHelper.AddAssignments.SetCurrentTimeAsDefault();
        }
        private void ReportPageBtn(object sender, RoutedEventArgs e)
        {
            WindowsHelper.MainWindow.TitleText.Text = "Отчет";
            WindowsHelper.MainWindow.Frame_W.Content = WindowsHelper.ReportPage;
            WindowsHelper.AddAssignments.LoadEmployees();
            WindowsHelper.AssignmentsListWindow.displayTasksData();
            WindowsHelper.AddAssignments.SetCurrentTimeAsDefault();
        }
    }
}
