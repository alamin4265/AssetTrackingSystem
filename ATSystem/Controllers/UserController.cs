using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using ATSystem.Models.Entity;
using ATSystem.Models.Interface.BLL;
using ATSystem.Models.ViewModel.ResetPassword;
using AutoMapper;

namespace ATSystem.Controllers
{
    public class UserController : Controller
    {
        private IUserManager userManager;
        private IOrganizationManager organizationManager;
        private ILoginHistoryManager historymanager;
        private IMessageManager messageManager;

        public UserController(IUserManager _userManager, IOrganizationManager _organizationManager, ILoginHistoryManager _history, IMessageManager _messageManager)
        {
            userManager = _userManager;
            organizationManager = _organizationManager;
            historymanager = _history;
            messageManager = _messageManager;
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User user)
        {
            if (userManager.IsExistUserNamePassword(user.UserName,user.Password))
            {
                Session["username"] = user.UserName;
                Session["password"] = user.Password;
                
                var designation = userManager.GetAll().Where(c => c.UserName == user.UserName);
                foreach (var t in designation)
                {
                    Session["fullname"] = t.FullName;
                    Session["organizationId"] = t.OrganizationId;
                    Session["designation"] = t.Designation;
                    Session["Approve"] = t.Approve;
                    Session["AssetApprove"] = t.AssetApprove;
                }
                int orgid = Convert.ToInt32(Session["organizationId"].ToString());
                var result = organizationManager.GetAll().Where(c => c.Id == orgid);
                foreach (var t in result)
                {
                    Session["organizationName"] = t.Name;
                }
                string ip = Request.UserHostAddress;

                DateTime date = DateTime.Now;
                LoginHistory historymodel = new LoginHistory()
                {
                    UserName = Session["username"].ToString(),
                    Ip = ip,
                    Time = date.ToString()
                };
                historymanager.Add(historymodel);
                ModelState.Clear();
                return RedirectToAction("Home", "Home");
            }
            else
            {
                ViewBag.Message = "Username or Password Went Wrong";
                return View(user);
            }
        }


        public ActionResult Registration()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("HomePage", "Home");
            }

            int orgid = 0;
            string username = Session["username"].ToString();
            if (Session["designation"].ToString() == "Organization Owner")
            {
                var uerlist=userManager.GetAll().Where(c => c.UserName == username);
                
                foreach (var t in uerlist)
                {
                    orgid = t.OrganizationId;
                }
                ViewBag.orglist = organizationManager.GetAll().Where(c => c.Id == orgid);
            }
            else
            {
                ViewBag.orglist = organizationManager.GetAll();
            }
            
