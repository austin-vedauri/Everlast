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
            return View(new MemberViewModel());
        }

        public ActionResult Register()
        {
            return View(new MemberViewModel());
        }

        [HttpPost]
        public ActionResult Login(MemberViewModel model)
        {
            //int result = 0;
            //result = new PortalCRUD().Login(model);
            //if (result > 0)
            //{
            //    model = new MemberCRUD().Read(result);
            //    new SessionsController().CreateMemberSession(model);
            //    return View("Index", "Member", model);
            //}

            new SessionsController().CreateMemberSession(model);
            model.MemberId = 7;
            return View("Index", "Member", model);
        }

        public ActionResult Logout()
        {
            new SessionsController().DestroyMemberSession();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult Register(MemberViewModel model)
        {
            //int result = 0;
            //result = new PortalCRUD().Register(model);
            // either send through email validation or create user on the spot

            int sessionResult = new SessionsController().CreateMemberSession(model);
            return View("Arrived", model);
        }

    }
}