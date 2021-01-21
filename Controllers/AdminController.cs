using Everlast.Models;
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

        [HttpPost]
        public ActionResult Login(Admin model)
        {
            return View("Account", model);
        }

        public ActionResult Account(Admin model)
        {
            return View("Account", model);
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