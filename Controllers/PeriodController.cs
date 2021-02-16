using Everlast.Managers;
using Everlast.Models;
using Everlast.ViewModels;
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

            model.StartDate = DateTime.Now;
            model.StopDate = DateTime.Now.AddHours(4);

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(Period model)
        {
            if (ModelState.IsValid)
            {
                model.StartDate = model.BeginDate.Add(model.BeginTime);
                model.StopDate = model.EndDate.Add(model.EndTime);

                if (model.StartDate > model.StopDate)
                {
                    ViewBag.MessageResult = "Start date and time cannot be later than the end date and time.";
                    return View(model);
                }

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
            List<PeriodViewModel> models = new List<PeriodViewModel>();
            models = new PeriodManager().GetWorkPeriodsForView();
            return View(models);
        }

        public ActionResult PeriodsForAccount(Guid accountGuid)
        {
            List<Period> models = new List<Period>();
            models = new PeriodManager().GetPeriodsByAccount(accountGuid);
            return View(models);
        }

        public ActionResult SearchPeriods(DateTime? startDate = null, DateTime? endDate = null, Guid? accountGuid = null)
        {
            List<PeriodViewModel> model = new PeriodManager().SearchPeriods(startDate, endDate, accountGuid);
            return PartialView("_Periods", model);
        }
     
    }
}