using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ATSystem.Models.Entity;
using ATSystem.Models.Interface.BLL;

namespace ATSystem.Controllers
{
    public class MessageController : Controller
    {
        private IMessageManager messageManager;
        private IUserManager usermanager;
        private UiLoader.UiLoader loader;
        public MessageController(IMessageManager _messageManager, IUserManager _usermanager , UiLoader.UiLoader _loader)
        {
            messageManager = _messageManager;
            usermanager = _usermanager;
            loader = _loader;

            }



        public ActionResult NewMessage()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("HomePage", "Home");
            }

            ViewBag.send = "";
            var username = Session["username"].ToString();
            int orgid = 0;
            int id = 0;
            var list = usermanager.GetAll().Where(c => c.UserName == username);
            foreach (var t in list)
            {
                id = t.Id;
                orgid = t.OrganizationId;
            }
            ViewBag.userlist = loader.GetUserByOrgIdandNotCurrentUser(id,orgid);
            bool s = Request.QueryString["success"] == "true";
            if (s)
            {
                ViewData["success"] = "Save Successfully";
            }

            return View();
        }

        [HttpPost]
        public ActionResult NewMessage(Message message)
        {
            
            string date = DateTime.Now.ToString();
            message.Date = date;

            var username = Session["username"].ToString();
            int orgid = 0;
            int id = 0;
            var list = usermanager.GetAll().Where(c => c.UserName == username);
            foreach (var t in list)
            {
                id = t.Id;
                orgid = t.OrganizationId;
            }

            string tomessageusername = null;
            var tolist = usermanager.GetAll().Where(c => c.Id==Convert.ToInt32(message.MessageTo));
            foreach (var t in tolist)
            {
                tomessageusername = t.UserName;
            }

            message.MessageFrom = username;
            message.MessageTo = tomessageusername;


            if (messageManager.Add(message))
            {
                ModelState.Clear();
                return RedirectToAction("NewMessage", new { success = "true" });
               
            }
            


            ViewBag.userlist = loader.GetUserByOrgIdandNotCurrentUser(id, orgid);

            return View();
        }


        public ActionResult Inbox()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("HomePage", "Home");
            }

            var messageuser =
                messageManager.GetAll().Where(c => c.MessageTo == Session["username"].ToString() && c.Read == false);
            foreach (var t in messageuser)
            {
                Message aMessage = messageManager.GetById(t.Id);
                aMessage.Read = true;
                messageManager.Update(aMessage);
            }
            return View();
        }

        public ActionResult Sent()
        {
            return View();
        }

        public ActionResult AllInboxMessage()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("HomePage", "Home");
            }

            var result = messageManager.GetAll().OrderBy(c => c.Date).Where(c=>c.MessageTo==Session["username"].ToString()).ToList();
            var jsonData = result.Select(c => new
            {
                c.Id,
                c.Date,
                c.Title,
                c.Details,
                c.MessageFrom
            });
            return Json(new { data = jsonData }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AllSentMessage()
        {
            var result = messageManager.GetAll().OrderBy(c => c.Date).Where(c => c.MessageFrom == Session["username"].ToString()).ToList();
            var jsonData = result.Select(c => new
            {
                c.Id,
                c.Date,
                c.Title,
                c.Details,
                c.MessageTo
            });
            return Json(new { data = jsonData }, JsonRequestBehavior.AllowGet);
        }
    }
}