using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Everlast.Models
{
    public class Appointment
    {
        // appointment data
        public int AppointmentId { get; set; }
        public Guid AppointmentGuid { get; set; }
        public int MemberId { get; set; }
        public Guid MemberGuid { get; set; }
        public int ServiceId { get; set; }
        public Guid ServiceGuid { get; set; }
        public int PartyId { get; set; }
        public Guid PartyGuid { get; set; }
        // appointment information
        public DateTime AppointmentStart { get; set; }
        public DateTime AppointmentEnd { get; set; }
        public string AddressLineOne { get; set; }
        public string AddressLineTwo { get; set; }
        public string AddressCity { get; set; }
        public string AddressState { get; set; }
        public string AddressPostal { get; set; }

        // appointment objects

        public Offer AppointmentForService { get; set; }
        public Party AppointmentForParty { get; set; }
        public Member AppointmentForMember { get; set; }
    }
}