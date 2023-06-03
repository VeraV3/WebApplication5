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
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Web.Mvc;
using WebApplication5.Models;
using NHibernate;
using NHibernate.Cfg;
using System.Web.SessionState;


namespace WebApplication5.Controllers
{
    public class AccountController : Controller
    {
        private ISessionFactory _sessionFactory;

        public AccountController()
        {

            _sessionFactory = NHibernateHelper2.CreateSessionFactory();
        }

        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login()
        {
            
            return View();
        }

        // POST: /Account/Login
        [HttpPost]
       // [AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            

            // Provera korisničkih podataka
            using (var session = _sessionFactory.OpenSession())
            {
                var user = session.QueryOver<Usr>()
                    .Where(u => u.Email == model.Email && u.Password == model.Password)
                    .SingleOrDefault();

                if (user != null && (!ModelState.IsValid) )
                {
                    // Postavi sesiju za prijavljenog korisnika
                    Session["Username"] = user.UserName;

                    // Redirektuj na odredište
                    return RedirectToAction("Index", "Home");
                }

                else
                {
                    ModelState.AddModelError("", "Neispravni podaci za prijavljivanje.");
                    return View(model);
                }
            }
        }

        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(Usr model)
        {
            if (ModelState.IsValid)
            {
                // Kreiraj novog korisnika
                var user = new Usr
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    Password = model.Password
                };

                // Sačuvaj korisnika
                using (var session = _sessionFactory.OpenSession())
                {
                    using (var transaction = session.BeginTransaction())
                    {
                        session.Save(user);
                        transaction.Commit();
                    }
                }

                // Prijavi korisnika

                // Redirektuj na odredište
                return RedirectToAction("Index", "Home");
            }

            // Ako nešto nije u redu, ponovno prikaži formu za registraciju
            return View(model);
        }

        // POST: /Account/Logout
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            if (Session["Username"] != null)
            {
                Session.Clear();
                return RedirectToAction("Login", "Account");
            }

            // Redirektuj na početnu stranicu mozda ovo u else granu  ViewBag.ErrorMessage = "Nema ulogovanog korisnika";return View();
            return RedirectToAction("Index", "Home");
        }

            // Metoda za lokalno preusmeravanje
            private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        // Oslobađanje resursa
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _sessionFactory.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool IsValidUser(string username, string password)
        {
           
            using (var session = _sessionFactory.OpenSession())
            {
                var user = session.QueryOver<Usr>()
                    .Where(u => u.UserName == username)
                    .SingleOrDefault();

                if (user != null && user.Password == password)
                {
                    return true;
                }
            }

            return false;
        }
    }
}


