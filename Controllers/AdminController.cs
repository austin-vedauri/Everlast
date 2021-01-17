using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Everlast.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Account()
        {
            return View();
        }

        public ActionResult Offers()
        {
            return View("~/Offer/Index.cshtml", "Offer");
        }

        public ActionResult Parties()
        {
            return View("~/Party/Index.cshtml", "Party");
        }
    }
}