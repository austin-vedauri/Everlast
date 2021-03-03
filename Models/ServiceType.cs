using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Everlast.Models
{
    public class ServiceType
    {
        public int ServiceTypeId { get; set; }
        public Guid ServiceTypeGuid { get; set; }
        public string ServiceTypeName { get; set; }
    }
}