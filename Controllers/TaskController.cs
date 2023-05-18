using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication5.Models;

namespace WebApplication5.Controllers
{
    public class TaskController : Controller
    {

        private static ISessionFactory CreateSessionFactory()
        {
            return Fluently.Configure()
                .Database(PostgreSQLConfiguration.Standard.ConnectionString("Server=localhost;Port=5432;Database=mojabaza;User Id=postgres;Password=1234;"))
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<TaskMap>())
                .BuildSessionFactory();
        }
        // GET: Task
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult TaskList()
        {
            
            using (var sessionFactory = CreateSessionFactory())
            {
                using (var session = sessionFactory.OpenSession())
                {
                    List<Task> taskList = new List<Task>();
                    taskList = session.Query<Task>().ToList();
                    return View(taskList);
                }
            }

            
        }
    }
}