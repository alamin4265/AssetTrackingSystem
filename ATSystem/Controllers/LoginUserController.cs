using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ATSystem.Models.Entity;
using ATSystem.Models.Interface.BLL;

namespace ATSystem.Controllers
{
    public class LoginUserController : Controller
    {
        private ILoginUserManager loginUserManager;
        private ILoginHistoryManager history;

        public LoginUserController(ILoginUserManager _loginUserManager, ILoginHistoryManager _history)
        {
            loginUserManager = _loginUserManager;
            history = _history;
        }


        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginUser loginUser)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (loginUserManager.IsExistUserNamePassword(loginUser.UserName, loginUser.Password))
            {
                Session["username"] = loginUser.UserName;
                Session["password"] = loginUser.Password;

                string ip = Request.UserHostAddress;
                DateTime date = DateTime.Now;

                LoginHistory historymodel=new LoginHistory()
                {
                    UserName = Session["username"].ToString(),
                    Ip = ip,
                    Time = date.ToString()
                };
                history.Add(historymodel);
                return RedirectToAction("Home","Home");
            }
            else
            {
                ModelState.Clear();
                ViewBag.message = "Username or Password Went Wrong";
            }
            return View();
        }

    }
}