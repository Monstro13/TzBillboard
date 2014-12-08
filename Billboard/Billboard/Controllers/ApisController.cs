using Billboard.Managers;
using Billboard.Models;
using Billboard.Repositories.LkpEnums;
using Billboard.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Billboard.Controllers
{
    public class ApisController : Controller
    {
        [HttpPost]
        public ActionResult Register(String login, String password)
        {
            var result = new UserService().RegisterUser(login, password);

            if (result.Code == StatusCode.Success)
            {
                FormsAuthentication.SetAuthCookie(result.User.Id, false);
                Session.Add("UserId", result.User.Id);

                return Json(result.User);
            }
            else return Json(null);
        }

        [HttpPost]
        public ActionResult Login(String login, String password)
        {
            var result = new UserService().Login(login, password);

            if (result.Code == StatusCode.Success)
            {
                FormsAuthentication.SetAuthCookie(result.User.Id, false);
                Session.Add("UserId", result.User.Id);

                return Json(result.User);
            }
            else return Json(null);
        }

        [HttpPost]
        public ActionResult CheckLogin()
        {
            if (User.Identity.IsAuthenticated)
            {
                var userId = SessionManager.GetUserId();
                var user = new UserService().GetUser(userId);

                return Json(user);

            }
            else return Json(null);
        }

        [HttpPost]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return Json(true);
        }

        [HttpPost]
        [Authorize]
        public ActionResult AddPost(NewPostViewModel post)
        {
            var fileName = new Random().Next().ToString() + ".png";
            var path = Path.Combine(Server.MapPath("~/Images"), fileName);
            post.file.SaveAs(path);

            var service = new UserService();
            service.AddPost(SessionManager.GetUserId(), post.Title, post.Text, "/Images/" + fileName);

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [Authorize]
        public ActionResult Posts(String page = "", String query = "")
        {
            var service = new UserService();
            return Json(service.GetPosts(SessionManager.GetUserId(), page, query));
        }

        [HttpPost]
        [Authorize]
        public ActionResult DeletePost(String id)
        {
            var result = new UserService().DeletePost(id);

            if (result.FromDb)
            {
                var path = Request.MapPath("~" + result.ImagePath);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
            }

            return Json(result.FromDb);
        }

        [HttpPost]
        [Authorize]
        public ActionResult SavePost(String id, String title, String text)
        {
            return Json(new UserService().SavePost(id, title, text));
        }
    }
}
