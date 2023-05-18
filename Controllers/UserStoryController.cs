using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
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
                        storyList = session.Query<UserStory>().Take(10).ToList();
                        return View(storyList);
                    }
                }
            
            
           
        }
    }
}