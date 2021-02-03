using Everlast.enums;
using Everlast.Managers;
using Everlast.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Everlast.Controllers
{
    public class AccountController : BaseController
    {
        public ActionResult Register()
        {
            Account model = new Account
            {
                AccountType = (int)AccountTypes.Member
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult Register(Account model)
        {
            if (ModelState.IsValid)
            {
                model = new AccountManager().Register(model);
                return RedirectToAction("Home", new { accountGuid = model.AccountGuid });
            }

            return View(model);
        }

        public ActionResult Login()
        {
            return View(new Account());
        }

        [HttpPost]
        public ActionResult Login(Account model)
        {
            if (ModelState.IsValid)
            {
                model = new AccountManager().Login(model);
                Guid test = model.AccountGuid;
                return RedirectToAction("Home", new { accountGuid = model.AccountGuid });
            }

            return View(model);
        }

        public ActionResult Logout()
        {
            ResetAccountSession();

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Home(Guid accountGuid)
        {
            Account model = new AccountManager().Read(accountGuid);
            SetAccountSession(model.AccountGuid, model.AccountType);
            return View(model);
        }

        public ActionResult Accounts()
        {
            List<Account> models = new AccountManager().GetAccounts();
            return View(models);
        }

        public ActionResult Create()
        {
            return View(new Account());
        }

        [HttpPost]
        public ActionResult Create(Account model)
        {
            if (ModelState.IsValid)
            {
                model = new AccountManager().Register(model);
                return RedirectToAction("Accounts");
            }

            return View(model);
        }

        public ActionResult Read(Guid accountGuid)
        {
            Account model = new AccountManager().Read(accountGuid);
            return View(model);
        }

        public ActionResult Update(Guid accountGuid)
        {
            Account model = new AccountManager().Read(accountGuid);
            return View(model);
        }
        
        [HttpPost]
        public ActionResult Update(Account model)
        {
            if (ModelState.IsValid)
            {
                model = new AccountManager().Update(model);
                return RedirectToAction("Home", model);
            }

            return View(model);
        }

        public ActionResult Delete(Guid accountGuid)
        {
            int result = new AccountManager().Delete(accountGuid);
            return RedirectToAction("Accounts");
        }

        public ActionResult Options()
        {
            return View();
        }

        public ActionResult RegisterGuest()
        {
            return View(new Account());
        }

        [HttpPost]
        public ActionResult RegisterGuest(Account model)
        {
            // create guest account

            model.AccountType = (int)AccountTypes.Guest;
            model.Username = Guid.NewGuid().ToString();
            model.Password = Guid.NewGuid().ToString();

            if (ModelState.IsValid)
            {
                model = new AccountManager().Register(model);
            }

            // on success, session the guest information
            SetAccountSession(model.AccountGuid, model.AccountType);

            // on success of sessions, redirect the guest
            // towards where ever they were going
            if (GetCurrentServiceGuid() != Guid.Empty)
            {
                // they want a service, redirect them to finish booking their service
                return RedirectToAction("Create", "Appointment");
            }


            return View(new Account());
        }

    }
}