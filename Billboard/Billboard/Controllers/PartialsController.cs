using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Billboard.Controllers
{
    public class PartialsController : Controller
    {
        public ActionResult Welcome()
        {
            return View();
        }

        public ActionResult Ad()
        {
            return View();
        }

        public ActionResult Empty()
        {
            return View();
        }

        public ActionResult Friends()
        {
            return View();
        }
    }
}
