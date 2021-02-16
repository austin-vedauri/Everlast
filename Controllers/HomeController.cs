using Everlast.Managers;
using Everlast.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Everlast.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }
        
        public ActionResult Members()
        {
            return View();
        }

        public ActionResult Services()
        {
            List<Service> models = new ServiceManager().GetServices();
            return View(models);
        }

        public ActionResult Gallery()
        {
            // get all gallery images
            return View();
        }

        public ActionResult Events()
        {
            List<Party> models = new PartyManager().GetParties();
            return View(models);
        }

        public ActionResult Developer()
        {
            return View();
        }
    }
}