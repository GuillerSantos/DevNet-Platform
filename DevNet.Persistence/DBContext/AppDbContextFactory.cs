using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DevNet.Persistence.DBContext
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        #region Public Methods

        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

            optionsBuilder.UseSqlServer(DbContextConfig.GetConnectionString())
                          .UseLazyLoadingProxies();

            return new AppDbContext(optionsBuilder.Options);
        }

        #endregion Public Methods
    }
}