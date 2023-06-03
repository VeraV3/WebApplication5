using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentNHibernate.Mapping;

namespace WebApplication5.Models
{
    public class TaskMap : ClassMap<Task>
    {
        public TaskMap()
        {
            Table("task");
            Id(x => x.Id).Column("id").GeneratedBy.Native();//.GeneratedBy.Identity();
            Map(x => x.Title).Column("title");
            Map(x => x.Description).Column("description");
            Map(x => x.UserStoryId).Column("userstoryid");
        }
    }
}