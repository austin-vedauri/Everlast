using Everlast.CRUD;
using Everlast.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Everlast.Controllers
{
    public class MemberController : Controller
    {
        // GET: Member
        public ActionResult Index(Guid memberGuid)
        {
            Member member = new MemberCRUD().Read(memberGuid);
            return View(member);
        }
    }
}