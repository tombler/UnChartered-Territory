﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SchoolChoicePlayground.Models
{
    public class AppContext : DbContext
    {
        // IDbSet, IQueryable
        public virtual DbSet<School> Schools { get; set; }
        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Alert> Alerts { get; set; } 
    }
}