using Everlast.CRUD;
using Everlast.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Everlast.Controllers
{
    public class PortalController : Controller
    {
        // GET: Portal
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(MemberViewModel model)
        {
            int result = 0;

            result = new PortalCRUD().Login(model);

            if (result > 0)
            {
                model = new MemberCRUD().Read(result);
                new SessionsController().CreateMemberSession(model);
                return View("Index", "Member", model);
            }

            return View();
        }

        [HttpPost]
        public ActionResult Register(MemberViewModel model)
        {
            int result = 0;

            result = new PortalCRUD().Register(model);
            
            // either send through email validation or create user on the spot

            return View();
        }

    }
}