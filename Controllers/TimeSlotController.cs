using Everlast.Managers;
using Everlast.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Everlast.Controllers
{
    public class TimeSlotController : Controller
    {
        public ActionResult Create()
        {
            return View(new TimeSlot());
        }

        [HttpPost]
        public ActionResult Create(TimeSlot model)
        {
            if (ModelState.IsValid)
            {
                model = new TimeSlotManager().Create(model);
                if (model.TimeSlotGuid != Guid.Empty)
                {
                    return RedirectToAction("Read", new { timeSlotGuid = model.TimeSlotGuid, accountGuid = model.AccountGuid });
                }
            }
            return View(model);
        }

        public ActionResult Read(Guid timeSlotGuid, Guid accountGuid)
        {
            TimeSlot model = new TimeSlotManager().Read(timeSlotGuid, accountGuid);
            return View(model);
        }

        public ActionResult Update()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Update(TimeSlot model)
        {
            if (ModelState.IsValid)
            {
                new TimeSlotManager().Update(model);
                return RedirectToAction("Read", new { timeSlotGuid = model.TimeSlotGuid, accountGuid = model.AccountGuid });
            }
            return View();
        }

        public ActionResult Delete(Guid timeSlotGuid, Guid accountGuid)
        {
            new TimeSlotManager().Delete(timeSlotGuid, accountGuid);
            return RedirectToAction("TimeSlots");
        }

        public ActionResult TimeSlots()
        {
            List<TimeSlot> models = new List<TimeSlot>();
            models = new TimeSlotManager().GetTimeSlots();
            return View(models);
        }

        public ActionResult TimeSlotsForAccount(Guid accountGuid)
        {
            List<TimeSlot> models = new List<TimeSlot>();
            models = new TimeSlotManager().GetTimeSlotsByAccount(accountGuid);
            return View(models);
        }
    }
}