// Wms.Infrastructure/Data/Configurations/AppTokenConfiguration.cs

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wms.Domain.Entities;

namespace Wms.Infrastructure.Data.Configurations;

public class AppTokenConfiguration : IEntityTypeConfiguration<AppToken>
{
    public void Configure(EntityTypeBuilder<AppToken> builder)
    {
        builder.ToTable("AppTokens");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.UserId)
            .IsRequired();

        builder.Property(e => e.Token)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(e => e.ExpiresAt)
            .IsRequired();

        builder.Property(e => e.IsRevoked)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(e => e.RevokedAt);
        builder.Property(e => e.CreatedIpAddress)
            .HasMaxLength(45);
        builder.Property(e => e.LastUsedAt);
        builder.Property(e => e.LastUsedIpAddress)
            .HasMaxLength(45);

        builder.Property(e => e.CreatedAt)
            .IsRequired();

        builder.Property(e => e.UpdatedAt);

        // Relationships
        builder.HasOne(e => e.User)
            .WithMany()
            .HasForeignKey(e => e.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // Indexes
        builder.HasIndex(e => e.Token)
            .IsUnique();
        builder.HasIndex(e => e.UserId);
        builder.HasIndex(e => e.ExpiresAt);
        builder.HasIndex(e => e.IsRevoked);
    }
}

