using DevNet.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DevNet.Persistence.Configurations.Users
{
    internal sealed class RefreshTokenConfig : IEntityTypeConfiguration<RefreshTokens>
    {
        #region Public Methods

        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<RefreshTokens> builder)
        {
            builder.ToTable("RefreshTokens");

            builder.HasKey(rt => rt.Id);

            builder.Property(rt => rt.RefreshToken)
                .IsRequired()
                .HasMaxLength(256);

            builder.Property(rt => rt.Email)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(rt => rt.ExpiresAt)
                .IsRequired();

            builder.Property(rt => rt.UpdatedAt)
                .IsRequired();

            builder.Property(rt => rt.IsRevoked)
                .IsRequired();

            builder.HasOne(rt => rt.ApplicationUser)
                .WithMany(au => au.RefreshTokens)
                .HasForeignKey(rt => rt.ApplicationUserId)
                .OnDelete(DeleteBehavior.Cascade);
        }

        #endregion Public Methods
    }
}