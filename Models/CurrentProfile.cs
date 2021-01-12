using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Everlast.Models
{
    public class CurrentProfile
    {
        public int MemberId { get; set; }
        public Guid MemberGuid { get; set; }
        public string FirstName { get; set; }
    }
}