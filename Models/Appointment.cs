using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Everlast.Models
{
    public class Appointment
    {
        public Guid AppointmentGuid { get; set; }
        public Guid ClientGuid { get; set; }
        public Guid InjectorGuid { get; set; }
        public Guid ServiceGuid { get; set; }
        public Guid PeriodGuid { get; set; }
        public DateTime AppointmentStart { get; set; }
        public DateTime AppointmentEnd { get; set; }
    }
}