            return View();
        }

        [HttpPost]
        public ActionResult Registration(User user)
        {
            if (Session["designation"].ToString() == "Admin")
            {
                user.Designation = "Organization Owner";
            }
            else if (Session["designation"].ToString() == "Organization Owner")
            {
                user.Designation = "Manager";
            }
            user.Approve = true;
            user.AssetApprove = true;
            if (ModelState.IsValid)
            {
                if (userManager.IsExistUserName(user.UserName))
                {
                    ViewBag.Message = "This UserName Has Already Taken";
                }
                else
                {
                    if (userManager.Add(user))
                    {
                        ViewBag.Message = "Registration Successfull";
                        ModelState.Clear();
                    }
                    else
                    {
                        ViewBag.Message = "Registration Failed";
                        return View(user);
                    }
                }
            }


            int orgid = 0;
            string username = Session["username"].ToString();
            if (Session["designation"].ToString() == "Organization Owner")
            {
                var uerlist = userManager.GetAll().Where(c => c.UserName == username);

                foreach (var t in uerlist)
                {
                    orgid = t.OrganizationId;
                }
                ViewBag.orglist = organizationManager.GetAll().Where(c => c.Id == orgid);
            }
            else
            {
                ViewBag.orglist = organizationManager.GetAll();
            }

            return View();
        }

        public ActionResult ProfileView()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("HomePage", "Home");
            }


            int MessageCount = messageManager.GetAll().Count(c => c.MessageTo == Session["username"].ToString() && c.Read == false);
            if (MessageCount == 0)
            {
                Session["MessageCount"] = null;
            }
            else
            {
                Session["MessageCount"] = MessageCount.ToString();
            }





            var username = Session["username"].ToString();
            var userinfo = userManager.GetAll().Where(c => c.UserName == username);
            foreach (var t in userinfo)
            {
                Session["userid"] = t.Id;
            }
            int id = Convert.ToInt32(Session["userid"].ToString());
            Models.Entity.User user = userManager.GetById(id);
            return View(user);
        }

        public ActionResult Profile()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("HomePage", "Home");
            }


            int MessageCount = messageManager.GetAll().Count(c => c.MessageTo == Session["username"].ToString() && c.Read == false);
            if (MessageCount == 0)
            {
                Session["MessageCount"] = "False";
            }
            else
            {
                Session["MessageCount"] = MessageCount.ToString();
            }
           


            var username = Session["username"].ToString();
            var userinfo = userManager.GetAll().Where(c => c.UserName == username);
            foreach (var t in userinfo){
                Session["userid"] = t.Id;
            }
            int id = Convert.ToInt32(Session["userid"].ToString());
            Models.Entity.User user =userManager.GetById(id);

            return View(user);
        }

        [HttpPost]
        public ActionResult Profile(User usermodel)
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("HomePage", "Home");
            }

            var username = Session["username"].ToString();
            var userinfo = userManager.GetAll().Where(c => c.UserName == username);
            foreach (var t in userinfo)
            {
                Session["userid"] = t.Id;
            }
            int id = Convert.ToInt32(Session["userid"].ToString());
            Models.Entity.User user = userManager.GetById(id);
            user.FullName = usermodel.FullName;
            user.Gender = usermodel.Gender;
            user.Email = usermodel.Email;
            user.PhoneNo = usermodel.PhoneNo;
            if (userManager.Update(user))
            {
                ViewBag.message = "Update Succssfully";
            }
            else
            {
                ViewBag.message = "Update Failed";
            }

            return View(user);
        }


        private bool IsValidContentType(string content)
        {
            return content.Equals("image/png") || content.Equals("image/jpg") || content.Equals("image/jpeg");
        }

        private bool IsValidContentLength(int contextlength)
        {
            return ((contextlength / 1024) / 1024) < 2;
        }

        [HttpPost]
        public ActionResult UploadImage(HttpPostedFileBase photo)
        {
            if (!IsValidContentType(photo.ContentType))
            {
                ViewBag.message = "Only PNG,JPG,JPEG Files Are Allowed";
                return View("Profile");
            }
            else if(!IsValidContentLength(photo.ContentLength))
            {
                ViewBag.message="Your file is too large";
                return View("Profile");
            }
            else
            {
                if (photo.ContentLength > 0)
                {
                    Random rand=new Random();
                    var rk = rand.Next(1000);
                    var fileName = Path.GetFileName(rk+photo.FileName);
                    var path = Path.Combine(Server.MapPath("~/Image/"), fileName);
                    photo.SaveAs(path);

                    int userid = Convert.ToInt32(Session["userid"].ToString());
                    Models.Entity.User user = new User();
                    user = userManager.GetById(userid);
                    user.Image = fileName;
                    if (userManager.Update(user))
                    {
                        ViewBag.message = "Uploaded Successfully";
                        return View("Profile", user);
                    }
                    else
                    {
                        ViewBag.message = "Uploaded Failed";
                    }
                }

                return View("Profile");
            }
            
        }





        public ActionResult ResetPassword()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("HomePage", "Home");
            }

            return View();
        }

        private bool IsCurrentPasswordExist(string pass)
        {
            string username =Session["username"].ToString();
            var all = userManager.GetAll().Where(c => c.UserName == username);
            int id = 0;
            string password = "";
            foreach (var t in all)
            {
                id = t.Id;
                password = t.Password;
            }
            if (password == pass)
            {
                return true;
            }
            else
            {
                return false;
            }
            

        }
        [HttpPost]
        public ActionResult ResetPassword(ResetPasswordVM resetvm)
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("HomePage", "Home");
            }

            if (IsCurrentPasswordExist(resetvm.CurrentPassword))
            {
                var result = Mapper.Map<User>(resetvm);

                string username = Session["username"].ToString();
                var all = userManager.GetAll().Where(c => c.UserName == username);
                int id = 0;
                foreach (var t in all)
                {
                    id = t.Id;
                }
                Models.Entity.User user = userManager.GetById(id);
                user.Password = result.Password;
                user.ConfirmPassword = result.ConfirmPassword;
                if (userManager.Update(user))
                {
                    ViewBag.success = "Update Successfull";
                }
                else
                {
                    ViewBag.success = "Update Failed";
                }
            }
            else
            {
                ViewBag.success = "Current Password Doesn't Match";
            }
            return View();
        }


        public ActionResult BranchManagers()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("HomePage", "Home");
            }

            int id = Convert.ToInt32(Request.QueryString["delete"]);
            if (id != 0)
            {
                User user = userManager.GetById(id);
                userManager.Delete(user);
                return RedirectToAction("BranchManagers");
            }
            return View();
        }


        public ActionResult AllUsers()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("HomePage", "Home");
            }

            return View();
        }


        public ActionResult MovementAuthorityYes(int? id)
        {
            if(id !=null)
            {
                Models.Entity.User user = userManager.GetById(id);
                if (user != null)
                {
                    user.Approve = true;
                    userManager.Update(user);
                }

            }
            return RedirectToAction("BranchManagers");
        }

        public ActionResult MovementAuthorityNo(int? id)
        {
            if (id != null)
            {
                Models.Entity.User user = userManager.GetById(id);
                if (user != null)
                {
                    user.Approve = false;
                    userManager.Update(user);
                }

            }
            return RedirectToAction("BranchManagers");
        }


        public ActionResult AssetAuthorityYes(int? id)
        {
            if (id != null)
            {
                Models.Entity.User user = userManager.GetById(id);
                if (user != null)
                {
                    user.AssetApprove = true;
                    userManager.Update(user);
                }

            }
            return RedirectToAction("BranchManagers");
        }

        public ActionResult AssetAuthorityNo(int? id)
        {
            if (id != null)
            {
                Models.Entity.User user = userManager.GetById(id);
                if (user != null)
                {
                    user.AssetApprove = false;
                    userManager.Update(user);
                }

            }
            return RedirectToAction("BranchManagers");
        }

        public ActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForgetPassword(ForgetPassword aForgetPassword)
        {
            User x = userManager.GetAll().FirstOrDefault(c => c.Email == aForgetPassword.Email);
            if (x == null)
            {
                ViewBag.Message = "Invalid Email";
            }
            else
            {
                ViewBag.Message = "Password Has Sent To Your Email Address";
            }
            return View();
        }

        public ActionResult LoginHistory()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("HomePage", "Home");
            }




            return View();
        }
    }
}