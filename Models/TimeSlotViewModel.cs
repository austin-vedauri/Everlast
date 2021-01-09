using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Everlast.Models
{
    public class TimeSlotViewModel
    {
        public int MemberId { get; set; }
        public Guid MemberGuid { get; set; }
        public int TimeSlotId { get; set; }
        public Guid TimeSlotGuid { get; set; }
        public DateTime SlotTimeStart { get; set; }
        public DateTime SlotTimeEnd { get; set; }
    }
}