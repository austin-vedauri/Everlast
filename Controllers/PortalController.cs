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
            return View(new Member());
        }

        [HttpPost]
        public ActionResult Login(Member member)
        {
            int sessionResult = 0;

            member = new PortalCRUD().Login(member);

            if (member.MemberId > 0)
            {
                sessionResult = new SessionsController().CreateMemberSession(member);
            }

            if (sessionResult > 0)
            {
                return View("Index", "Member", member);
            }

            return View(new Member());
        }

        public ActionResult Register()
        {
            return View(new Member());
        }

        [HttpPost]
        public ActionResult Register(Member member)
        {
            int sessionResult = 0;

            bool usernameExists = new PortalCRUD().UsernameExists(member.Username);

            if (!usernameExists)
            {
                member = new PortalCRUD().Register(member);
            }

            if (member.MemberId > 0)
            {
                sessionResult = new SessionsController().CreateMemberSession(member);
            }

            if (sessionResult > 0)
            {
                return RedirectToAction("Index", "Member", new { member = member.MemberGuid });
            }

            return View(new Member());
        }

        public ActionResult Logout()
        {
            new SessionsController().DestroyMemberSession();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult Account(Member model)
        {
            return View(model);
        }

        public ActionResult ResetPassword(Member model)
        {
            return View();
        }

        [HttpPost]
        public ActionResult DoResetPassword(Member model)
        {
            return View("Profile", model);
        }

        [HttpPost]
        public JsonResult Subscribe()
        {
            return Json("", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Unsubscribe()
        {
            return Json("", JsonRequestBehavior.AllowGet);
        }

    }
}