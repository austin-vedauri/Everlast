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
    public class HelperController : Controller
    {
        [HttpPost]
        public JsonResult GetAvailableAppointmentsFromInjector(Guid accountGuid, Guid serviceGuid)
        {
            // do the big c# work in manager
            List<Slot> models = new PeriodManager().GetAvailableAppointmentTimesForInjectorOnDate(DateTime.Now, accountGuid, serviceGuid);

            List<SelectListItem> modelsSelectList = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = "Select...",
                    Value = DateTime.MinValue.ToString()
                }
            };

            if (models != null)
            {
                foreach (Slot item in models)
                {
                    modelsSelectList.Add(new SelectListItem
                    {
                        Text = item.StartTime.ToString() + " to " + item.EndTime.ToString(),
                        Value = item.StartTime.ToString()
                    });
                }
            }

            return Json(new SelectList(modelsSelectList, "Value", "Text"), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetAvailableInjectors()
        {
            List<Account> models = new AccountManager().GetAccountsByType(AccountTypes.Injector);

            List<SelectListItem> modelsSelectList = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = "Select Who...",
                    Value = Guid.Empty.ToString()
                }
            };

            if (models != null)
            {
                foreach (Account model in models)
                {
                    modelsSelectList.Add(new SelectListItem
                    {
                        Text = model.FirstName + " " + model.LastName,
                        Value = model.AccountGuid.ToString()
                    });
                }
            }

            return Json(new SelectList(modelsSelectList, "Value", "Text"), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetAvailableServices()
        {
            List<Service> models = new ServiceManager().GetServices();

            List<SelectListItem> modelsSelectList = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = "Select...",
                    Value = Guid.Empty.ToString()
                }
            };

            if (models != null)
            {
                foreach (Service model in models)
                {
                    modelsSelectList.Add(new SelectListItem
                    {
                        Text = model.Title + " " + model.Price,
                        Value = model.ServiceGuid.ToString()
                    });
                }
            }

            return Json(new SelectList(modelsSelectList, "Value", "Text"), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetAccountTypes()
        {
            List<SelectListItem> modelsSelectList = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = "Select",
                    Value = "0"
                },
                new SelectListItem
                {
                    Text = "Administrator",
                    Value = ((int)AccountTypes.Administrator).ToString()
                },
                new SelectListItem
                {
                    Text = "Injector",
                    Value = ((int)AccountTypes.Injector).ToString()
                },
                new SelectListItem
                {
                    Text = "Member",
                    Value = ((int)AccountTypes.Member).ToString()
                },
                new SelectListItem
                {
                    Text = "Guest",
                    Value = ((int)AccountTypes.Guest).ToString()
                }
            };

            return Json(new SelectList(modelsSelectList, "Value", "Text"), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetParty(Guid partyGuid)
        {
            Party model = new PartyManager().Read(partyGuid);
            return Json(model, JsonRequestBehavior.AllowGet);
        }
    }
}