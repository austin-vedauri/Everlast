using Everlast.Managers;
using Everlast.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Everlast.Controllers
{
    public class PeriodController : BaseController
    {
        public ActionResult Create()
        {
            Period model = new Period
            {
                AccountGuid = GetCurrentAccountGuid(),
                Accounts = new AccountManager().GetAccountsByType(enums.AccountTypes.Injector),
            };

            model.Start = DateTime.Now;
            model.Stop = DateTime.Now.AddHours(4);

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(Period model)
        {
            if (ModelState.IsValid)
            {
                model = new PeriodManager().Create(model);
                if (model.PeriodGuid != Guid.Empty)
                {
                    return RedirectToAction("Read", new { periodGuid = model.PeriodGuid, accountGuid = model.AccountGuid });
                }
            }
            return View(model);
        }

        public ActionResult Read(Guid periodGuid, Guid accountGuid)
        {
            Period model = new PeriodManager().Read(periodGuid, accountGuid);
            return View(model);
        }

        public ActionResult Update(Guid periodGuid, Guid accountGuid)
        {
            Period model = new PeriodManager().Read(periodGuid, accountGuid);
            model.Accounts = new AccountManager().GetAccountsByType(enums.AccountTypes.Injector);

            return View(model);
        }

        [HttpPost]
        public ActionResult Update(Period model)
        {
            if (ModelState.IsValid)
            {
                new PeriodManager().Update(model);
                return RedirectToAction("Read", new { periodGuid = model.PeriodGuid, accountGuid = model.AccountGuid });
            }
            return View();
        }

        public ActionResult Delete(Guid periodGuid, Guid accountGuid)
        {
            new PeriodManager().Delete(periodGuid, accountGuid);
            return RedirectToAction("Periods");
        }

        public ActionResult Periods()
        {
            List<Period> models = new List<Period>();
            models = new PeriodManager().GetPeriods();
            return View(models);
        }

        public ActionResult PeriodsForAccount(Guid accountGuid)
        {
            List<Period> models = new List<Period>();
            models = new PeriodManager().GetPeriodsByAccount(accountGuid);
            return View(models);
        }

     
    }
}