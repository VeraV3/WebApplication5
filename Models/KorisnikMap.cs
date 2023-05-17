using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentNHibernate.Mapping;

    namespace WebApplication5.Models
    {
        public class KorisnikMap : ClassMap<Korisnik>
        {
            public KorisnikMap()
            {
                Table("Users");
                Id(x => x.Id, "id").GeneratedBy.Identity();
                Map(x => x.UserName, "username");
                Map(x => x.Email, "email");
                Map(x => x.Password, "password");
            }
        }
    }

