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
        public JsonResult GetAvailableAppointmentsFromInjector(Guid accountGuid, Guid serviceGuid, Guid periodGuid)
        {
            // do the big c# work in manager
            List<Slot> models = new PeriodManager().GetAvailableAppointmentTimesForInjectorByPeriod(periodGuid, accountGuid, serviceGuid);

            List<SelectListItem> modelsSelectList = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = "Select Appointment",
                    Value = DateTime.MinValue.ToString()
                }
            };

            if (models != null)
            {
                foreach (Slot item in models)
                {
                    modelsSelectList.Add(new SelectListItem
                    {
                        Text = item.StartTime.ToShortTimeString().ToString() + " to " + item.EndTime.ToShortTimeString().ToString(),
                        Value = item.StartTime.ToString(),
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
                    Text = "Select Injector",
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
                    Text = "Select Service",
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
        public JsonResult GetAvailablePeriods(Guid accountGuid)
        {
            List<Period> models = new PeriodManager().GetPeriodsByAccountForCurrentWeek(accountGuid);

            List<SelectListItem> modelsSelectList = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = "Select Day",
                    Value = Guid.Empty.ToString()
                }
            };

            if (models != null)
            {
                foreach (Period model in models)
                {
                    modelsSelectList.Add(new SelectListItem
                    {
                        Text = model.StartDate.ToShortDateString(),
                        Value = model.PeriodGuid.ToString()
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
                    Text = "Select Account Type",
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

        public bool IsValidEmailAddress(string emailAddress)
        {
            if (String.IsNullOrEmpty(emailAddress) || 
                emailAddress.Length <= 5 ||
                !emailAddress.Contains(".") ||
                !emailAddress.Contains("@"))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        [HttpPost]
        public JsonResult GetAvailableServiceTypes()
        {
            List<ServiceType> models = new ServiceTypeManager().GetServiceTypes();

            List<SelectListItem> modelsSelectList = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = "Select Service Type",
                    Value = Guid.Empty.ToString()
                }
            };

            if (models != null)
            {
                foreach (ServiceType model in models)
                {
                    modelsSelectList.Add(new SelectListItem
                    {
                        Text = model.ServiceTypeName,
                        Value = model.ServiceTypeGuid.ToString()
                    });
                }
            }

            return Json(new SelectList(modelsSelectList, "Value", "Text"), JsonRequestBehavior.AllowGet);
        }
    }
}