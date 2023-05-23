using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication5.Models;

namespace WebApplication5.Controllers
{
    public class AccountController : Controller
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
                // Provera korisničkih podataka i autentifikacija
                if (IsValidUser(username, password))
                {
                    // Postavite sesiju ili koristite drugi mehanizam za označavanje korisnika kao prijavljenog
                    Session["Username"] = username;
                    return RedirectToAction("Index", "Home");
                }

                // Prikaz poruke o grešci ako prijava nije uspela
                ViewBag.ErrorMessage = "Neispravni korisnički podaci.";
                return View();
            }

            // Javna metoda za odjavu korisnika
            public ActionResult Logout()
            {
                
                Session.Clear();
                return RedirectToAction("Index", "Home");
            }

        private static ISessionFactory CreateSessionFactory()
        {
            return Fluently.Configure()
                .Database(PostgreSQLConfiguration.Standard.ConnectionString("Server=localhost;Port=5432;Database=mojabaza;User Id=postgres;Password=1234;"))
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Usr>())
                .BuildSessionFactory();
        }

        private bool IsValidUser(string username, string password)
        {
            using (var sessionFactory = CreateSessionFactory())
            using (var session = sessionFactory.OpenSession())
            {
                var user = session.QueryOver<Usr>()
                    .Where(u => u.UserName == username)
                    .SingleOrDefault();

                if (user != null && user.Password == password)
                {
                    return true; // Korisnički podaci su ispravni
                }
            }

            return false; // Korisnički podaci nisu ispravni
        }

    }
}

