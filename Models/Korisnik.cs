﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate.Mapping.Attributes;

namespace WebApplication5.Models
{
    [Class(Table = "Users")]
    public class Korisnik
    {
        [Id(Name = "Id", Column = "id")]
        [Generator(1, Class = "identity")]
        public virtual int Id { get; set; }

        [Property(Column = "username")]
        public virtual string UserName { get; set; }

        [Property(Column = "email")]
        public virtual string Email { get; set; }

        [Property(Column = "password")]
        public virtual string Password { get; set; }

    }
}