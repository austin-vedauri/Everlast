using Everlast.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Everlast.ViewModels
{
    public class AppointmentViewModel
    {
        public Guid AppointmentGuid { get; set; }
        public Guid TreatmentGuid { get; set; }
        public Guid ServiceGuid { get; set; }
        public string InjectorName { get; set; }
        public string ClientName { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public DateTime AppointmentStart { get; set; }
        public DateTime AppointmentEnd { get; set; }
    }
}