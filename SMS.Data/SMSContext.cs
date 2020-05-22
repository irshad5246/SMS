using Microsoft.AspNet.Identity.EntityFramework;
using SMS.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Data
{
   
    public class SMSContext : IdentityDbContext<SMSUser>
    {
        public SMSContext()
            : base("SMSConnection")
        {

        }

        public DbSet<Country> Countries { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<City> Cities { get; set; }
        //public DbSet<EndUser> EndUsers { get; set; }
        public DbSet<Image> Images { get; set; }

        public static SMSContext Create()
        {
            return new SMSContext();
        }

      


    }
}


