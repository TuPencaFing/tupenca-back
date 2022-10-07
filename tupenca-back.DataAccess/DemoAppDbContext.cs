﻿using Microsoft.EntityFrameworkCore;
using tupenca_back.Model;

namespace tupenca_back.DataAccess
{
    public class AppDbContext : DbContext
    {
      
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) 
        {
        }

        public DbSet<User>? Users { get; set; }
        public DbSet<Campeonato>? Campeonatos { get; set; }

    }

}
