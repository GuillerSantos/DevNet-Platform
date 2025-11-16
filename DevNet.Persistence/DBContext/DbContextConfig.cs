using Microsoft.Extensions.Configuration;

namespace DevNet.Persistence.DBContext
{
    public static class DbContextConfig
    {
        #region Public Methods

        public static string GetConnectionString()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            return configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        }

        #endregion Public Methods
    }
}