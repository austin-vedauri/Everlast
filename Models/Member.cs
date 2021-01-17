using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Everlast.Models
{
    public class Member
    {
        public int MemberId { get; set; }
        public Guid MemberGuid { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool Verified { get; set; }
        public string Phone { get; set; }
        public bool IsAdmin { get; set; }
    }
}