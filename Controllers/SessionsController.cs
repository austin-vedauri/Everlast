using Everlast.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace Everlast.Controllers
{
    public class SessionsController : Controller
    {
        public int CreateMemberSession(MemberViewModel model)
        {

            CurrentProfile profile = new CurrentProfile()
            {
                FirstName = model.FirstName,
                MemberGuid = new Guid(),
                MemberId = 1
            };

            System.Web.HttpContext.Current.Session["Profile"] = profile;

            return 1;
        }

        public CurrentProfile ReadMemberSession()
        {
            return System.Web.HttpContext.Current.Session["Profile"] as CurrentProfile;
        }

        public int UpdateMemberSession(MemberViewModel model)
        {
            this.DestroyMemberSession();
            this.CreateMemberSession(model);
            return 1;
        }

        public int DestroyMemberSession()
        {
            System.Web.HttpContext.Current.Session["Profile"] = null;
            return 1;
        }
    }
}