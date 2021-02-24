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
    public class TreatmentController : Controller
    {
        // GET: Treatment
        public ActionResult _Treatment(Guid serviceGuid)
        {
            Treatment model = new TreatmentManager().Read(serviceGuid);
            return PartialView("_Treatment", model);
        }

        public ActionResult Create()
        {
            return View(new Treatment());
        }

        [HttpPost]
        public ActionResult Create(Treatment model)
        {
            model = new TreatmentManager().Create(model);
            return RedirectToAction("Treatments");
        }

        public ActionResult Read(Guid treatmentGuid)
        {
            Treatment model = new TreatmentManager().Read(treatmentGuid);
            return View("Read", model);
        }

        public ActionResult Update(Guid treatmentGuid)
        {
            Treatment model = new TreatmentManager().Read(treatmentGuid);
            return View("Update", model);
        }

        [HttpPost]
        public ActionResult Update(Treatment model)
        {
            model = new TreatmentManager().Update(model);
            return RedirectToAction("Treatments");
        }

        public ActionResult Delete(Guid treatmentGuid)
        {
            int result = new TreatmentManager().Destroy(treatmentGuid);
            return RedirectToAction("Treatments");
        }

        public ActionResult GetBaseTreatments()
        {
            List<Treatment> models = new TreatmentManager().GetTreatments();
            return View("Treatments", models);
        }

        public ActionResult Treatments()
        {
            List<TreatmentViewModel> models = new TreatmentManager().GetAllTreatmentsWithServiceName();
            return View("Treatments", models);
        }

    }
}