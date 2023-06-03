using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication5.Models
{
    public class TaskViewModel
    {
        public Usr owner { get; set; }
        public Task task { get; set; }
    }
}