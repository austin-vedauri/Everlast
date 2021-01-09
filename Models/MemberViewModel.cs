using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Everlast.Models
{
    public class MemberViewModel
    {
        public int MemberId { get; set; }
        public Guid MemberGuid { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public bool EmailAddressVerified { get; set; }
        public string PhoneNumber { get; set; }
    }
}