using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Everlast.Controllers
{
    public class BaseController : Controller
    {
        public Guid GetCurrentAccountGuid()
        {
            if (Session["AccountGuid"] != null)
            {
                return (Guid)Session["AccountGuid"];
            }
            else
            {
                return Guid.Empty;
            }
        }

        public void SetCurrentAccountGuid(Guid accountGuid)
        {
            Session["AccountGuid"] = accountGuid;
        }

        public int GetCurrentAccountType()
        {
            if (Session["AccountType"] != null)
            {
                return (int)Session["AccountType"];
            }
            else
            {
                return 0;
            }
        }

        public void SetCurrentAccountType(int accountType)
        {
            Session["AccountType"] = accountType;
        }

        public Guid GetCurrentServiceGuid()

        {
            if (Session["ServiceGuid"] != null)
            {
                return (Guid)Session["ServiceGuid"];
            }
            else
            {
                return Guid.Empty;
            }
        }

        public void SetCurrentServiceGuid(Guid serviceGuid)
        {
            Session["ServiceGuid"] = serviceGuid;
        }

        public void ResetAccountSession()
        {
            Session["AccountGuid"] = null;
            Session["AccountType"] = null;
        }

        public void SetAccountSession(Guid accountGuid, int accountType)
        {
            Session["AccountGuid"] = accountGuid;
            Session["AccountType"] = accountType;
        }
    }
}