using BookStoreWebUI.Models.Account;
using BookStoreWebUI.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebGrease.Css.Ast;

namespace BookStoreWebUI.Controllers
{
    public class AccountController : BaseController
    {
        new public LoginUserInfo LoginUserInfo
        {
            get { return GetLoginUserInfo(); }
            set
            {
                Session[Constants.SESSION_WEB_LOGIN_INFO] = value;
            }
        }

        private LoginUserInfo GetLoginUserInfo()
        {
            if (Session[Constants.SESSION_WEB_LOGIN_INFO] == null)
            {
                throw new Exception("Invalid login");
            }
            return (LoginUserInfo)Session[Constants.SESSION_WEB_LOGIN_INFO];
        }

        // GET: Account
        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(WebLoginReqInfo model)
        {
            // login
            ResUserLoginInfo response = await DataAccess.DataAccess.Authenticate(model);

            // redirect
            if (response.Result.Code == Constants.NACK_RESULT)
            {
                TempData["InvalidLogin"] = response.Result.Message;
                TempData.Keep();
                return RedirectToAction("Login", "Account");
            }

            // web login
            if (User.Identity.IsAuthenticated)
            {
                FormsAuthentication.SignOut();
            }

            LoginUserInfo = new LoginUserInfo()
            {
                LoginID = response.UserID,
                UserDlpName = response.UserDlpName,
                UserName = model.UserName,
                Password = model.Password,
                Token = response.AuthToken
            };

            FormsAuthentication.SetAuthCookie(LoginUserInfo.UserName.ToString(), true);

            if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
            {
                return Redirect(model.ReturnUrl);
            }
            return RedirectToAction("Index", "Book");
        }

        [Authorize]
        public ActionResult Logout()
        {
            if (User.Identity.IsAuthenticated)
            {
                FormsAuthentication.SignOut();
            }

            if (GetLoginUserInfo() != null)
            {
                // clear session
                Session.Clear();
                Session.Abandon();
            }

            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        public ActionResult CheckUserAuthentication()
        {
            return Json(new { IsAuthenticated = LoginUserInfo != null }, JsonRequestBehavior.AllowGet);
        }
    }
}