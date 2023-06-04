using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication5.Models
{
    public class TaskViewModel
    {
        public String Owner { get; set; }
        public String UserStoryTitle { get; set; }

        public int userStoryId { get; set; }
        public Task task { get; set; }
    }
}