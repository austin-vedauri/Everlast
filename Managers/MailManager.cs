using Everlast.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace Everlast.Managers
{
    public class MailManager
    {
        public void SendMailToAccount(Mail model)
        {
            MailMessage message = new MailMessage();

            if (string.IsNullOrEmpty(model.To))
            {
                message.To.Add("team@vedauri.com");
            }

            message.To.Add(model.To);
            message.From = new MailAddress("vedauri@gmail.com", "Vedauri Developer NO REPLY");
            message.Subject = model.Subject;
            string body = model.Body;
            message.Body = body;
            message.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient
            {
                Host = "relay-hosting.secureserver.net",
                Port = 587,
                UseDefaultCredentials = false,
                Credentials = new System.Net.NetworkCredential("vedauri@gmail.com", "Rh33se!!07"),
                EnableSsl = true
            };

            smtp.Send(message);
        }

        public void SendMailToAccounts(List<Mail> model)
        {
            foreach (Mail item in model)
            {
                SendMailToAccount(item);
            }
            
        }

        public void SendMailToNewMember(Mail model)
        {
            HtmlString welcomeHtml = new HtmlString("<b>Congratulations!</b><br />" +
                "You're now a member of <i>Everlasting Aesthetics</i>.<br /> <br />" +
                "<a href='ea.vedauri.com/Account/Login'>Login</a>");
            
            model.Subject = "Everlast Member - Congratulations";
            model.Body = welcomeHtml.ToString();
            SendMailToAccount(model);
        }
    }
}