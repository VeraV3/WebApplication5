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

       // [HttpPost]
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


            userStory.Id = 999;
            userStory.UserId = 1;
            
            using (var sessionFactory = CreateSessionFactory())
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
            


            /*session.Close();
            sessionFactory.Close();*/

            // return RedirectToAction("Index", "Home");
            return View();
        }
    }
}