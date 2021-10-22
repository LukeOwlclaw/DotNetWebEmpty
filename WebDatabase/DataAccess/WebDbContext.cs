using System;
using System.Collections.Generic;
using Database.Models.BO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

// Ignore spelling:
namespace Database.DataAccess
{
    public class WebDbContext : DbContext
    {
        public WebDbContext(DbContextOptions<WebDbContext> options) : base(options)
        {
            // Disable lazy loading so we have control about database calls
            ChangeTracker.LazyLoadingEnabled = false;
        }

        public DbSet<Sample> Sample => Set<Sample>();
    }
}
