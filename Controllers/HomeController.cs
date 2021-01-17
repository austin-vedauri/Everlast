using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Everlast.Controllers
{
    public class HomeController : Controller
    {

        /*
         * everlast:

add services and cost, 
requests appointments and consults,
white bg with blue hair is actual logo
lips photo isn't really the logo but is stilled used
         */
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

        public ActionResult Services()
        {
            // get all services
            return View(new List<Models.Offer>());
        }

        public ActionResult Gallery()
        {
            // get all gallery images
            return View();
        }

        public ActionResult Events()
        {
            // get all events

            return View(new List<Models.Party>());
        }
    }
}