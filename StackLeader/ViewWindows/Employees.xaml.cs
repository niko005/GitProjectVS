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
    public partial class Employees : Page
    {
        public Employees()
        {
            InitializeComponent();
            displayEmployeeData();
        }

        private void Click_Add_View_Employee(object sender, RoutedEventArgs e)
        {
            WindowsHelper.MainWindow.Frame_W.Content = WindowsHelper.AddEmployee;
            WindowsHelper.MainWindow.TitleText.Text = "Добавить сотрудника";
        }
        public void displayEmployeeData()
        {
            EmployeeData ed = new EmployeeData();
            List<EmployeeData> listData = ed.employeeListData();
            dataGridAddEmpl.ItemsSource = listData;
        }
        public void clearFields()
        {
            addEmployee_id.Text = "";
            addEmployee_email.Text = "";
            addEmployee_username.Text = "";
            addEmployee_password.Text = "";
            addEmployee_lastname.Text = "";
            addEmployee_firstname.Text = "";
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
        private void Click_Back(object sender, RoutedEventArgs e)
        {
            WindowsHelper.MainWindow.Frame_W.Content = null;
            WindowsHelper.MainWindow.Show();
            WindowsHelper.MainWindow.TitleText.Text = null;
        }
    }
}
