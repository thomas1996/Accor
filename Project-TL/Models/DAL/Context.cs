using Project_TL.Models.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Project_TL.Models.DAL
{
    public class Context : DbContext
    {
        public Context() : base("Project") { }

        public DbSet<Branch> Branches { get; set; }
        public DbSet<Firm> Firms { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Owner> Owner { get; set; }
        public DbSet<Syst> Systems { get; set; }
        public DbSet<User> Users { get; set; }

    }
}