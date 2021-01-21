using Everlast.CRUD;
using Everlast.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Everlast.Controllers
{
    public class AppointmentController : Controller
    {
        // GET: Appointment
        public ActionResult Index()
        {
            // get list of appointments
            return View();
        }

        public ActionResult ScheduleAppointment(Guid serviceGuid)
        {
            Appointment appointment = new Appointment
            {
                AppointmentForService = new OfferCRUD().Read(serviceGuid)
            };

            return View(appointment);
        }

        [HttpPost]
        public ActionResult ScheduleAppointment(Appointment appointment)
        {
            return View("AppointmentConfirmation", appointment);
        }
    }
}