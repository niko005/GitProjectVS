using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace StackLeader
{
    public class SearchAdmin
    {
        public int AdminID { get; set; }
        public string AdminName { get; set; }
        public bool IsAdmin { get; set; }

        public List<SearchAdmin> adminListData()
        {
            List<SearchAdmin> adminlistData = new List<SearchAdmin>();
            if (WindowsHelper.connect.State != ConnectionState.Open)
                WindowsHelper.connect.Open();
            try
            {
                string selectData = "SELECT EmployeeID, username, isadmin FROM users Where isadmin = 1 and delete_date IS NULL ";

                using (SqlCommand cmd = new SqlCommand(selectData, WindowsHelper.connect))
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        SearchAdmin sd = new SearchAdmin
                        {
                            AdminID = (int)reader["EmployeeID"],
                            AdminName = reader["username"].ToString(),
                            IsAdmin = (bool)reader["isadmin"]
                        };
                        adminlistData.Add(sd);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: "+ ex.Message);
                // Обработка ошибки
            }
            finally
            {
                WindowsHelper.connect.Close();
            }

            return adminlistData;
        }
    }
}
