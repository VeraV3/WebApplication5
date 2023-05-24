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



public class AccountController : Controller
{
    private readonly UserManager<Usr> _userManager;
    private readonly SignInManager<Usr> _signInManager;

    public AccountController()
    {

    }
    public AccountController(UserManager<Usr> userManager, SignInManager<Usr> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    // Akcija za prikaz forme za registraciju
    public ActionResult Register()
    {
        return View();
    }

    // Akcija za procesiranje podataka iz forme za registraciju
    [System.Web.Mvc.HttpPost]
    public async Task<ActionResult> Register(RegisterViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = new Usr { UserName = model.Email, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToAction("Index", "Home");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }

        return View(model);
    }

    // Akcija za prikaz forme za logovanje
    public ActionResult Login()
    {
        return View();
    }

    // Akcija za procesiranje podataka iz forme za logovanje
    [System.Web.Mvc.HttpPost]
    public async Task<ActionResult> Login(LoginViewModel model, string returnUrl = null)
    {
        if (ModelState.IsValid)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Invalid login attempt.");
            }
        }

        return View(model);
    }

    // Akcija za odjavljivanje
    [HttpPost]
    public async Task<ActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }
}







/*
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
      
            
            [HttpPost]
            public ActionResult Login(string username, string password)
            {
                
                if (IsValidUser(username, password))
                {
                  
                    Session["Username"] = username;
                return RedirectToAction("Logout", "Account");
                  //  return RedirectToAction("Index", "Home");
                }

              
                ViewBag.ErrorMessage = "Neispravni korisnički podaci.";
                return View();
            }

      //  [HttpPost]
       // [ValidateAntiForgeryToken]
        public ActionResult Logout()
            {

            if (Session["Username"] != null)
            {
                Session.Clear();
                return RedirectToAction("Login", "Account");
            }
            else 
            { 
                ViewBag.ErrorMessage = "Nema ulogovanog korisnika"; 
                 return View();
            }
              
            

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
                    return true; 
                }
            }

            return false; 
        }

    }
}

*/