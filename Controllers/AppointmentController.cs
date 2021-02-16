using Everlast.enums;
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
    public class AppointmentController : BaseController
    {
        public ActionResult Appointments()
        {
            List<AppointmentViewModel> models = new AppointmentManager().GetScheduledAppointmentsForView();
            return View(models);
        }

        public ActionResult Create()
        {
            Appointment model = new Appointment();

            if (GetCurrentServiceGuid() != Guid.Empty)
            {
                model.ServiceGuid = GetCurrentServiceGuid();
            }

            model.ClientGuid = GetCurrentAccountGuid();

            ViewBag.Message = "You're almost finished!";

            return View(model);
        }

        // this gets called from the services page
        public ActionResult VerifyAccountForAppointment(Guid serviceGuid)
        {

            // set the current service guid
            SetCurrentServiceGuid(serviceGuid);

            // verify if the user is logged in or not
            if (GetCurrentAccountGuid() != Guid.Empty)
            {
                // you are logged in, take them to create
                return RedirectToAction("Create", "Appointment");

            }
            else
            {
                // you are not logged in, continue as guest or login?
                return RedirectToAction("Options", "Account");
            }
        }

        [HttpPost]
        public ActionResult Create(Appointment model)
        {
            if (ModelState.IsValid)
            {
                Service service = new ServiceManager().Read(model.ServiceGuid);

                model.AppointmentEnd = model.AppointmentStart.AddHours(service.Hours).AddMinutes(service.Minutes);
                model = new AppointmentManager().Create(model);
                return RedirectToAction("Read", "Appointment", new { appointmentGuid = model.AppointmentGuid });
            }
            return View(model);
        }

        public ActionResult Read(Guid appointmentGuid)
        {
            AppointmentViewModel model = new AppointmentManager().GetScheduledAppointmentByAppointmentGuidForView(appointmentGuid);
            return View(model);
        }

        public ActionResult Update(Guid appointmentGuid)
        {
            Appointment model = new AppointmentManager().Read(appointmentGuid);
            return View(model);
        }

        [HttpPost]
        public ActionResult Update(Appointment model)
        {
            if (ModelState.IsValid)
            {
                int result = new AppointmentManager().Update(model);

                if (result > 0)
                {
                    return RedirectToAction("Appointments");
                }
            }

            return View(model);
        }

        public ActionResult Delete(Guid appointmentGuid)
        {
            new AppointmentManager().Delete(appointmentGuid);
            return RedirectToAction("Appointments");
        }

    }
}