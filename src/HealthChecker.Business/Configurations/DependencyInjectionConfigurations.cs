using Hangfire;
using Hangfire.SqlServer;
using HealthChecker.Business.Contracts;
using HealthChecker.Business.Services;
using HealthChecker.Persistence;
using HealthChecker.Persistence.Contracts;
using HealthChecker.Persistence.Dtos;
using HealthChecker.Persistence.Entities;
using HealthChecker.Persistence.Repositories;
using HealthChecker.Persistence.SqlServer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace HealthChecker.Business.Configurations
{
    /// <summary>
    /// Extensions for IServiceCollection interface.
    /// </summary>
    public static class DependencyInjectionConfigurations
    {
        /// <summary>
        /// Registers application services to the container. 
        /// </summary>
        public static void AddAppServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContextPool<AppDbContext>(options => { options.UseSqlServer(config.GetConnectionString("HealthCheckerDb")); });
            services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>();

            services.AddHangfire(configuration => configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage(config.GetConnectionString("HealthCheckerDb"), new SqlServerStorageOptions
                {
                    CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                    SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                    QueuePollInterval = TimeSpan.Zero,
                    UseRecommendedIsolationLevel = true,
                    DisableGlobalLocks = true
                }));
            services.AddHangfireServer();

            services.AddAutoMapper(typeof(AutoMapperMappings));

            services.AddScoped(typeof(ICrudRepository<,>), typeof(SqlRespository<,>));
            services.AddScoped(typeof(ITargetRepository), typeof(SqlRespository<Target, TargetDto>));
            services.AddScoped<ITargetService, TargetService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IBackgroundJobService, HangfireJobService>();

            /* 
             * !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
             * 
             * NOTE : 
             *      These settings are not configured with security in mind. 
             *      The goal is to ease the usage of the app in development. 
             *      Change them before deploying to anywhere.
             * 
             * !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
             * 
             */
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 4;
                options.Password.RequiredUniqueChars = 1;

                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(50);
                options.Lockout.MaxFailedAccessAttempts = 50;
                options.Lockout.AllowedForNewUsers = false;

                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = false;

                options.SignIn.RequireConfirmedAccount = false;
                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;
            });

            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

                options.LoginPath = "/Account/Login";
                options.AccessDeniedPath = "/Account/AccessDenied";
                options.SlidingExpiration = true;
            });
        }
    }
}
