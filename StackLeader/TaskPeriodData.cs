using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace StackLeader
{
    internal class TaskPeriodData
    {
        public int TaskID { set; get; }
        public string Title { set; get; }
        public string Description { set; get; }
        public string AssignedToEmployee { set; get; }
        public string Status { set; get; }
        public string Priority {  set; get; }
        public DateTime? Deadline { set; get; }
        public DateTime? Date_register { set; get; }
        public int StatusID, PriorityID;
        public string selectData;

        public List<TaskPeriodData> GetCompletedTasksForPeriod(DateTime startDate, DateTime endDate)
        {
            List<TaskPeriodData> taskDatas = new List<TaskPeriodData>();

            if (WindowsHelper.connect.State != ConnectionState.Open)
                WindowsHelper.connect.Open();

            try
            {
                if (WindowsHelper.ReportPage.AllEmployeeReport_Check.IsChecked == true)
                {
                    selectData = "SELECT Tasks.TaskID, Tasks.Title, Tasks.Description, " +
                                "users.lastname + ' ' + users.firstname AS AssignedToEmployee, " +
                                "Statuses.StatusName AS Status, Tasks.Date_register, Tasks.Deadline, Priorities.PriorityName as Priority " +
                                "FROM Tasks " +
                                "INNER JOIN users ON Tasks.AssignedToEmployee = users.EmployeeID " +
                                "INNER JOIN Statuses ON Tasks.StatusID = Statuses.StatusID " +
                                "INNER JOIN Priorities ON Tasks.PriorityID = Priorities.PriorityID "+
                                "WHERE Tasks.StatusID = @StatusID AND Tasks.Deadline >= @StartDate AND Tasks.Deadline <= @EndDate AND Priorities.PriorityID = @PriorityID";

                    StatusID = WindowsHelper.ReportPage.StatusComboBox.SelectedIndex + 1;
                    PriorityID = WindowsHelper.ReportPage.PriorityComboBox.SelectedIndex + 1;

                    using (SqlCommand cmd = new SqlCommand(selectData, WindowsHelper.connect))
                    {
                        cmd.Parameters.AddWithValue("@StartDate", startDate);
                        cmd.Parameters.AddWithValue("@EndDate", endDate);
                        cmd.Parameters.AddWithValue("@StatusID", StatusID);
                        cmd.Parameters.AddWithValue("@PriorityID", PriorityID);

                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            TaskPeriodData tpd = new TaskPeriodData();
                            tpd.TaskID = (int)reader["TaskID"];
                            tpd.Title = reader["Title"].ToString();
                            tpd.Description = reader["Description"].ToString();
                            tpd.AssignedToEmployee = reader["AssignedToEmployee"].ToString();
                            tpd.Status = reader["Status"].ToString();
                            tpd.Priority = reader["Priority"].ToString();
                            tpd.Deadline = reader["Deadline"] != DBNull.Value ? (DateTime?)reader["Deadline"] : null;
                            tpd.Date_register = reader["Date_register"] != DBNull.Value ? (DateTime?)reader["Date_register"] : null;
                            taskDatas.Add(tpd);
                        }
                    }
                }
                else
                {
                    EmployeeData assignedTo = (EmployeeData)WindowsHelper.ReportPage.AssignedToComboBox.SelectedItem;
                    int assignedToID = assignedTo.EmployeeID;

     

                    selectData = "SELECT Tasks.TaskID, Tasks.Title, Tasks.Description, users.lastname + ' ' + users.firstname AS AssignedToEmployee,\r\nStatuses.StatusName AS Status, Tasks.Date_register, Tasks.Deadline, Priorities.PriorityName as Priority,\r\nTasks.AssignedToEmployee AS AssignedToID FROM Tasks INNER JOIN users ON Tasks.AssignedToEmployee = users.EmployeeID\r\nINNER JOIN Statuses ON Tasks.StatusID = Statuses.StatusID\r\nINNER JOIN Priorities ON Tasks.PriorityID = Priorities.PriorityID\r\nWHERE Tasks.StatusID = @StatusID AND Tasks.Deadline >= @StartDate  \r\nAND Tasks.Deadline <= @EndDate AND Tasks.AssignedToEmployee = @AssignedToID AND Priorities.PriorityID = @PriorityID";

                    StatusID = WindowsHelper.ReportPage.StatusComboBox.SelectedIndex + 1;
                    PriorityID = WindowsHelper.ReportPage.PriorityComboBox.SelectedIndex + 1;

                    using (SqlCommand cmd = new SqlCommand(selectData, WindowsHelper.connect))
                    {
                        cmd.Parameters.AddWithValue("@StartDate", startDate);
                        cmd.Parameters.AddWithValue("@EndDate", endDate);
                        cmd.Parameters.AddWithValue("@StatusID", StatusID);
                        cmd.Parameters.AddWithValue("@PriorityID", PriorityID);
                        cmd.Parameters.AddWithValue("@AssignedToID", assignedToID);

                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            TaskPeriodData tpd = new TaskPeriodData();
                            tpd.TaskID = (int)reader["TaskID"];
                            tpd.Title = reader["Title"].ToString();
                            tpd.Description = reader["Description"].ToString();
                            tpd.AssignedToEmployee = reader["AssignedToEmployee"].ToString();
                            tpd.Status = reader["Status"].ToString();
                            tpd.Priority = reader["Priority"].ToString();
                            tpd.Deadline = reader["Deadline"] != DBNull.Value ? (DateTime?)reader["Deadline"] : null;
                            tpd.Date_register = reader["Date_register"] != DBNull.Value ? (DateTime?)reader["Date_register"] : null;
                            taskDatas.Add(tpd);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex);
            }
            finally
            {
                WindowsHelper.connect.Close();
            }

            return taskDatas;
        }
    }
}
