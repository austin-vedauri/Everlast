using Everlast.Managers;
using Everlast.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Everlast.Controllers
{
    public class AttendeeController : Controller
    {
        public ActionResult Create(Guid partyGuid)
        {
            Attendee model = new Attendee
            {
                PartyGuid = partyGuid
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(Attendee model)
        {
            if (ModelState.IsValid)
            {
                model = new AttendeeManager().Create(model);
            }

            return RedirectToAction("Read", new { attendeeGuid = model.AttendeeGuid, partyGuid = model.PartyGuid });
        }

        public ActionResult Read(Guid attendeeGuid, Guid partyGuid)
        {
            Attendee model = new AttendeeManager().Read(attendeeGuid, partyGuid);
            model.Event = new PartyManager().Read(partyGuid);

            return View(model);
        }

        public ActionResult Update()
        {
            return View();
        }

        public ActionResult Delete()
        {
            return View();
        }
    }
}