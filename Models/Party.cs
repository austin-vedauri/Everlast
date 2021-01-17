using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Everlast.Models
{
    public class Party
    {
        public int PartyId { get; set; }
        public Guid PartyGuid { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal EntryFee { get; set; }
        public bool Active { get; set; }
        /*datetime*/
        public DateTime PartyDate { get; set; }
        public DateTime PartyStart { get; set; }
        public DateTime PartyEnd { get; set; }
        public TimeSpan PartyTimeSpan { get; set; }
        /*address*/
        public string AddressLineOne { get; set; }
        public string AddressLineTwo { get; set; }
        public string City { get; set; }
        public string Postal { get; set; }
        public string State { get; set; }
        /*image file location*/
        public string PartyImagePath { get; set; }
    }
}