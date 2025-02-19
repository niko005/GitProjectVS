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

namespace StackLeader.ViewWindows
{

    public partial class NotificationWindow : Window
    {
        public NotificationWindow()
        {
            InitializeComponent();
        }

        private void listView_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                var listView = sender as ListView;
                if (listView != null)
                {
                    var selectedItem = listView.SelectedItem;
                    if (selectedItem != null)
                    {
                        WindowsHelper.MainWindow.Frame_W.Content = WindowsHelper.AssignmentsListWindow;
                        WindowsHelper.AssignmentsListWindow.displayTasksData();
                        this.Hide();
                    }
                }
            }
        }
    }
}
