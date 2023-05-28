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

        }
    
}