﻿using FluentNHibernate.Cfg;
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
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            using (var session = _sessionFactory.OpenSession())
            {
                var user = session.QueryOver<Usr>()
                    .Where(u => u.Email == model.Email && u.Password == model.Password)
                    .SingleOrDefault();
                if (user != null)
                {
                    Session["Username"] = user.UserName;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid login credentials.");
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
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new Usr
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    Password = model.Password
                };

                
                using (var session = _sessionFactory.OpenSession())
                {
                    using (var transaction = session.BeginTransaction())
                    {
                        session.Save(user); 
                        transaction.Commit();
                    }
                }

                // TODO prijavi korisnika 
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }

        // POST: /Account/Logout
        //[HttpPost]
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


