using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChatAppMVC.Controllers
{
    public class MainLayoutController : Controller
    {
        // GET: MainLayout
        public ActionResult Index()
        {
            return View();
        }
        public PartialViewResult PartialHead()
        {
            return PartialView();
        }
        public PartialViewResult PartialLeft()
        {
            return PartialView();
        }
        public PartialViewResult PartialRight()
        {
            return PartialView();
        }
        public PartialViewResult SubPartialLeftMessageBox()
        {
            return PartialView();
        }
    }
}