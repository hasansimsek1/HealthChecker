using HealthChecker.Persistence.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HealthChecker.Persistence.SqlServer
{



    /// <summary>
    /// Database context of the application. Inherits from <see cref="IdentityDbContext"/> and does the necessary persistence operations of application entities and membership management system.
    /// </summary>
    public class AppDbContext : IdentityDbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }


        public DbSet<Target> Targets { get; set; }
        public DbSet<HealthCheck> HealthChecks { get; set; }
    }
}
