using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace StackLeader
{
    internal class EmployeeData
    {
        public int EmployeeID { get; set; } // 1
        public string Email { get; set; } // 2
        public string Username { get; set; } // 3
        public string Password { get; set; } // 4
        public string Lastname { get; set; } // 5
        public string Firstname { get; set; } // 6
        public bool IsAdmin { get; set; } // 7
        public string FullName => $"{Lastname} {Firstname} ({Username})";

        public List<EmployeeData> employeeListData()
        {
            List<EmployeeData> listData = new List<EmployeeData>();

            if (WindowsHelper.connect.State != ConnectionState.Open)
                WindowsHelper.connect.Open();

            try
            {
                string selectData = "SELECT EmployeeID, email, username, password, lastname, " +
                    "firstname, isadmin FROM users WHERE delete_date IS NULL";

                using (SqlCommand cmd = new SqlCommand(selectData, WindowsHelper.connect))
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        EmployeeData ed = new EmployeeData
                        {
                            EmployeeID = (int)reader["EmployeeID"],
                            Email = reader["email"].ToString(),
                            Username = reader["username"].ToString(),
                            Password = WindowsHelper.HashPassword(reader["password"].ToString()),
                            Lastname = reader["lastname"].ToString(),
                            Firstname = reader["firstname"].ToString(),
                            IsAdmin = (bool)reader["isadmin"]
                        };
                        listData.Add(ed);
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
            return listData;
        }
    }
}