using Everlast.CRUD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Everlast.Controllers
{
    public class PartyController : Controller
    {
        // GET: Party
        public ActionResult Index()
        {
            return View(new PartyCRUD().GetParties());
        }

        public ActionResult Create()
        {
            return View(new Models.Party());
        }

        public ActionResult Read()
        {
            return View(new Models.Party());
        }

        public ActionResult Update()
        {
            return View(new Models.Party());
        }

        public ActionResult Delete()
        {
            return View(new Models.Party());
        }
    }
}