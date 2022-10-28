using ChatAppMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ChatAppMVC.Controllers
{
    public class LoginController : Controller
    {
        dbChatBoxEntities db = new dbChatBoxEntities();

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(tblUser p)
        {
            var values = db.tblUsers.FirstOrDefault(x => x.UserMail == p.UserMail && x.UserPassword == p.UserPassword);
            if (values != null)
            {
                FormsAuthentication.SetAuthCookie(values.UserMail,false);
                Session["UserMail"] = p.UserMail;
                return RedirectToAction("Index","Main");
            }
            else
            {
                return RedirectToAction("Index","Register");
            }
         
        }
    }
}