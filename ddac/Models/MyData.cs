using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace ddac.Models
{
    public class MyData : DbContext
    {
        public MyData() : base("MyData") { }
        public DbSet<location> locations { get; set; }
        public DbSet<country> countrys { get; set; }
        public DbSet<cruise> cruises { get; set; }
        public DbSet<ship> ships { get; set; }
        public DbSet<Trip> Trips { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
    }
}