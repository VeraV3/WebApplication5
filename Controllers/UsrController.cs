using System.Linq;
using System.Web.Mvc;
using WebApplication5.Models;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Mapping.Attributes;

namespace WebApplication5.Controllers
{
    public class UserController : Controller
    {
        private static ISessionFactory CreateSessionFactory()
        {
            var configuration = new Configuration();
            configuration.DataBaseIntegration(x =>
            {
                x.ConnectionString = "Server=localhost;Port=5432;Database=mojabaza;User Id=postgres;Password=1234;";
                x.Driver<NpgsqlDriver>();
                x.Dialect<PostgreSQL82Dialect>();
            });
            //configuration.AddClass(typeof(Usr));  zbog ovog trazi xml mapiranje a radim preko atributskog
            configuration.AddAssembly(typeof(Usr).Assembly);
            return configuration.BuildSessionFactory();
        }

        public ActionResult Index()
        {
            using (var sessionFactory = CreateSessionFactory())
            using (var session = sessionFactory.OpenSession())
            {
                var users = session.Query<Usr>().Take(10).ToList();
                return View(users);
            }
        }
    }
}
