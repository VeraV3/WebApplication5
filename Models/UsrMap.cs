using FluentNHibernate.Mapping;

namespace WebApplication5.Models
{
    public class UsrMap : ClassMap<Usr>
    {
        public UsrMap()
        {
            Table("Users");
            Id(x => x.Id).Column("id").GeneratedBy.Native(); 
            Map(x => x.UserName).Column("username");
            Map(x => x.Email).Column("email");
            Map(x => x.Password).Column("password");
        }
    }
}


/*using FluentNHibernate.Mapping;


namespace WebApplication5.Models
{
    public class UsrMap : ClassMap<Usr>
    {
        public UsrMap()
        {
            Table("Users");
            Id(x => x.Id).Column("id").GeneratedBy.Identity();
            Map(x => x.UserName).Column("username");
            Map(x => x.Email).Column("email");
            Map(x => x.Password).Column("password");
        }
    }
}
*/