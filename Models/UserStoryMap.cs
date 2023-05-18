using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentNHibernate.Mapping;


namespace WebApplication5.Models
{
    
        public class UserStoryMap : ClassMap<UserStory>
        {
            public UserStoryMap()
            {
                Table("userstory");
                Id(x => x.Id).Column("id").GeneratedBy.Identity();
                Map(x => x.Title).Column("title");
                Map(x => x.Description).Column("description");
                Map(x => x.UserId).Column("userid");
                //AutoMap();
            }
        }
    
}