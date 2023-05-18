using NHibernate.Mapping.Attributes;

namespace WebApplication5.Models
{
    namespace WebApplication5.Models
    {
        public class Usr
        {
            public virtual int Id { get; set; }
            public virtual string UserName { get; set; }
            public virtual string Email { get; set; }
            public virtual string Password { get; set; }
        }
    }
}
