using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication5.Models
{
    public class Task
    {
        public virtual int Id { get; set; }

        public virtual string Title { get; set; }

        public virtual string Description { get; set; }

        public virtual string Status { get; set; }

        public virtual string Owner { get; set; }

        public virtual UserStory UserStory { get; set; }

        public virtual bool Done { get; set; }

    }
}