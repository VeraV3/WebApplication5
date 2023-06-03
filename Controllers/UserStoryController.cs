﻿using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Mapping.ByCode;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebApplication5.Models;


namespace WebApplication5.Controllers
{
    public class UserStoryController : Controller
    {
        
        private static ISessionFactory CreateSessionFactory()
        {
            return Fluently.Configure()
                .Database(PostgreSQLConfiguration.Standard.ConnectionString("Server=localhost;Port=5432;Database=mojabaza;User Id=postgres;Password=1234;Include Error Detail=true;"))
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<UserStoryMap>())
                .BuildSessionFactory();
        }

        // GET: UserStory
        public ActionResult Index()
        {
            return View();
        }

        /*  public ActionResult UserStoryList()
          {

                  using (var sessionFactory = CreateSessionFactory())
                  {
                      using (var session = sessionFactory.OpenSession())
                      {   List<UserStory> storyList = new List<UserStory>();
                          storyList = session.Query<UserStory>().ToList();
                          //storyList = session.Query<UserStory>().Take(10).ToList();
                      return View(storyList);
                      }
                  }



          }
        */


        public ActionResult UserStoryList(string owner)
        {
            using (var sessionFactory = CreateSessionFactory())
            {
                using (var session = sessionFactory.OpenSession())
                {
                    IQueryable<UserStory> query = session.Query<UserStory>();
                    List<UserStory> storyList = query.ToList();
                    List<UserStoryViewModel> usViewModels = new List<UserStoryViewModel>();

                    foreach (UserStory story in storyList)
                    {
                        Usr user = session.Query<Usr>().FirstOrDefault(u => u.Id == story.UserId);

                   
                        if (user != null)
                        {
                            
                            UserStoryViewModel viewModel = new UserStoryViewModel
                            {
                                Owner = user.UserName,
                                userStory = story
                            };

                            usViewModels.Add(viewModel);
                        }
                    }

                    if (!string.IsNullOrEmpty(owner))
                    {
                        usViewModels = usViewModels.Where(us => us.Owner == owner).ToList();
                    }

                    if (Request.IsAjaxRequest())
                    {
                        return PartialView("_UserStoryListPartial", usViewModels);
                    }

                    return View(usViewModels);
                }
            }
        }



        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(UserStory userStory)
        {
            
            if (ModelState.IsValid)
            {
             
                int nextId;
                using (var sessionFactory = CreateSessionFactory())
                {
                    using (var session = sessionFactory.OpenSession())
                    {
                        var maxId = session.Query<UserStory>().Max(us => us.Id);
                        nextId = maxId + 1;
                    }
                }

                
                userStory.Id = nextId;

                
                using (var sessionFactory = CreateSessionFactory())
                {
                    using (var session = sessionFactory.OpenSession())
                    {
                        using (var transaction = session.BeginTransaction())
                        {
                            session.SaveOrUpdate(userStory);
                            transaction.Commit();
                        }
                    }
                }

                return RedirectToAction("UserStoryList");
            }

            // Ako model nije validan, ponovno prikazujemo formu za unos sa greškama
            return View(userStory);
        }

        public ActionResult Edit(int id)
        {
            using (var sessionFactory = CreateSessionFactory())
            {
                using (var session = sessionFactory.OpenSession())
                {
                    var userStory = session.Get<UserStory>(id);
                    if (userStory == null)
                    {
                        return HttpNotFound();
                    }

                    return View(userStory);
                }
            }
        }
       
        [HttpPost]
        public ActionResult Edit(UserStory userStory)
        {
            using (var sessionFactory = CreateSessionFactory())
            {
                using (var session = sessionFactory.OpenSession())
                {
                    var existingUserStory = session.Get<UserStory>(userStory.Id);
                    if (existingUserStory == null)
                    {
                        return HttpNotFound();
                    }

                    existingUserStory.Title = userStory.Title;
                    existingUserStory.Description = userStory.Description;

                    using (var transaction = session.BeginTransaction())
                    {
                        session.Update(existingUserStory);
                        transaction.Commit();
                    }
                }
            }
            return RedirectToAction("UserStoryList");
        }

        public ActionResult Delete(int id)
        {
            using (var sessionFactory = Fluently.Configure()
               .Database(PostgreSQLConfiguration.Standard.ConnectionString("Server=localhost;Port=5432;Database=mojabaza;User Id=postgres;Password=1234;"))
               .Mappings(m => m.FluentMappings.AddFromAssemblyOf<TaskMap>())
               .BuildSessionFactory())
            {

                DeleteTasksForUserStory(id, sessionFactory);
                DeleteUserStory(id, sessionFactory);

                return RedirectToAction("UserStoryList", "UserStory");
 

            }

            
        }

        //TODO ispravi logiku i dodaj userStrory ovde detailsModelView, linkove sredi
        public ActionResult Details(int id)
        {
            using (var sessionFactory = Fluently.Configure()
                .Database(PostgreSQLConfiguration.Standard.ConnectionString("Server=localhost;Port=5432;Database=mojabaza;User Id=postgres;Password=1234;"))
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<TaskMap>())
                .BuildSessionFactory()) 
            {

                List<Task> taskList = GetTasksForUserStory(id, sessionFactory);
        
                 
                return View(taskList);
                
            }

        }
        
        List<Task> GetTasksForUserStory(int userStoryId, ISessionFactory sessionFactory)
        {
            using (var session = sessionFactory.OpenSession())
            {
                var sqlQuery = session.CreateSQLQuery(@"
                    SELECT t.id, t.title, t.description, t.userstoryid
                    FROM task t
                    JOIN userstory us ON t.userstoryid = us.id
                    WHERE us.id = :userStoryId
                ");
                sqlQuery.SetInt32("userStoryId", userStoryId);
                sqlQuery.AddEntity(typeof(Task));

                return sqlQuery.List<Task>().ToList<Task>();
            }
        }
        
        void DeleteTasksForUserStory(int userStoryId, ISessionFactory sessionFactory)
        {
            using (var session = sessionFactory.OpenSession())
            {
                var sqlQuery = session.CreateSQLQuery(@"
                  DELETE FROM task
                  WHERE userstoryid = :userStoryId
                ");
                sqlQuery.SetInt32("userStoryId", userStoryId);

                using (var transaction = session.BeginTransaction())
                {
                    sqlQuery.ExecuteUpdate();
                    transaction.Commit();
                }
            }
        }
        
        void DeleteUserStory(int userStoryId, ISessionFactory sessionFactory)
        {
            using (var session = sessionFactory.OpenSession())
            {
                var sqlQuery = session.CreateSQLQuery(@"
                    DELETE FROM userstory
                    WHERE id = :userStoryId
                 ");
                sqlQuery.SetInt32("userStoryId", userStoryId);

                using (var transaction = session.BeginTransaction())
                {
                    sqlQuery.ExecuteUpdate();
                    transaction.Commit();
                }
            }
        }


    }
}