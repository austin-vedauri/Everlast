using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Everlast.Controllers
{
    public class OfferController : Controller
    {
        // GET: Offer
        public ActionResult Index()
        {
            return View(new List<Models.Offer>());
        }

        public ActionResult Create()
        {
            return View(new Models.Offer());
        }

        public ActionResult Read()
        {
            return View(new Models.Offer());
        }

        public ActionResult Update()
        {
            return View(new Models.Offer());
        }

        public ActionResult Delete()
        {
            return View(new Models.Offer());
        }
    }
}