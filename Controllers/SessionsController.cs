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
            if (Session.Count > 0)
            {
                this.DestroyMemberSession();
            }

            PropertyInfo[] properties = typeof(MemberViewModel).GetProperties();

            foreach (PropertyInfo property in properties)
            {
                Session.Add(property.Name, property.GetValue(property));
            }

            return Session.Count;
        }

        public HttpSessionStateBase ReadMemberSession()
        {
            return Session.Contents;
        }

        public int UpdateMemberSession(MemberViewModel model)
        {
            this.DestroyMemberSession();
            this.CreateMemberSession(model);
            return Session.Count;
        }

        public int DestroyMemberSession()
        {
            Session.RemoveAll();
            return Session.Count;
        }
    }
}