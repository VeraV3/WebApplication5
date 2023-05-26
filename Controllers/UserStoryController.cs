using FluentNHibernate.Cfg;
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
                .Database(PostgreSQLConfiguration.Standard.ConnectionString("Server=localhost;Port=5432;Database=mojabaza;User Id=postgres;Password=1234;"))
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<UserStoryMap>())
                .BuildSessionFactory();
        }

        // GET: UserStory
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UserStoryList()
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

        //[HttpPost]
        public ActionResult Create(UserStory userStory)
        {
            /*FormCollection formCollection
            UserStory userStory = new UserStory();
            userStory.Title = formCollection["Title"];
            userStory.Description = formCollection["Description"];
            */





            /*
            var configuration = new Configuration();
            configuration.Configure(); 
            var mapper = new ModelMapper();
            mapper.AddMapping(typeof(UserStoryMap)); 

            configuration.AddMapping(mapper.CompileMappingForAllExplicitlyAddedEntities());

            ISessionFactory sessionFactory = configuration.BuildSessionFactory();
            ISession session = sessionFactory.OpenSession();
            */


            
            /*userStory.UserId = 1;

            using (var sessionFactory = Fluently.Configure()
                    .Database(PostgreSQLConfiguration.Standard.ConnectionString("Server=localhost;Port=5432;Database=mojabaza;User Id=postgres;Password=1234;"))
                    .Mappings(m => m.FluentMappings.AddFromAssemblyOf<UserStoryMap>())
                    .BuildSessionFactory()) 
            {
                using (var session = sessionFactory.OpenSession())
                {
                    using (var transaction = session.BeginTransaction())
                    {
                        session.Save(userStory);
                        transaction.Commit();
                    }

                }
            }
            */
            


            /*session.Close();
            sessionFactory.Close();*/

             return RedirectToAction("Index", "Home");
            
        }

       
       // [HttpPost]
        //[Route("UserStory/Edit/{id}")]
        public ActionResult Edit(UserStory userStory)
        {
            if (ModelState.IsValid)
            {
                using (var sessionFactory = CreateSessionFactory())
                {
                    using (var session = sessionFactory.OpenSession())
                    {
                        // Učitajte postojeću priču iz baze podataka na osnovu ID-ja
                        var existingUserStory = session.Get<UserStory>(userStory.Id);
                        if (existingUserStory == null)
                        {
                            return HttpNotFound();
                        }

                        // Ažurirajte polja sa novim vrednostima
                        existingUserStory.Title = userStory.Title;
                        existingUserStory.Description = userStory.Description;

                        // Sačuvajte ažuriranu priču u bazu podataka
                        using (var transaction = session.BeginTransaction())
                        {
                            session.Update(existingUserStory);
                            transaction.Commit();
                        }

                        return RedirectToAction("UserStoryList");
                    }
                }
            }

            // Ako je ModelState neispravan, ponovno prikažite formu sa validacionim porukama
            return View(userStory);
        }

        /*
            return RedirectToAction("UserStoryList", "UserStory");
        }   
    */

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
    }
}