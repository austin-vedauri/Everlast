using Everlast.Managers;
using Everlast.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Everlast.Controllers
{
    public class ServiceTypeController : Controller
    {
        public ActionResult ServiceTypes()
        {
            List<ServiceType> services = new ServiceTypeManager().GetServiceTypes();
            return View(services);
        }

        public ActionResult Create()
        {
            return View(new ServiceType());
        }

        [HttpPost]
        public ActionResult Create(ServiceType model)
        {
            if (ModelState.IsValid)
            {
                model = new ServiceTypeManager().Create(model);

                return RedirectToAction("ServiceTypes");
            }
            return View(model);
        }

        public ActionResult Read(Guid serviceTypeGuid)
        {
            ServiceType model = new ServiceTypeManager().Read(serviceTypeGuid);
            return View(model);
        }

        public ActionResult Update(Guid serviceTypeGuid)
        {
            ServiceType model = new ServiceTypeManager().Read(serviceTypeGuid);
            return View(model);
        }

        [HttpPost]
        public ActionResult Update(ServiceType model)
        {
            if (ModelState.IsValid)
            {
                int result = new ServiceTypeManager().Update(model);
                return RedirectToAction("Services");
            }

            return View(model);
        }

        public ActionResult Delete(Guid serviceTypeGuid)
        {
            new ServiceTypeManager().Delete(serviceTypeGuid);
            return RedirectToAction("ServiceTypes");
        }
    }
}