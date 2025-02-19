using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
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
using System.IO;
using Microsoft.Win32;

namespace StackLeader.ViewWindows
{
    public partial class AssignmentsListWindow : Page
    {
        private List<string> selectedFiles = new List<string>();
        private byte[] documentData; private string UpdateFilePath;

        public AssignmentsListWindow()
        {
            InitializeComponent();
            if (AuthorizationWindow.Username != null)
                displayTasksData();
            WindowsHelper.AddAssignments.LoadEmployees();
        }
        public void displayTasksData()
        {
            TaskData td = new TaskData();
            List<TaskData> TaskData = td.taskListData();
            dataGridViewTasks.ItemsSource = null;
            dataGridViewTasks.ItemsSource = TaskData;
        }

        private void AssignedToEmployee_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

/*            // Получение DataGrid, который вызвал событие
            TaskData selectedItem = dataGridViewTasks.SelectedItem as TaskData;

            ComboBox comboBox = sender as ComboBox;
            if (comboBox != null && dataGridViewTasks.SelectedItem is TaskData selectedTask)
            {
                if (comboBox.SelectedValue != null)
                {
                    int selectedNewStatusId = (int)comboBox.SelectedIndex + 1;
                    // Обновить запись в базе данных
                    UpdateTaskInDatabase(selectedTask.TaskID, selectedNewStatusId);
                    // Обновить свойство приоритета в объекте
                    selectedTask.StatusID = selectedNewStatusId;
                    // Отправка уведомления
                    if (WindowsHelper.connect.State != ConnectionState.Open)
                        WindowsHelper.connect.Open();
                    if (WindowsHelper.IsAdmin != true)
                    {
                        foreach (var adminid in WindowsHelper.admins)
                        {
                            WindowsHelper.AddAssignments.SendNotification(adminid.AdminID, $"Поручение {selectedItem.Title}  {selectedItem.AssignedToEmployee}");
                        }
                    }
                    RefreshTasksData();
                }
            }*/
        }

        private void StatusComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Получение DataGrid, который вызвал событие
            TaskData selectedItem = dataGridViewTasks.SelectedItem as TaskData;

