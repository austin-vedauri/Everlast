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

        public ActionResult Register()
        {
            return View(new Member());
        }

        [HttpPost]
        public ActionResult Login(Member model)
        {
            int sessionResult = 0;

            Member member = new PortalCRUD().Login(model);
            member.IsAdmin = true;

            if (member.MemberId > 0)
            {
                sessionResult = new SessionsController().CreateMemberSession(model);
            }
            else
            {
                return View(new Member());
            }

            if (sessionResult > 0)
            {
                return View("Profile", member);
            }
            else
            {
                return View(new Member());
            }
        }

        public ActionResult Logout()
        {
            new SessionsController().DestroyMemberSession();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult Register(Member model)
        {
            int sessionResult = 0;
            bool usernameExists = false;

            PortalCRUD crud = new PortalCRUD();

            usernameExists = crud.UsernameExists(model.Username);

            if (usernameExists)
            {
                return View(new Member());
            }
            else
            {
                model = crud.Register(model);
            }

            if (model.MemberId > 0)
            {
                sessionResult = new SessionsController().CreateMemberSession(model);
            }

            if (sessionResult > 0)
            {
                model.IsAdmin = true;
                return View("Profile", model);
            }
            else
            {
                return View(new Member());
            }
        }

        public ActionResult Account(Member model)
        {
            return View(model);
        }

    }
}