using ChatAppMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChatAppMVC.Controllers
{
    public class RegisterController : Controller
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
            p.UserNick = p.UserName + p.UserSurname;
            p.UserCode = RandomString(8);
            var values = db.tblUsers.Where(x => x.UserMail == p.UserMail).FirstOrDefault();

            if (values == null)
            {
                db.tblUsers.Add(p);
                db.SaveChanges();
            }
            return View();
        }

        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}