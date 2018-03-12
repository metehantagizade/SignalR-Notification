using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using Microsoft.AspNet.SignalR;
using SignalR_Entity;

namespace SignalR_Notification
{
    public class NotificationComponents
    {
        public void RegisterNotification(DateTime? currentTime)
        {
            string conStr = ConfigurationManager.ConnectionStrings["SqlConString"].ConnectionString;
            string sqlCommand = "";
            if (currentTime != null)
                sqlCommand = @"select [SenderUserId],[ReceiverUserId],[Content] from [NTF].[Notifications] where [SendDate] > @SendDate";
            else
                sqlCommand = @"select [SenderUserId],[ReceiverUserId],[Content] from [NTF].[Notifications]";

            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand cmd = new SqlCommand(sqlCommand, con);
                if(currentTime != null)
                    cmd.Parameters.AddWithValue("@SendDate", currentTime);
                if (con.State != System.Data.ConnectionState.Open)
                {
                    con.Open();
                }
                cmd.Notification = null;
                SqlDependency sqlDep = new SqlDependency(cmd);
                sqlDep.OnChange += SqlDep_OnChange;
                // we must have to execute the command here
                using(SqlDataReader reader = cmd.ExecuteReader())
                {
                    // noting need to add here now
                }
            }
        }

        private void SqlDep_OnChange(object sender, SqlNotificationEventArgs e)
        {
            if (e.Type == SqlNotificationType.Change)
            {
                SqlDependency sqlDep = sender as SqlDependency;
                sqlDep.OnChange -= SqlDep_OnChange;
                // from here we will send notification message to client
                var notificationHub = GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
                notificationHub.Clients.All.notify("Added");

                //re-register notification
                RegisterNotification(DateTime.Now);

            }
        }
        public  List<Notifications> GetNotifications(DateTime? afterDate)
        {
            using (SignalRNotificationEntities dc= new SignalRNotificationEntities())
            {
                //return dc.Notifications.Where(s => s.SendDate > afterDate).OrderByDescending(s => s.SendDate).ToList();
                //if(afterDate == null)
                    return dc.Notifications.OrderByDescending(s => s.SendDate).ToList();
                //else
                //    return dc.Notifications.Where(s => s.SendDate > afterDate).OrderByDescending(s => s.SendDate).ToList();
            }
        }
    }
}