using DevNet.Api;
using Serilog;

public class Program
{
    #region Public Methods

    public static async Task Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .CreateBootstrapLogger();

        Log.Information("Starting DevNet");

        var builder = WebApplication.CreateBuilder(args);

        builder.Host.UseSerilog((context, services, configuration) =>
            configuration
                .ReadFrom.Configuration(context.Configuration)
                .ReadFrom.Services(services)
                .Enrich.FromLogContext()
                .WriteTo.Console(),
                true);

        var app = builder
            .ConfigureServices()
            .ConfigurePipeline();

        app.UseSerilogRequestLogging();

        // await app.ResetDatabaseAsync();

        app.Run();
    }

    #endregion Public Methods
}