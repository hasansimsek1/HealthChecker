using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace HealthChecker.Persistence.SqlServer
{
    /// <summary>
    /// Architecture of this project needs to implement IDesignTimeDbContextFactory. 
    /// 
    /// <para/>
    /// 
    /// For more information you can visit
    /// <a cref="https://docs.microsoft.com/en-us/ef/core/miscellaneous/cli/dbcontext-creation">
    /// EF Core design-time DbContext creation on docs.microsoft.com
    /// </a>
    /// 
    /// </summary>
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            try
            {
                var configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();

                var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
                optionsBuilder.UseSqlServer(configuration.GetConnectionString("HealthCheckerDb"));

                return new AppDbContext(optionsBuilder.Options);
            }
            catch
            {
                // log
                throw;
            }
        }
    }
}
