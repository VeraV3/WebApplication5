using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication5.Models
{
    public class UserStoryViewModel
    {
        public String Owner { get; set; }
        public UserStory userStory { get; set; }
    }
}