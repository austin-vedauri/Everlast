using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Everlast.Models
{
    public class TimeSlot
    {
        public Guid TimeSlotGuid { get; set; }
        public Guid AccountGuid { get; set; }
        public DateTime TimeSlotStart { get; set; }
        public DateTime TimeSlotEnd { get; set; }
        public bool Available { get; set; }

    }
}