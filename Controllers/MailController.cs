using Everlast.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace Everlast.Controllers
{
    public class MailController : Controller
    {
        // GET: Mail
        public ActionResult SendMail()
        {
            return View(new Mail());
        }

        [HttpPost]
        public ViewResult SendMail(Mail model)
        {
            if (ModelState.IsValid)
            {
                
                return View("Index");
            }
            else
            {
                return View(model);
            }
        }
    }
}