using Everlast.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Everlast.Models
{
    public class Account
    {
        public Guid AccountGuid { get; set; }
        public int AccountType { get; set; }
        public AccountTypes GetAccountTypes { get; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}