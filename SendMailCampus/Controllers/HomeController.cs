using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
            return View();
        }

        public ActionResult SendEmail()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SendEmail(string body, string to)
        {
            const int port = 587;
            const bool enableSsl = true;

            ViewBag.Message = string.Format("Settings: port {0} and SSL->{1}.  Using IP {2}", port, enableSsl, Request.ServerVariables["LOCAL_ADDR"]);
            
            try
            {
                var client = new SmtpClient("bulkmail-dev.ucdavis.edu") {UseDefaultCredentials = false};
                client.ClientCertificates.Add(new X509Certificate(Server.MapPath("~/prepurchasingtest.cer")));
                client.EnableSsl = enableSsl;
                client.Port = port;
                client.Send("srkirkland@ucdavis.edu", to, "bulkmail sample", body);
                
                ViewBag.Message = string.Format("Email sent at {0}", DateTime.Now);
            }
            catch (Exception exception)
            {
                ViewBag.Message = string.Format("Email send failed because: {0}", exception.GetBaseException().Message);
            }

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
