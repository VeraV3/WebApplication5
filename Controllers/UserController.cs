using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Mapping.Attributes;
using Npgsql;
using WebApplication5.Models;

namespace WebApplication5.Controllers
{
    [Controller]
    public class UserController : ControllerBase
    {
        private ISessionFactory CreateSessionFactory()
        {
            var cfg = new Configuration();
            cfg.DataBaseIntegration(db =>
            {
                db.Dialect<PostgreSQL83Dialect>(); // Promenite na odgovarajući dialekt za vašu verziju PostgreSQL
                db.Driver<NpgsqlDriver>(); // Promenite na odgovarajući drajver za PostgreSQL
                db.ConnectionString = "Server=localhost;Port=5432;Database=mojabaza;User Id=postgres;Password=1234;"; // Prilagodite prema vašim informacijama o vezi
                db.LogSqlInConsole = true;
            });

            cfg.AddInputStream(HbmSerializer.Default.Serialize(typeof(User).Assembly));
            var mapping = cfg.BuildMapping();
            cfg.AddMapping((NHibernate.Cfg.MappingSchema.HbmMapping)mapping);

            return cfg.BuildSessionFactory();

        }

        [HttpGet]
        [Route("api/users")]
        public ActionResult<IEnumerable<User>> GetUsers()
        {
            var sessionFactory = CreateSessionFactory();

            using (var session = sessionFactory.OpenSession())
            {
                var users = session.Query<User>().Take(10).ToList();
                return Ok(users);
            }
        }
    }
}
