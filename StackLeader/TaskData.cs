using StackLeader.ViewWindows;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace StackLeader
{
    internal class TaskData
    {
        public int TaskID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Priority { get; set; }
        public string AssignedToEmployee { get; set; }
        public string Status { get; set; }
        public DateTime? Deadline { get; set; }
        public DateTime? CompletionDate { get; set; }
        public int StatusID { get; set; }
        public int PriorityID { get; set; }
        public byte[] Document { get; set; }
        public string Documentname { get; set; }
        public int? DocumentID { get; set; }
        private string selectData;
        private int assignedToID;
        public bool HasDocument { get; set; }

        public List<TaskData> taskListData()
        {
            List<TaskData> taskDatas = new List<TaskData>();
            if (WindowsHelper.connect.State != ConnectionState.Open)
                WindowsHelper.connect.Open();
            try
            {
                selectData = " SELECT Tasks.TaskID, Tasks.Title, Tasks.Description, " +
                             " users.Lastname + ' ' + users.Firstname AS AssignedToEmployee, " +
                             " Statuses.StatusName AS Status, Tasks.Deadline, Priorities.PriorityName as Priority, " +
                             " Tasks.PriorityID, Tasks.StatusID, " +
                             " Documents.DocumentName as DocumentName, Documents.Document as Document, Documents.DocumentID as DocumentID, CompletionDate " +
                             " FROM Tasks " +
                             " INNER JOIN users ON Tasks.AssignedToEmployee = users.EmployeeID " +
                             " INNER JOIN Statuses ON Tasks.StatusID = Statuses.StatusID " +
                             " INNER JOIN Priorities ON Tasks.PriorityID = Priorities.PriorityID " +
                             " FULL JOIN Documents ON Tasks.DocumentID = Documents.DocumentID " +
                             " WHERE Tasks.StatusID IS NOT NULL ";

                    if (WindowsHelper.AssignmentsListWindow.AllTasks_Check.IsChecked == false)
                    {
                        selectData += "AND Tasks.StatusID <> 3 ";
                    }

                    if (WindowsHelper.IsAdmin == false)
                    {
                        selectData += " AND users.EmployeeID = @userId";
                        WindowsHelper.AssignmentsListWindow.AllEmployeeTask_Check.Visibility = System.Windows.Visibility.Hidden;
                        WindowsHelper.AssignmentsListWindow.AssignedToComboBox.Visibility = System.Windows.Visibility.Hidden;
                        WindowsHelper.AssignmentsListWindow.NotAdminHiddenCombobox.Visibility = System.Windows.Visibility.Hidden;
                    }
                    else if (WindowsHelper.MainWindow.Frame_W.Content == WindowsHelper.AssignmentsListWindow && WindowsHelper.AssignmentsListWindow.AllEmployeeTask_Check.IsChecked == false)
                    {
                        selectData += " AND users.EmployeeID = @EmployeeID";
                    }
                    using (SqlCommand cmd = new SqlCommand(selectData, WindowsHelper.connect))
                    {
                        if (WindowsHelper.IsAdmin == false)
                        {
                            cmd.Parameters.AddWithValue("@userId", AuthorizationWindow.UserID);
                        }
                        else if (WindowsHelper.AssignmentsListWindow.AssignedToComboBox.SelectedIndex != -1 && WindowsHelper.MainWindow.Frame_W.Content == WindowsHelper.AssignmentsListWindow && WindowsHelper.AssignmentsListWindow.AllEmployeeTask_Check.IsChecked == false)
                        {
                            EmployeeData assignedTo = (EmployeeData)WindowsHelper.AssignmentsListWindow.AssignedToComboBox.SelectedItem;
                            assignedToID = assignedTo.EmployeeID;
                            cmd.Parameters.AddWithValue("@EmployeeID", assignedToID);
                        }

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                TaskData td = new TaskData();
                                td.TaskID = (int)reader["TaskID"];
                                td.Title = reader["Title"].ToString();
                                td.Description = reader["Description"].ToString();
                                td.AssignedToEmployee = reader["AssignedToEmployee"].ToString();
                                td.Status = reader["Status"].ToString();
                                td.Priority = reader["Priority"].ToString();
                                td.Deadline = reader["Deadline"] != DBNull.Value ? (DateTime?)reader["Deadline"] : null;
                                td.CompletionDate = reader["CompletionDate"] != DBNull.Value ? (DateTime?)reader["CompletionDate"] : null;
                                td.PriorityID = (int)reader["PriorityID"];
                                td.Documentname = reader["DocumentName"].ToString();
                                td.StatusID = (int)reader["StatusID"];
                                td.DocumentID = reader["DocumentID"] != DBNull.Value ? (int?)reader["DocumentID"] : null;  // Проверка на DBNull
                                td.Document = reader["Document"] != DBNull.Value ? (byte[])reader["Document"] : null;
                                td.HasDocument = td.Document != null && td.Document.Length > 0;
                                taskDatas.Add(td);
                            }
                        }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                // Обработка ошибки
            }
            finally
            {
                WindowsHelper.connect.Close();
            }
            return taskDatas;
        }
    }
}