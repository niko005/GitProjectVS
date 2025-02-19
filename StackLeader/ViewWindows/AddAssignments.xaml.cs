using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
using System.Net;
using System.Net.Mail;
using Microsoft.Win32;
using System.IO;

namespace StackLeader.ViewWindows
{
    public partial class AddAssignments : Page
    {
        private byte[] documentData;
        private List<string> selectedFiles = new List<string>();

        public AddAssignments()
        {
            InitializeComponent();
            LoadEmployees();
            StatusComboBox.SelectedIndex = 0;
            SetCurrentTimeAsDefault();
        }
        public void LoadEmployees()
        {
            EmployeeData employeeData = new EmployeeData();
            List<EmployeeData> employees = employeeData.employeeListData();
            AssignedToComboBox.ItemsSource = employees;
            AssignedToComboBox.DisplayMemberPath = "FullName"; // Отображаемое имя (можно настроить)
        }
        public void SetCurrentTimeAsDefault()
        {
            // Получить текущее время на 1 час больше
            DateTime now = DateTime.Now.AddHours(1);
            string currentTime = now.ToString("HH:00");
            DateTime currentDate = DateTime.Today.AddDays(1);
            // Найти и установить текущее время на 1 час больше в ComboBox
            DeadlineDatePicker.SelectedDate = currentDate;
            foreach (var item in TimeComboBox.Items)
            {
                if ((item as ComboBoxItem).Content.ToString() == currentTime)
                {
                    TimeComboBox.SelectedItem = item;
                    break;
                }
            }
        }
        private void SelectFilesButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Multiselect = true,
                Filter = "All files (*.*)|*.*"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                selectedFiles.Clear();
                selectedFiles.AddRange(openFileDialog.FileNames);
                SelectedFilesTextBlock.Text = string.Join(", ", openFileDialog.SafeFileNames);

