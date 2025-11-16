using DevNet.Application.Common.Behaviors;
using FluentValidation;
using MediatR;
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
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            var assembly = Assembly.GetExecutingAssembly();
            foreach (var type in assembly.GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract && t.Name.EndsWith("Service")))
            {
                foreach (var iface in type.GetInterfaces())
                {
                    services.AddScoped(iface, type);
                }
            }

            return services;
        }

        #endregion Public Methods
    }
}