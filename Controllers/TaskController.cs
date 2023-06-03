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
                .ExposeConfiguration(cfg => { cfg.SetProperty(NHibernate.Cfg.Environment.UseProxyValidator, bool.FalseString); })
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

        public ActionResult Delete(int id)
        {
            using (var sessionFactory = Fluently.Configure()
               .Database(PostgreSQLConfiguration.Standard.ConnectionString("Server=localhost;Port=5432;Database=mojabaza;User Id=postgres;Password=1234;"))
               .Mappings(m => m.FluentMappings.AddFromAssemblyOf<TaskMap>())
               .BuildSessionFactory())
            {


                DeleteTask(id, sessionFactory);

                return RedirectToAction("TaskList", "Task");


            }


        }

        void DeleteTask(int taskId, ISessionFactory sessionFactory)
        {
            using (var session = sessionFactory.OpenSession())
            {
                var sqlQuery = session.CreateSQLQuery(@"
                         DELETE FROM task
                         WHERE id = :taskId
                     ");
                sqlQuery.SetInt32("taskId", taskId);

                using (var transaction = session.BeginTransaction())
                {
                    sqlQuery.ExecuteUpdate();
                    transaction.Commit();
                }
            }
        }


        public ActionResult AddTask()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddTask(Task task)
        {
            //TODO: ovde bi vlasnik ovog taska trebalo da bude ulogovani korisnik, odn da bude task njegovog user story-a
            //za sada cu sve stavljati da budu taskovi user story-a 23. :D
            task.UserStoryId = 23;
            using (var sessionFactory = CreateSessionFactory())
            {
                using (var session = sessionFactory.OpenSession())
                {
                    using (var transaction = session.BeginTransaction())
                    {
                        session.SaveOrUpdate(task);
                        transaction.Commit();
                    }
                }
            }

            return RedirectToAction("TaskList", "Task");
        }

        // Akcija za prikaz forme za uređivanje zadatka
        public ActionResult Edit(int id)
        {
            using (var sessionFactory = CreateSessionFactory())
            {
                using (var session = sessionFactory.OpenSession())
                {
                    var task = session.Get<Task>(id);
                    if (task == null)
                    {
                        return HttpNotFound();
                    }

                    return View(task);
                }
            }
        }

        // Akcija za izmenu zadatka
        [HttpPost]
        public ActionResult Edit(Task task)
        {
            using (var sessionFactory = CreateSessionFactory())
            {
                using (var session = sessionFactory.OpenSession())
                {
                    using (var transaction = session.BeginTransaction())
                    {
                        session.Update(task);
                        transaction.Commit();
                    }
                }
            }
            return RedirectToAction("TaskList");
        }

    }
}