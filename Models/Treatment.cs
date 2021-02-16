using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Everlast.Models
{
    public class Treatment
    {
        public Guid TreatmentGuid { get; set; }
        public Guid ServiceGuid { get; set; }
        public string TreatmentName { get; set; }
        public string TreatmentDescription { get; set; }
    }
}