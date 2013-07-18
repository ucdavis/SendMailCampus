using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Mvc;

namespace SendMailCampus.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Settings: port 25 and no SSL";

            return View();
        }

        public ActionResult SendEmail()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SendEmail(string body, string to)
        {
            var client = new SmtpClient("bulkmail2.ucdavis.edu");
            client.ClientCertificates.Add(new X509Certificate(Server.MapPath("~/cert.cer")));
            //client.EnableSsl = true;
            client.Port = 25;
            client.Send("srkirkland@ucdavis.edu", to, "bulkmail sample", body);

            ViewBag.Message = string.Format("Email sent at {0}", DateTime.Now);
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
