using Billboard.Managers;
using Billboard.Models;
using Billboard.Repositories.LkpEnums;
using Billboard.Services;
using Billboard.Services.Interfaces;
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
        private readonly IUserService _userService;

        public ApisController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public ActionResult Register(String login, String password)
        {
            var result = _userService.RegisterUser(login, password);

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
            var result = _userService.Login(login, password);

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
                var user = _userService.GetUser(userId);

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

            _userService.AddPost(SessionManager.GetUserId(), post.Title, post.Text, "/Images/" + fileName);

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [Authorize]
        public ActionResult Posts(String page = "", String query = "")
        {
            return Json(_userService.GetPosts(SessionManager.GetUserId(), page, query));
        }

        [HttpPost]
        [Authorize]
        public ActionResult DeletePost(String id)
        {
            var result = _userService.DeletePost(id);

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
            return Json(_userService.SavePost(id, title, text));
        }

        [HttpPost]
        [Authorize]
        public ActionResult Friends(String userId)
        {
            return Json(_userService.GetFriends(userId));
        }

        [HttpPost]
        [Authorize]
        public ActionResult FriendsPosts(String login)
        {
            return Json(_userService.GetPostsByLogin(login));
        }

        [HttpPost]
        [Authorize]
        public ActionResult AllUsers()
        {
            return Json(_userService.GetAllUsers());
        }

        [HttpPost]
        [Authorize]
        public ActionResult AddToFriend(string id)
        {
            return Json(_userService.AddToFriend(SessionManager.GetUserId(), id));
        }
    }
}
