using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Poule.Models;

namespace Poule.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Poule.Models.Contact> Contact { get; set; }


        public DbSet<Game> Games { get; set; }
        public DbSet<User> MyUsers { get; set; }
        public DbSet<Prediction> Predictions { get; set; }
    }
}
