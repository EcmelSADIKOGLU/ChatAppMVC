using ChatAppMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChatAppMVC.Controllers
{
    public class MainController : Controller
    {
        dbChatBoxEntities db = new dbChatBoxEntities();


        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            string mail = Session["UserMail"].ToString();
            var values = db.tblUsers.Where(x => x.UserMail == mail).FirstOrDefault();
            ViewBag.Friends = db.tblFriends.Where(x=> (x.FriendSender == values.UserID || x.FriendReceiver == values.UserID)&& x.FriendStatus == 2).ToList();
            return View(values);
        }
        public ActionResult MessageBox(int id)
        {
            string mail = Session["UserMail"].ToString();
            var values = db.tblUsers.Where(x => x.UserMail == mail).FirstOrDefault();
            ViewBag.Friends = db.tblFriends.Where(x => (x.FriendSender == values.UserID || x.FriendReceiver == values.UserID) && x.FriendStatus == 2).ToList();

            ViewBag.item = db.tblUsers.Find(id);
            var messages = db.tblMessages.Where(x => (x.MessageSender == id && x.MessageReceiver == values.UserID) || (x.MessageReceiver == id && x.MessageSender == values.UserID));
            ViewBag.messages = messages;
            ViewBag.unreadedMessage = db.tblMessages.Where(x => (x.MessageSender == id || x.MessageReceiver == id) && x.MessageStatus == false).Count();
            foreach (var item in messages)
            {

                item.MessageStatus = true;
            }

            
            return View(values);
        }
        [HttpPost]
        public ActionResult SendMessage()
        {
            string messageContent = Request["MessageContent"];
            string messageReceiver = Request["ReceiverID"];
            string messageSender = Request["SenderID"];

            var message = new tblMessage();
            message.MessageSender = Convert.ToInt32(messageSender);
            message.MessageReceiver = Convert.ToInt32(messageReceiver);
            message.MessageContent = messageContent;
            message.MessageStatus = false;
            message.MessageDate = DateTime.Now;
            

            db.tblMessages.Add(message);
            db.SaveChanges();


            string mail = Session["UserMail"].ToString();
            var values = db.tblUsers.Where(x => x.UserMail == mail).FirstOrDefault();
            ViewBag.Friends = db.tblFriends.Where(x => (x.FriendSender == values.UserID || x.FriendReceiver == values.UserID) && x.FriendStatus == 2).ToList();
            return RedirectToAction("MessageBox", new {id = messageReceiver});
        }
    }
}