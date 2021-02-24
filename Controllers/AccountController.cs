using Everlast.enums;
using Everlast.Managers;
using Everlast.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

                if (String.IsNullOrEmpty(model.FirstName) || 
                    String.IsNullOrEmpty(model.LastName) ||
                    String.IsNullOrEmpty(model.Email) ||
                    String.IsNullOrEmpty(model.Phone) ||
                    String.IsNullOrEmpty(model.Username) ||
                    String.IsNullOrEmpty(model.Password)
                    )
                {
                    ViewBag.MessageResult = "Please fill out the form completely to register.";
                    return View(model);
                }
                else if (!new HelperController().IsValidEmailAddress(model.Email))
                {
                    ViewBag.MessageResult = "Please enter a valid email address.";
                    return View(model);
                }

                model = new AccountManager().Register(model);

                try
                {
                    Mail message = new Mail
                    {
                        To = model.Email
                    };
                    new MailManager().SendMailToNewMember(message);
                }
                catch (Exception ex)
                {
                    LogError(ex.Message, MethodBase.GetCurrentMethod().ToString());
                }

                SetAccountSession(model.AccountGuid, model.AccountType);
                return RedirectToAction("Home");
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
                try
                {
                    model = new AccountManager().Login(model);
                }
                catch (Exception ex)
                {
                    LogError(ex.Message, MethodBase.GetCurrentMethod().ToString());
                }
               
                if (model.AccountGuid != Guid.Empty)
                {
                    SetAccountSession(model.AccountGuid, model.AccountType);
                    return RedirectToAction("Home");
                }
                else
                {
                    ViewBag.MessageResult = "The username or password is not valid.";
                    return View(new Account());
                }
            }

            ViewBag.MessageResult = "The username and password are both required";
            return View(new Account());
        }

        public ActionResult Logout()
        {
            ResetAccountSession();

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Home()
        {
            Guid accountGuid = GetCurrentAccountGuid();

            if (accountGuid == Guid.Empty)
            {
                return View("Login");
            }
            else
            {
                Account model = new Account();

                try
                {
                    model = new AccountManager().Read(accountGuid);
                }
                catch (Exception ex)
                {
                    LogError(ex.Message, MethodBase.GetCurrentMethod().ToString());
                }

                return View(model);
            }
        }

        public ActionResult Accounts()
        {
            List<Account> models = new List<Account>();

            try
            {
                models = new AccountManager().GetAccounts();
            }
            catch (Exception ex)
            {
                LogError(ex.Message, MethodBase.GetCurrentMethod().ToString());
            }

            return View(models);
        }

        public ActionResult GetAccounts()
        {
            List<Account> models = new List<Account>();

            try
            {
                models = new AccountManager().GetAccounts();
            }
            catch (Exception ex)
            {
                LogError(ex.Message, MethodBase.GetCurrentMethod().ToString());
            }

            return PartialView("_Accounts", models);
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
                try
                {
                    model = new AccountManager().Register(model);
                }
                catch (Exception ex)
                {
                    LogError(ex.Message, MethodBase.GetCurrentMethod().ToString());
                }

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
                return RedirectToAction("Read", new { accountGuid = model.AccountGuid });
            }

            return View(model);
        }

        public ActionResult Delete(Guid accountGuid)
        {
            Account model = new AccountManager().Read(accountGuid);
            return PartialView("_DeleteAccount", model);
        }

        [HttpPost]
        public JsonResult DeleteAccount(Guid accountGuid)
        {
            int result = new AccountManager().Delete(accountGuid);
            return Json(result, JsonRequestBehavior.AllowGet);
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