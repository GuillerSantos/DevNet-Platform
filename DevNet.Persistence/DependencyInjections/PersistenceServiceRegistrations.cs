using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace DevNet.Persistence.DependencyInjections
{
    public static class PersistenceServiceRegistrations
    {
        #region Public Methods

        public static IServiceCollection AddPersistenceServices(
          this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException(
                    "Connection string 'DefaultConnection' not found.");

            var assembly = Assembly.GetExecutingAssembly();

            foreach (var type in assembly.GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract &&
                    (t.Name.EndsWith("Repository"))))
            {
                var interfaces = type.GetInterfaces();
                foreach (var iface in interfaces)
                {
                    services.AddScoped(iface, type);
                }
            }

            return services;
        }

        #endregion Public Methods
    }
}