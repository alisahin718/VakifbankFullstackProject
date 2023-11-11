using Business.Repositories.Manager;
using Business.Repositories.Service;
using DataAccess.Context;
using Hangfire;
using Hangfire.SqlServer;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Extensions
{
    public static class ServicesExtensions
    {
        public static void ConfigureSqlContext(this IServiceCollection services,
            IConfiguration configuration) =>
            services.AddDbContext<RepositoryContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("ConnectionStrings:Default")));

        public static void ConfigureHangfireContext(this IServiceCollection services,
            IConfiguration configuration)
        {
            var hangfireConnectionString = configuration["ConnectionStrings:BgJobsDb"];

            services.AddHangfire(config =>
            {
                var option = new SqlServerStorageOptions
                {
                    PrepareSchemaIfNecessary = true,
                    QueuePollInterval = TimeSpan.FromMinutes(5),
                    CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                    SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                    UseRecommendedIsolationLevel = true,
                    UsePageLocksOnDequeue = true,
                    DisableGlobalLocks = true
                };
                config.UseSqlServerStorage(hangfireConnectionString, option)
                .WithJobExpirationTimeout(TimeSpan.FromHours(6));
            });
            services.AddHangfireServer();
        }

        public static void ConfigureLoggerService(this IServiceCollection services) => 
            services.AddSingleton<ILoggerService, LoggerManager>();



    }
}
