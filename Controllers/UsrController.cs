using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using System.Linq;
using System.Web.Mvc;
using WebApplication5.Models;


namespace WebApplication5.Controllers
{
    public class UsrController : Controller
    {
        private static ISessionFactory CreateSessionFactory()
        {
            return Fluently.Configure()
                .Database(PostgreSQLConfiguration.Standard.ConnectionString("Server=localhost;Port=5432;Database=mojabaza;User Id=postgres;Password=1234;"))
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<UsrMap>())
                .BuildSessionFactory();
        }

        public ActionResult Index()
        {
            using (var sessionFactory = CreateSessionFactory())
            {
                using (var session = sessionFactory.OpenSession())
                {
                    var users = session.Query<Usr>().Take(10).ToList();
                    return View(users);
                }
            }
        }
    }
}