                // Чтение содержимого выбранных файлов в массив байтов
                if (selectedFiles.Count > 0)
                {
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        foreach (var filePath in selectedFiles)
                        {
                            byte[] fileData = File.ReadAllBytes(filePath);
                            memoryStream.Write(fileData, 0, fileData.Length);
                        }documentData = memoryStream.ToArray();
                    }
                }
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // Получение данных из формы
            string title = TitleTextBox.Text.Trim();
            string description = DescriptionTextBox.Text.Trim();
            EmployeeData assignedTo = (EmployeeData)AssignedToComboBox.SelectedItem;
            int assignedToID = assignedTo.EmployeeID;
            string priority = ((ComboBoxItem)PriorityComboBox.SelectedItem)?.Content.ToString();
            int priorityID = PriorityComboBox.SelectedIndex + 1;
            string status = ((ComboBoxItem)StatusComboBox.SelectedItem)?.Content.ToString();
            int statusID = StatusComboBox.SelectedIndex + 1;
            DateTime? selectedDate = DeadlineDatePicker.SelectedDate;
            string selectedTime = (TimeComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();

            if (selectedDate.HasValue && !string.IsNullOrEmpty(selectedTime))
            {
                string[] timeParts = selectedTime.Split(':');
                int hours = int.Parse(timeParts[0]);
                int minutes = int.Parse(timeParts[1]);

                DateTime deadline = new DateTime(
                    selectedDate.Value.Year,
                    selectedDate.Value.Month,
                    selectedDate.Value.Day,
                    hours,
                    minutes,
                    0); // Секунды устанавливаем в 0

                // Проверка на заполненность обязательных полей
                if (string.IsNullOrEmpty(title) || assignedTo == null || priority == null || deadline == null)
                {
                    MessageBox.Show("Пожалуйста, заполните все обязательные поля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                MessageBoxResult messageBoxResult = MessageBox.Show("Вы уверены, что хотите добавить поручение?", "Confirmation Message", MessageBoxButton.YesNo, MessageBoxImage.Information);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    try
                    {
                        if (WindowsHelper.connect.State != ConnectionState.Open)
                            WindowsHelper.connect.Open();
                        DateTime today = DateTime.Today;
                        string updateTask = "";
                        if (documentData != null)
                            updateTask = @"INSERT INTO Documents(Document, DocumentName) VALUES(@document, @document_name);
                                 DECLARE @documentID INT;
                                 SET @documentID = SCOPE_IDENTITY();
                                 INSERT INTO Tasks (Title, Description, AssignedToEmployee, Date_register, PriorityID, Deadline, StatusID, DocumentID)
                                 VALUES(@title, @description, @assignedTo, @date_register, @priorityID, @deadline, @statusID, @documentID)";
                        else
                        {
                            updateTask = @"INSERT INTO Tasks (Title, Description, AssignedToEmployee, Date_register, PriorityID, Deadline, StatusID)
                                 VALUES(@title, @description, @assignedTo, @date_register, @priorityID, @deadline, @statusID)";
                        }
                        using (SqlCommand cmd = new SqlCommand(updateTask, WindowsHelper.connect))
                        {
                            cmd.Parameters.AddWithValue("@title", title);
                            cmd.Parameters.AddWithValue("@description", description);
                            cmd.Parameters.AddWithValue("@assignedTo", assignedToID);
                            cmd.Parameters.AddWithValue("@date_register", today);
                            cmd.Parameters.AddWithValue("@priorityID", priorityID);
                            cmd.Parameters.AddWithValue("@deadline", deadline);
                            cmd.Parameters.AddWithValue("@statusID", statusID);

                            if (documentData != null)
                            {
                                cmd.Parameters.AddWithValue("@document", documentData);
                                cmd.Parameters.AddWithValue("@document_name", SelectedFilesTextBlock.Text);
                            }
                            else
                            {
                                cmd.Parameters.Add("@document", SqlDbType.VarBinary).Value = DBNull.Value;
                                cmd.Parameters.Add("@document_name", SqlDbType.NVarChar).Value = DBNull.Value;
                            }

                            cmd.ExecuteNonQuery();

                            WindowsHelper.AssignmentsListWindow.displayTasksData();

                            // Отправка уведомления по электронной почте
                            string emailSubject = "Новое поручение";
                            string emailBody = $"Уважаемый {assignedTo.Firstname},\n\n" +
                                               $"Вам назначено новое поручение: {title}\n\n" +
                                               $"Описание: {description}\n" +
                                               $"Приоритет: {priority}\n" +
                                               $"Срок выполнения: {deadline}\n\n" +
                                               $"С уважением,\nВаша команда.";

                            SendEmailNotification(AuthorizationWindow.EmailReg, "nnbRAjvdv07UrPBKkdj3", assignedTo.Email, emailSubject, emailBody, selectedFiles);
                            WindowsHelper.MainWindow.UpdateUnreadNotificationsCount();
                            MessageBox.Show("Поручение успешно сохранено!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                            clearFields();

                            // Отправка уведомления
                            if (WindowsHelper.connect.State != ConnectionState.Open)
                                WindowsHelper.connect.Open();
                            SendNotification(assignedToID, $"Новое поручение: {title}");
                            SetCurrentTimeAsDefault();
                            documentData = null;
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
                    MessageBox.Show("Добавление отменено", "Information Message", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                clearFields();
            }
        }

        public void clearFields()
        {
            // Очистка полей после сохранения
            TitleTextBox.Clear();
            DescriptionTextBox.Clear();
            AssignedToComboBox.SelectedIndex = -1;
            PriorityComboBox.SelectedIndex = -1;
            DeadlineDatePicker.SelectedDate = null;
            selectedFiles.Clear();
            SelectedFilesTextBlock.Text = string.Empty;
        }
        public void SendNotification(int userID, string message)
        {
            // код для сохранения уведомления в базу данных или отправки его другим способом
            // Пример:
            string insertNotification = "INSERT INTO Notifications (NotificationsToEmployeeID, Message, CreatedAt) VALUES (@userID, @message, @date)";
            using (SqlCommand cmd = new SqlCommand(insertNotification, WindowsHelper.connect))
            {
                cmd.Parameters.AddWithValue("@userID", userID);
                cmd.Parameters.AddWithValue("@message", message);
                cmd.Parameters.AddWithValue("@date", DateTime.Now);

                cmd.ExecuteNonQuery();
            }
        }
        public void SendEmailNotification(string fromEmail, string fromPassword, string toEmail, 
            string subject, string body, List<string> attachments)
        {
            try
            {
                var fromAddress = new MailAddress(fromEmail);
                var toAddress = new MailAddress(toEmail);
                var smtp = new SmtpClient
                {
                    Host = string.Format("smtp.{0}", AuthorizationWindow.SmtpEmail),
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                };

                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body
                })
                {
                    // Добавление вложений
                    foreach (var attachmentPath in attachments)
                    {
                        if (File.Exists(attachmentPath))
                        {
                            Console.WriteLine("Добавление вложения: " + attachmentPath);
                            var attachment = new Attachment(attachmentPath);
                            message.Attachments.Add(attachment);
                        }
                        else
                        {Console.WriteLine("Файл не найден: " + attachmentPath);}
                    }
                    smtp.Send(message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка при отправке письма: " + ex.Message);
            }
        }
    }
}
