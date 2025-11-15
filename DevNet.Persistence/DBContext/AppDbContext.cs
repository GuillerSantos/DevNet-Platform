using DevNet.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DevNet.Persistence.DBContext
{
    public sealed class AppDbContext : DbContext
    {
        #region Public Constructors

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        public DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }

        #endregion Public Constructors
    }
}