using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Everlast.Models
{
    public class Attendee
    {
        public Guid AttendeeGuid { get; set; }
        public Guid PartyGuid { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public Party Event { get; set; }
    }
}