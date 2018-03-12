using SignalR_Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SignalR_Notification.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetNotificationContacts()
        {
            //var notificationRegisterTime = Session["LastUpdated"] != null ? Convert.ToDateTime(Session["LastUpdated"]) : DateTime.Now;
            NotificationComponents nc = new NotificationComponents();
            List<Notifications> list = new List<Notifications>();
            //if(Session["LastUpdated"] == null)
                list = nc.GetNotifications(null);
            //else
            //    list = nc.GetNotifications(Convert.ToDateTime(Session["LastUpdated"]));
            //update session here for get only new added contacts (notification)
            Session["LastUpdated"] = DateTime.Now;
            return new JsonResult { Data = list, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}