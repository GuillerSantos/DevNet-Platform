using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace DevNet.Infrastructure.DependencyInjections
{
    public static class InsfrastructureServiceRegistrations
    {
        #region Public Methods

        public static IServiceCollection AddInfrastructureServices(
           this IServiceCollection services, IConfiguration configuration)
        {
            var assembly = Assembly.GetExecutingAssembly();

            foreach (var type in assembly.GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract &&
                    (t.Name.EndsWith("Service"))))
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