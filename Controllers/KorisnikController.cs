﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using WebApplication5.Models;
using NHibernate.Tool.hbm2ddl;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Driver;
using Npgsql;
//using FluentNHibernate.Tool.hbm2ddl;

namespace WebApplication5.Controllers
{
    public class KorisnikController : Controller
    {

  /*      private static ISessionFactory CreateSessionFactory()
        {
            var sessionFactory = Fluently.Configure()
                .Database(PostgreSQLConfiguration.Standard.ConnectionString("Server=localhost;Port=5432;Database=mojabaza;User Id=postgres;Password=1234;"))
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Korisnik>().Add<KorisnikMap>()) 
                .ExposeConfiguration(cfg => new SchemaUpdate(cfg).Execute(false, true))
                .BuildSessionFactory();

            return sessionFactory;
        }
*/
 /*       private static ISessionFactory CreateSessionFactory()
        {
            var configuration = new Configuration();
            configuration.DataBaseIntegration(x =>
            {
                x.ConnectionString = "Server=localhost;Port=5432;Database=mojabaza;User Id=postgres;Password=1234;";
                x.Driver<NHibernate.Driver.NpgsqlDriver>();
                x.Dialect<PostgreSQL82Dialect>();
            });

            configuration.Add(typeof(KorisnikMap));

            return configuration.BuildSessionFactory();
        }*/

        private static ISessionFactory CreateSessionFactory()
        {
            var configuration = new Configuration();
            configuration.DataBaseIntegration(x =>
            {
                x.ConnectionString = "Server=localhost;Port=5432;Database=mojabaza;User Id=postgres;Password=1234;";
                x.Driver<NpgsqlDriver>();
                x.Dialect<PostgreSQL82Dialect>();
            });

            configuration.AddAssembly(typeof(KorisnikMap).Assembly);

            return configuration.BuildSessionFactory();
        }


        public ActionResult Index()
        {
            /*using (var sessionFactory = CreateSessionFactory())
            {
                using (var session = sessionFactory.OpenSession())
                {
                    var korisnik = session.Get<Korisnik>(9);
                    ViewBag.text = "PROSLEDJUJEM username devetog korisnika" + korisnik.UserName;
                    return View(korisnik);
                }
            }*/
            Korisnik korisnik = new Korisnik();
            List<Korisnik> listaKorisnika = new List<Korisnik>();
            string connectionString = "Server=localhost;Port=5432;Database=mojabaza;User Id=postgres;Password=1234;";
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM users";
                using (var command = new NpgsqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                           
                            int id = reader.GetInt32(0);
                            string username = reader.GetString(1);
                            string email = reader.GetString(2);

                            //Korisnik korisnik = new Korisnik();
                            korisnik.Id = id;
                            korisnik.UserName = username;
                            korisnik.Email = email;
                            //TODO sredi putanju da bude portabilno
                            string filePath = "C:\\Users\\Korisnik\\source\\repos\\WebApplication5\\App_Data\\izlazIzPrograma.txt";
                            string message = $"ID: {id}, Username: {username}, Email: {email}";
                            System.IO.File.AppendAllText(filePath, message + System.Environment.NewLine);
                            listaKorisnika.Add(korisnik);
                        }
                    }
                }


            }
           // ViewBag.Korisnici = listaKorisnika;
            return View(listaKorisnika);

        }


        /*  private static ISessionFactory CreateSessionFactory()
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
                     return View(korisnik);
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


        //return View();        }
    }
}