using ChatAppMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChatAppMVC.Controllers
{
    public class ProfilController : Controller
    {
        dbChatBoxEntities db = new dbChatBoxEntities();

        [HttpGet]
        public ActionResult Index()
        {
            string mail = (string)Session["UserMail"];
            var values = db.tblUsers.Where(x => x.UserMail == mail).FirstOrDefault();
            ViewBag.Friends = db.tblFriends.Where(x => (x.FriendSender == values.UserID || x.FriendReceiver == values.UserID) && x.FriendStatus == 2).ToList();
            return View(values);
        }

        [HttpPost]
        public ActionResult UpdateProfil(tblUser p)
        {
            int id = p.UserID;
            var values = db.tblUsers.Find(id);
            values.UserMail = p.UserMail;
            values.UserNick = p.UserNick;
            values.UserImageURL = p.UserImageURL;
            db.SaveChanges();
            Session["UserMail"] = values.UserMail;

            return RedirectToAction("Index");
        }


        public ActionResult SendFriend(tblUser p)
        {
            tblFriend Friend = new tblFriend();
            Friend.FriendSender = p.UserID;
            int receiver = db.tblUsers.Where(x => x.UserCode == p.UserCode).Select(x => x.UserID).FirstOrDefault();
            Friend.FriendReceiver = receiver;
            Friend.FriendStatus = 0;
            db.tblFriends.Add(Friend);
            db.SaveChanges();
           

            return RedirectToAction("Index");

        }
        
        public ActionResult friendRequest(int id)
        {
            var value = db.tblFriends.Where(x => x.FriendReceiver == id && x.FriendStatus == 0).ToList();
            ViewBag.friendRequest = value;
            
            string mail = (string)Session["UserMail"];
            var values = db.tblUsers.Where(x => x.UserMail == mail).FirstOrDefault();
            ViewBag.Friends = db.tblFriends.Where(x => (x.FriendSender == values.UserID || x.FriendReceiver == values.UserID) && x.FriendStatus == 2).ToList();
            return View(values);

        }
        public ActionResult AddFriend(int id)
        {
            var value = db.tblFriends.Find(id);
            value.FriendStatus = 2;
            db.SaveChanges();
            
            return RedirectToAction("Index");

        }
        public ActionResult RejectFriend(int id)
        {
            var value = db.tblFriends.Find(id);
            value.FriendStatus = 1;
            db.SaveChanges();
            
            return RedirectToAction("Index");

        }

    }
}