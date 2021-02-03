using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Everlast.Models
{
    public class Service
    {
        public Guid ServiceGuid { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Hours { get; set; }
        public int Minutes { get; set; }
        public bool Active { get; set; }
    }
}