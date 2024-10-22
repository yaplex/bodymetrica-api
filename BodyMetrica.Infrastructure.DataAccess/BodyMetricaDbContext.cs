using BodyMetrica.Core.Models;
using BodyMetrica.Infrastructure.DataAccess.Weight.Records;
using Microsoft.EntityFrameworkCore;

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
                entity.Property(o => o.Weight).HasColumnName("WeightInKg");
            });
        modelBuilder.Entity<User>(
            entity =>
            {
                entity.ToTable("Users");
                entity.HasKey(x => x.Id);
            });
    }
}