using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication5.Controllers
{
    public class AcountController : Controller
    //upravlja prijavljivanjem i odjavljivanjem korisnika
    {
        // Javna metoda za prikaz login stranice
       public ActionResult Login()
            {
                return View();
            }
      
            // Metoda za obradu prijave korisnika
            [HttpPost]
            public ActionResult Login(string username, string password)
            {
                // Provjera korisničkih podataka i autentifikacija
                if (IsValidUser(username, password))
                {
                    // Postavite sesiju ili koristite drugi mehanizam za označavanje korisnika kao prijavljenog
                    Session["Username"] = username;
                    return RedirectToAction("Index", "Home");
                }

                // Prikaz poruke o grešci ako prijava nije uspjela
                ViewBag.ErrorMessage = "Neispravni korisnički podaci.";
                return View();
            }

            // Javna metoda za odjavu korisnika
            public ActionResult Logout()
            {
                // Uklonite sesiju ili koristite drugi mehanizam za odjavu korisnika
                Session.Clear();
                return RedirectToAction("Index", "Home");
            }

            // Pomoćna metoda za provjeru korisničkih podataka
            private bool IsValidUser(string username, string password)
            {
            return false;
                // Implementirajte logiku za provjeru korisničkih podataka
                // Vratite true ako su ispravni, inače false
            }
        
    }
}

