using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace WebApplication5.Controllers
{
    //[Authorize]
    public class HomeController : Controller
    {

        

        // Tabelarni prikaz user story-a
        public ActionResult UserStories()
        {
            
            return View();
        }

        // Tabelarni prikaz taskova
        public ActionResult Tasks()
        {
            return View();
        }

        
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "O aplikaciji";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Kontakt stranica";

            return View();
        }
    }
}