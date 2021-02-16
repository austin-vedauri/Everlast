using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Everlast.ViewModels
{
    public class PeriodViewModel
    {
        public Guid PeriodGuid { get; set; }
        public Guid AccountGuid { get; set; }
        public string FullName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime StopDate { get; set; }
    }
}