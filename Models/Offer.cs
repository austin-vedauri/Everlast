using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Everlast.Models
{
    public class Offer
    {
        public int OfferId { get; set; }
        public Guid OfferGuid { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Fee { get; set; }
        public TimeSpan Duration { get; set; }
        public bool Active { get; set; }
    }
}