using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Everlast.Models
{
    public class Period
    {
        public Guid PeriodGuid { get; set; }

        public Guid AccountGuid { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime StopDate { get; set; }

        [Display(Name = "Shift Begin Date")]
        [DataType(DataType.Date)]
        public DateTime BeginDate { get; set; }
        [Display(Name = "Shift Begin Time")]
        [DataType(DataType.Time)]
        public TimeSpan BeginTime { get; set; }

        [Display(Name = "Shift End Date")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
        [Display(Name = "Shift End Time")]
        [DataType(DataType.Time)]
        public TimeSpan EndTime { get; set; }


        public List<Account> Accounts { get; set; }
    }
}