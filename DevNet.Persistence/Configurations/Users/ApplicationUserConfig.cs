using DevNet.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevNet.Persistence.Configurations.Users
{
    internal sealed class ApplicationUserConfig : IEntityTypeConfiguration<ApplicationUser>
    {
        #region Public Methods

        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.ToTable("ApplicationUsers");

            builder.HasKey(au => au.Id);
            builder.Property(au => au.FirstName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(au => au.LastName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(au => au.Email)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(au => au.PasswordHash)
                .IsRequired();

            builder.Property(au => au.Role)
                .IsRequired();
        }

        #endregion Public Methods
    }
}