            ComboBox comboBox = sender as ComboBox;
            if (comboBox != null && dataGridViewTasks.SelectedItem is TaskData selectedTask)
            {
                if (comboBox.SelectedValue != null)
                {
                    int selectedNewStatusId = (int)comboBox.SelectedIndex+1;
                    // Обновить запись в базе данных
                    UpdateTaskStatus(selectedTask.TaskID, selectedNewStatusId, selectedTask.CompletionDate);
                    // Обновить свойство приоритета в объекте
                    selectedTask.StatusID = selectedNewStatusId;
                    // Отправка уведомления
                    if (WindowsHelper.connect.State != ConnectionState.Open)
                        WindowsHelper.connect.Open();
                    if (WindowsHelper.IsAdmin != true)
                    {
                        foreach (var adminid in WindowsHelper.admins)
                        {
                            WindowsHelper.AddAssignments.SendNotification(adminid.AdminID, $"Поручение {selectedItem.Title} выполнено. {selectedItem.AssignedToEmployee}");
                        }
                    }
                    RefreshTasksData();
                }
            }
        }

        private void UpdateTaskInDatabase(int taskId, int newPriorityId)
        {
 /*           // Метод для обновления назначенного сотрудника в базе данных
            string updateQuery = "UPDATE Tasks SET AssignedToEmployee = @AssignedToEmployee WHERE TaskID = @TaskID";
            using (SqlCommand cmd = new SqlCommand(updateQuery, WindowsHelper.connect))
            {
                cmd.Parameters.AddWithValue("@AssignedToEmployee", taskId);
                cmd.Parameters.AddWithValue("@TaskID", task.TaskID);
                if (WindowsHelper.connect.State != ConnectionState.Open)
                    WindowsHelper.connect.Open();
                cmd.ExecuteNonQuery();
                WindowsHelper.connect.Close();
            }*/
        }
        private void UpdateTaskStatus(int taskId, int newPriorityId, DateTime? CompletionDate)
        {
                if (WindowsHelper.connect.State != ConnectionState.Open)
                    WindowsHelper.connect.Open();

                string updateQuery = "UPDATE Tasks SET StatusID = @StatusID, CompletionDate = @CompletionDate WHERE TaskID = @TaskID";
                DateTime today = DateTime.Today;
                using (SqlCommand cmd = new SqlCommand(updateQuery, WindowsHelper.connect))
                {
                    cmd.Parameters.AddWithValue("@StatusID", newPriorityId);
                    cmd.Parameters.AddWithValue("@TaskID", taskId);
                    cmd.Parameters.AddWithValue("@CompletionDate", today);
                    cmd.ExecuteNonQuery();
                }
        }
        public void RefreshTasksData()
        {
            Dispatcher.Invoke(() =>
            {
                displayTasksData();
                WindowsHelper.AddAssignments.LoadEmployees();
            });
        }

        private void DownloadFileButton_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridViewTasks.SelectedItem is TaskData selectedTask && selectedTask.Document != null)
            {
                Microsoft.Win32.SaveFileDialog saveFileDialog = new Microsoft.Win32.SaveFileDialog();
                saveFileDialog.FileName = selectedTask.Documentname;
                if (saveFileDialog.ShowDialog() == true)
                {
                    File.WriteAllBytes(saveFileDialog.FileName, selectedTask.Document);
                    MessageBox.Show("Файл успешно сохранен!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                MessageBox.Show("Нет документа для загрузки.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void dataGridViewTasks_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Получение DataGrid, который вызвал событие
            DataGrid dataGrid = sender as DataGrid;

            TaskData selectedItem = dataGrid.SelectedItem as TaskData;
            if (selectedItem != null)
            {
                // Определение позиции нажатия правой кнопки мыши
                Point position = e.GetPosition(dataGrid);
                // Создание контекстного меню
                ContextMenu contextMenu = new ContextMenu();
                // Создание элементов контекстного меню

                if (WindowsHelper.IsAdmin == true)
                {
                    MenuItem menuItem2 = new MenuItem();
                    menuItem2.Header = "Удалить поручение";
                    menuItem2.Click += (s, args) => { MenuItem2_Click(selectedItem); }; // Передаем выбранный элемент как параметр в обработчик
                    contextMenu.Items.Add(menuItem2);
                }
                MenuItem menuItem1 = new MenuItem();
                menuItem1.Header = "Добавить файл";
                menuItem1.Click += (s, args) => { MenuItem1_Click(selectedItem); }; // Передаем выбранный элемент как параметр в обработчик


                // Добавление элементов в контекстное меню
                contextMenu.Items.Add(menuItem1);


                // Показ контекстного меню в указанной позиции
                dataGrid.ContextMenu = contextMenu;
                contextMenu.IsOpen = true;
            }
        }
        private void MenuItem1_Click(TaskData selectedItem)
        {
            try
            {
                if (WindowsHelper.connect.State != ConnectionState.Open)
                    WindowsHelper.connect.Open();

                // Выбираем файл для обновления
                UpdateFilesButton_Click();

                if (documentData != null)
                {
                    string updateTaskFile;

                    if (selectedItem.DocumentID != null)
                    {
                        // Если у выбранной задачи уже есть документ, выполняем обновление
                        updateTaskFile = "UPDATE Documents SET Document = @document, DocumentName = @documentname " +
                                         "WHERE DocumentID = @documentid"; // добавляем DocumentID
                    }
                    else
                    {
                        // Если у выбранной задачи нет документа, выполняем вставку нового документа
                        updateTaskFile = "INSERT INTO Documents(Document, DocumentName) VALUES(@document, @documentname); " +
                                         "DECLARE @documentID INT; " +
                                         "SET @documentID = SCOPE_IDENTITY(); " +
                                         "UPDATE Tasks SET DocumentID = @documentID WHERE TaskID = @taskId";
                    }
                    using (SqlCommand cmd = new SqlCommand(updateTaskFile, WindowsHelper.connect))
                    {
                        cmd.Parameters.AddWithValue("@document", documentData ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@documentname", UpdateFilePath);

                        // Добавляем параметр @documentid, если он есть
                        if (selectedItem.DocumentID != null)
                        {
                            cmd.Parameters.AddWithValue("@documentid", selectedItem.DocumentID);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@taskId", selectedItem.TaskID);
                        }

                        cmd.ExecuteNonQuery();

                        displayTasksData();
                        MessageBox.Show("Файл успешно изменен!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error Message", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }




        private void MenuItem2_Click(TaskData selectedItem)
        {
            try
            {
                if (selectedItem != null && selectedItem.DocumentID != null)
                {
                    if (WindowsHelper.connect.State != ConnectionState.Open)
                        WindowsHelper.connect.Open();

                    string deleteFileQuery = "DELETE FROM Documents WHERE DocumentID = @documentid";
                    using (SqlCommand cmd = new SqlCommand(deleteFileQuery, WindowsHelper.connect))
                    {
                        cmd.Parameters.AddWithValue("@documentid", selectedItem.DocumentID);
                        cmd.ExecuteNonQuery();

                        // Удаление файла прошло успешно, теперь обновляем DocumentID
                        selectedItem.Document = null;
                        selectedItem.Documentname = null;
                        WindowsHelper.AssignmentsListWindow.displayTasksData();
                        MessageBox.Show("Файл успешно удален!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error Message", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void UpdateFilesButton_Click()
            {
            if (WindowsHelper.connect.State != ConnectionState.Open)
                WindowsHelper.connect.Open(); 
            OpenFileDialog openFileDialog = new OpenFileDialog
                {
                    Multiselect = true,
                    Filter = "All files (*.*)|*.*"
                };

                if (openFileDialog.ShowDialog() == true)
                {
                    selectedFiles.Clear();
                    selectedFiles.AddRange(openFileDialog.FileNames);
                    UpdateFilePath = string.Join(", ", openFileDialog.SafeFileNames);

                    // Чтение содержимого выбранных файлов в массив байтов
                    if (selectedFiles.Count > 0)
                    {
                        using (MemoryStream memoryStream = new MemoryStream())
                        {
                            foreach (var filePath in selectedFiles)
                            {
                                FileInfo fileInfo = new FileInfo(filePath);
                                if (fileInfo.Length > 40 * 1024 * 1024) // 40 MB
                                {
                                    MessageBox.Show($"Файл {fileInfo.Name} превышает максимальный размер 50MB и не будет добавлен.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                                    documentData = null;
                                    return;
                                }
                                byte[] fileData = File.ReadAllBytes(filePath);
                                memoryStream.Write(fileData, 0, fileData.Length);
                            }
                            documentData = memoryStream.ToArray();
                        }
                    }
                }
            }

        private void CheckBox_Checked(object sender, EventArgs e)
        {
            displayTasksData();
        }
        private void CheckBox_Unchecked(object sender, EventArgs e)
        {
            displayTasksData();
        }
        private void CheckBox_AllTask_Checked(object sender, EventArgs e)
        {
            displayTasksData();
        }
        public void LoadEmployees()
        {
            EmployeeData employeeData = new EmployeeData();
            List<EmployeeData> employees = employeeData.employeeListData();
            AssignedToComboBox.ItemsSource = employees;
            AssignedToComboBox.DisplayMemberPath = "FullName"; // Отображаемое имя (можно настроить)
        }
        private void AssignetToComboBox_Click(object sender, SelectionChangedEventArgs e)
        {
            displayTasksData();
        }
        
        private void CheckBox_AllTask_Unchecked(object sender, EventArgs e)
        {
            displayTasksData();
        }

    }
}