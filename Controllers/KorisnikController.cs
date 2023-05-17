using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Linq;
using System.Web.Mvc;
using WebApplication5.Models;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Driver;
using System.IO;
using NHibernate.Mapping.Attributes;

namespace WebApplication5.Controllers
{
    public class KorisnikController : Controller
    {
        private static ISessionFactory CreateSessionFactory()
        {
            
            var configuration = new Configuration();
            configuration.DataBaseIntegration(x =>
            {
                x.ConnectionString = "Server=localhost;Port=5432;Database=mojabaza;User Id=postgres;Password=1234;";
                x.Driver<NpgsqlDriver>();
                x.Dialect<PostgreSQL82Dialect>();
            });
            //configuration.AddClass(typeof(Usr));  zbog ovog trazi xml mapiranje a radim preko atributskog
            //configuration.AddClass(typeof(Korisnik));
            configuration.AddAssembly(typeof(Korisnik).Assembly);
            return configuration.BuildSessionFactory();
        }

        // GET: Korisnik
        public ActionResult Index()
        {
            using (var sessionFactory = CreateSessionFactory())
            {
                using (var session = sessionFactory.OpenSession())
                {
                    var korisnik = session.Get<Korisnik>(9);
                    ViewBag.text = "PROSLEDJUJEM OVAJ TEKST" + korisnik.UserName;
                    // var korisnici = session.Query<Korisnik>().Take(10).ToList();
                     return View();
                }
            }



            /*

            string s;
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Korisnik.hbm.xml");
            // bool fileExists = File.Exists(filePath);
            bool fileExists = System.IO.File.Exists(filePath);
            if (fileExists)
            {
                s = "postoji";
            }
            else
            {
                s = "ne postoji";
            }*/

           
            //return View();
        }
    }
}