using DevNet.Application.Abstractions.Persistence;
using DevNet.Persistence.DBContext;
using DevNet.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DevNet.Persistence.DependencyInjections
{
    public static class PersistenceServiceRegistrations
    {
        #region Public Methods

        public static IServiceCollection AddPersistenceServices(
            this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>((sp, options) =>
            {
                var loggerFactory = sp.GetRequiredService<ILoggerFactory>();
                options.UseSqlServer(DbContextConfig.GetConnectionString())
                       .UseLoggerFactory(loggerFactory)
                       .UseLazyLoadingProxies();

                if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
                    options.EnableSensitiveDataLogging();
            });

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IAsyncBaseRepository<>), typeof(AsyncBaseRepository<>));

            var repositoryAssembly = typeof(ApplicationUserRepository).Assembly;

            var repositoryTypes = repositoryAssembly.GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract && t.Name.EndsWith("Repository"));

            foreach (var repoType in repositoryTypes)
            {
                var serviceInterfaces = repoType.GetInterfaces()
                    .Where(i => i != typeof(IAsyncBaseRepository<>));

                foreach (var serviceInterface in serviceInterfaces)
                {
                    services.AddScoped(serviceInterface, repoType);
                }
            }

            return services;
        }

        #endregion Public Methods
    }
}