using BodyMetrica.Domain.Common.Models;
using BodyMetrica.Domain.Weight.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BodyMetrica.Infrastructure.DataAccess;

public class BodyMetricaDbContext : DbContext
{
    public BodyMetricaDbContext(DbContextOptions<BodyMetricaDbContext> options)
        : base(options)
    {
    }

    public DbSet<WeightLogRecord> WeightLogs { get; set; }
    public DbSet<UserProfile> ApplicationUsers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<WeightLogRecord>(
            entity =>
            {
                entity.ToTable("WeightLogs");
                entity.HasKey(x => x.Id);
                entity.Property(o => o.UserId).HasColumnName("UserId");
                entity.Property(o => o.WeightInKg).HasColumnName("WeightInKg");
                entity.Property(o => o.RecordDate).HasColumnName("RecordDate");
            });
        modelBuilder.Entity<UserProfile>(
            entity =>
            {
                entity.ToTable("ApplicationUsers");
                entity.HasKey(x => x.Id);
                entity.Property(o => o.ExternalId).HasColumnName("ExternalId");
                entity.Property(o => o.WeightUnits).HasColumnName("WeightUnits");
                entity.Property(o => o.CreatedAt).HasColumnName("CreatedAt");
            });
    }
}