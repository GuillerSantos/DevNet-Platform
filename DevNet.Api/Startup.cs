using DevNet.Api.Middleware;
using DevNet.Application.DependencyInjections;
using DevNet.Infrastructure.DependencyInjections;
using DevNet.Persistence.DBContext;
using DevNet.Persistence.DependencyInjections;
using Microsoft.EntityFrameworkCore;

namespace DevNet.Api
{
    public static class Startup
    {
        #region Public Methods

        public static WebApplication ConfigureServices(
            this WebApplicationBuilder builder)
        {
            builder.Services.AddApplicationServices(builder.Configuration);
            builder.Services.AddInfrastructureServices(builder.Configuration);
            builder.Services.AddPersistenceServices(builder.Configuration);

            builder.Services.AddAuthorization();
            builder.Services.AddAntiforgery();
            builder.Services.AddControllers();
            builder.Services.AddOpenApi();

            var allowedOrigins = builder.Configuration
                .GetSection("AllowedOrigins")
                .Get<string[]>();

            builder.Services.AddCors(opt =>
            {
                opt.AddPolicy("open", policy =>
                policy.WithOrigins(allowedOrigins)
                      .AllowAnyMethod()
                      .AllowAnyHeader()
                      .SetPreflightMaxAge(TimeSpan.FromMinutes(10)));
            });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGenAuth();

            return builder.Build();
        }

        public static WebApplication ConfigurePipeline(this WebApplication app)
        {
            app.UseCors("open");

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseStaticFiles();
            app.UseMiddleware<GlobalExceptionMiddleware>();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseHttpsRedirection();
            app.MapControllers();

            return app;
        }

        public static async Task ResetDatabaseAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();

            try
            {
                var context = scope.ServiceProvider.GetService<AppDbContext>();
                if (context != null)
                {
                    await context.Database.EnsureDeletedAsync();
                    await context.Database.MigrateAsync();
                }
            }
            catch (Exception ex)
            {
                var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "An error occurred while resetting the database.");
            }
        }

        #endregion Public Methods
    }
}