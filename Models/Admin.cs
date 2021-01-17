using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Everlast.Models
{
    public class Admin
    {
        public int AdminId { get; set; }
        public Guid AdminGuid { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}