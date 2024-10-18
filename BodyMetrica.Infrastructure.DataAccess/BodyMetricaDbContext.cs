using BodyMetrica.Domain.Common.Models;
using BodyMetrica.Infrastructure.DataAccess.Weight.Records;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BodyMetrica.Infrastructure.DataAccess;

public class BodyMetricaDbContext : DbContext
{
    public BodyMetricaDbContext(DbContextOptions<BodyMetricaDbContext> options)
        : base(options)
    {
    }

    public DbSet<WeightLogRecord> WeightLogs { get; set; }
    public DbSet<User> ApplicationUsers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<WeightLogRecord>(
            entity =>
            {
                entity.ToTable("WeightLogs");
                entity.HasKey(x => x.Id);
                entity.Property(o => o.OwnerId).HasColumnName("OwnerId");
                entity.Property(o => o.Weight).HasColumnName("WeightInKg");
                entity.Property(o => o.RecordDate).HasColumnName("RecordDate");
            });
        modelBuilder.Entity<User>(
            entity =>
            {
                entity.ToTable("Users");
                entity.HasKey(x => x.Id);
                entity.Property(o => o.ExternalId).HasColumnName("ExternalId");
                // entity.Property(o => o.WeightUnits).HasColumnName("WeightUnits")
                    // .HasConversion(new EnumToStringConverter<WeightUnits>());
                entity.Property(o => o.CreatedAt).HasColumnName("CreatedAt");
            });
    }
}