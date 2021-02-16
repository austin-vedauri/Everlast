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
    public class ServiceController : BaseController
    {
        public ActionResult Services()
        {
            List<Service> services = new ServiceManager().GetServices();
            return View(services);
        }

        public ActionResult Create()
        {
            return View(new Service());
        }

        [HttpPost]
        public ActionResult Create(Service model)
        {
            if (ModelState.IsValid)
            {
                model = new ServiceManager().Create(model);

                return RedirectToAction("Services");
            }
            return View(model);
        }

        public ActionResult Read(Guid serviceGuid)
        {
            Service model = new ServiceManager().Read(serviceGuid);
            return View(model);
        }

        public ActionResult Update(Guid serviceGuid)
        {
            Service model = new ServiceManager().Read(serviceGuid);
            return View(model);
        }

        [HttpPost]
        public ActionResult Update(Service model)
        {
            if (ModelState.IsValid)
            {
                int result = new ServiceManager().Update(model);
                return RedirectToAction("Services");
            }

            return View(model);
        }

        public ActionResult Delete(Guid serviceGuid)
        {
            new ServiceManager().Delete(serviceGuid);
            return RedirectToAction("Services");
        }
    }
}