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
        [Display(Name = "Shift Begin")]
        [DataType(DataType.DateTime)]
        public DateTime Start { get; set; }
        [Display(Name = "Shift End")]
        [DataType(DataType.DateTime)]
        public DateTime Stop { get; set; }

        public List<Account> Accounts { get; set; }
    }
}