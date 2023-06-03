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


        /*  public ActionResult TaskList()
          {
              using (var sessionFactory = CreateSessionFactory())
              {
                  using (var session = sessionFactory.OpenSession())
                  {
                      var taskViewModelList = new List<TaskViewModel>();
                      var taskList = session.Query<Task>().ToList();

                      foreach (var task in taskList)
                      {
                          var taskViewModel = new TaskViewModel();

                          // Izvršavanje upita za vlasnika koristeći spajanje tabela
                          var ownerQuery = session.Query<UserStory>()
                              .Join(session.Query<Usr>(), userStory => userStory.UserId, user => user.Id, (userStory, user) => new { UserStory = userStory, User = user })
                              .Where(joinResult => joinResult.UserStory.Id == task.UserStoryId)
                              .Select(joinResult => joinResult.User.UserName)
                              .FirstOrDefault();

                          // Izvršavanje upita za naslov priče korisnika
                          var userStoryTitleQuery = session.Query<UserStory>()
                              .Where(userStory => userStory.Id == task.UserStoryId)
                              .Select(userStory => userStory.Title)
                              .FirstOrDefault();

                          taskViewModel.owner = ownerQuery ?? "N/A"; // Ako vlasnik nije pronađen, postavljamo na "N/A"
                          taskViewModel.userStoryTitle = userStoryTitleQuery ?? "N/A"; // Ako naslov priče korisnika nije pronađen, postavljamo na "N/A"
                          taskViewModel.task = task;

                          taskViewModelList.Add(taskViewModel);
                      }

                      return View(taskViewModelList);
                  }
              }
          }
        */

        public ActionResult TaskList()
        {
            using (var sessionFactory = CreateSessionFactory())
            {
                using (var session = sessionFactory.OpenSession())
                {
                    var taskViewModelList = new List<TaskViewModel>();

                    var taskList = session.Query<Task>()
                        .Join(session.Query<UserStory>(), task => task.UserStoryId, userStory => userStory.Id, (task, userStory) => new { Task = task, UserStory = userStory })
                        .Join(session.Query<Usr>(), joinResult => joinResult.UserStory.UserId, user => user.Id, (joinResult, user) => new { Task = joinResult.Task, UserStory = joinResult.UserStory, User = user })
                        .ToList();

                    foreach (var item in taskList)
                    {
                        var taskViewModel = new TaskViewModel();
                        taskViewModel.Owner = item.User.UserName;
                        taskViewModel.UserStoryTitle = item.UserStory.Title;
                        taskViewModel.task = item.Task;

                        taskViewModelList.Add(taskViewModel);
                    }

                    return View(taskViewModelList);
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
        public ActionResult AddTask(TaskViewModel model)
        {
            //TODO: ovde bi vlasnik ovog taska trebalo da bude ulogovani korisnik, odn da bude task njegovog user story-a
            //za sada cu sve stavljati da budu taskovi user story-a 23. :D
            model.task.UserStoryId = 23;
            using (var sessionFactory = CreateSessionFactory())
            {
                using (var session = sessionFactory.OpenSession())
                {
                    using (var transaction = session.BeginTransaction())
                    {
                        session.SaveOrUpdate(model.task);
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