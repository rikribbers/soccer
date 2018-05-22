﻿using Microsoft.EntityFrameworkCore;

namespace Poule.Entities
{
    public class PouleDbContext : DbContext
    {
        public PouleDbContext(DbContextOptions<PouleDbContext> options)
            : base(options)
        {
        }

        public DbSet<Game> Games { get; set; }
        public DbSet<User> MyUsers { get; set; }
        public DbSet<Prediction> Predictions { get; set; }
    }
}