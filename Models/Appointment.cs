using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Everlast.Models
{
    public class Appointment
    {
        public int MemberId { get; set; }
        public Guid MemberGuid { get; set; }
        public int AppointmentId { get; set; }
        public Guid AppointmentGuid { get; set; }
        public DateTime AppointmentStart { get; set; }
        public DateTime AppointmentEnd { get; set; }
    }
}