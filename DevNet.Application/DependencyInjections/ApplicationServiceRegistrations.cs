using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace DevNet.Application.DependencyInjections
{
    public static class ApplicationServiceRegistrations
    {
        #region Public Methods

        public static IServiceCollection AddApplicationServices(
            this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

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