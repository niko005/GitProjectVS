using StackLeader.ViewWindows;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackLeader
{
    internal class NotificationData
    {
        public int NotificationID { get; set; }
        public int NotificationsToEmployeeID { get; set; }
        public DateTime? Date { get; set; }
        public string Message { get; set; }
        bool IsRead { get; set; }
        private string selectData;
        public List<NotificationData> notificationListData()
        {if (WindowsHelper.connect.State != ConnectionState.Open)
                WindowsHelper.connect.Open(); 
            List<NotificationData> notificationDatas = new List<NotificationData>();
            try{selectData = "SELECT * FROM Notifications Where NotificationsToEmployeeID=@UserID AND IsRead=0";
                using (SqlCommand cmd = new SqlCommand(selectData, WindowsHelper.connect))
                {cmd.Parameters.AddWithValue("@UserID", AuthorizationWindow.UserID);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {while (reader.Read())
                        {NotificationData nd = new NotificationData();
                            nd.NotificationID = (int)reader["NotificationID"];
                            nd.NotificationsToEmployeeID = (int)reader["NotificationsToEmployeeID"];
                            nd.Message = reader["Message"].ToString();
                            nd.IsRead = reader.GetBoolean(reader.GetOrdinal("IsRead"));
                            nd.Date = reader["CreatedAt"] != DBNull.Value ? (DateTime?)reader["CreatedAt"] : null;
                            notificationDatas.Add(nd);
                        }}}}
            catch (Exception ex)
            {Console.WriteLine("Error: " + ex.Message);}
            return notificationDatas;
        }
    }
}