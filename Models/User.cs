using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication5.Models
{
    public class User
    {
        public virtual int Id { get; set; }

        public virtual string Nick { get; set; }

        public virtual string email { get; set; }


    }
}