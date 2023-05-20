using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace WebApplication5.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {

        // Akcije dostupne samo za prijavljene korisnike

        // Tabelarni prikaz user story-a
        public ActionResult UserStories()
        {
            // Implementirajte logiku za prikaz user story-a iz baze
            return View();
        }

        // Tabelarni prikaz taskova
        public ActionResult Tasks()
        {
            // Implementirajte logiku za prikaz taskova iz baze
            return View();
        }

        // Ostale akcije
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}