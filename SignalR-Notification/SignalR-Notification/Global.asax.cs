using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SignalR_Notification
{
    public class MvcApplication : System.Web.HttpApplication
    {
        string con = ConfigurationManager.ConnectionStrings["SqlConString"].ConnectionString;
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            //here is application start we will start sql dependancy
            SqlDependency.Start(con);
        }
        protected void Session_Start(object sender, EventArgs e)
        {
            NotificationComponents nc = new NotificationComponents();
            var currentTime = DateTime.Now;
            HttpContext.Current.Session["LastUpdated"] = currentTime;
            nc.RegisterNotification(null);
        }
        protected void Application_End()
        {
            // here we will stop sql dependancy
            SqlDependency.Stop(con);
        }
    }
}
