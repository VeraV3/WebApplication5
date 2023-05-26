using NHibernate;
using NHibernate.Cfg;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using WebApplication5.Models;

public class ApplicationDbContext
{
    private readonly ISessionFactory _sessionFactory;

    public ApplicationDbContext(string connectionString)
    {
        var configuration = new Configuration();
        configuration.DataBaseIntegration(db =>
        {
            db.ConnectionString = connectionString;
            db.Dialect<NHibernate.Dialect.PostgreSQL83Dialect>();
        });

        var modelMapper = new ModelMapper();
      //  modelMapper.AddMapping<UsrMap>();

        var hbmMapping = modelMapper.CompileMappingForAllExplicitlyAddedEntities();
        configuration.AddMapping(hbmMapping);

        _sessionFactory = configuration.BuildSessionFactory();
    }

    public void RegisterUser(Usr user)
    {
        using (var session = OpenSession())
        using (var transaction = session.BeginTransaction())
        {
            session.Save(user);
            transaction.Commit();
        }
    }

    public Usr GetUserByUsername(string username)
    {
        using (var session = OpenSession())
        {
            return session.QueryOver<Usr>()
                .Where(u => u.UserName == username)
                .SingleOrDefault();
        }
    }

    private ISession OpenSession()
    {
        return _sessionFactory.OpenSession();
    }
}
