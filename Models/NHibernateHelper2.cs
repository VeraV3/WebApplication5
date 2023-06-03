using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication5.Models;



    public class NHibernateHelper2
    {
        private static ISessionFactory _sessionFactory;

        public static ISession OpenSession()
        {
            if (_sessionFactory == null)
                _sessionFactory = CreateSessionFactory();

            return _sessionFactory.OpenSession();
        }

        public static ISessionFactory CreateSessionFactory()
        {
            return Fluently.Configure()
                .Database(PostgreSQLConfiguration.Standard.ConnectionString("Server=localhost;Port=5432;Database=mojabaza;User Id=postgres;Password=1234;Include Error Detail=true;"))
                .Mappings(m => m.FluentMappings
                    .AddFromAssemblyOf<UserStoryMap>()
                    .AddFromAssemblyOf<UsrMap>()
                    .AddFromAssemblyOf<TaskMap>()
                )
                .BuildSessionFactory();
        }
    }
