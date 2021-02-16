using Everlast.Managers;
using Everlast.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Everlast.Controllers
{
    public class PartyController : BaseController
    {
        public ActionResult Parties()
        {
            List<Party> parties = new PartyManager().GetParties();
            return View(parties);
        }

        public ActionResult Create()
        {
            return View(new Party());
        }

        [HttpPost]
        public ActionResult Create(Party model)
        {
            if (ModelState.IsValid)
            {
                model = new PartyManager().Create(model);
                return RedirectToAction("Parties");
            }
            return View(new Party());
        }

        public ActionResult Read(Guid partyGuid)
        {
            Party model = new PartyManager().Read(partyGuid);
            return View(model);
        }

        public ActionResult Update(Guid partyGuid)
        {
            Party model = new PartyManager().Read(partyGuid);
            return View(model);
        }
        
        [HttpPost]
        public ActionResult Update(Party model)
        {
            if (ModelState.IsValid)
            {
                model = new PartyManager().Update(model);
                return RedirectToAction("Parties");
            }

            return View(model);
        }

        public ActionResult Delete(Guid partyGuid)
        {
            new PartyManager().Destroy(partyGuid);
            return RedirectToAction("Parties");
        }

    }
}