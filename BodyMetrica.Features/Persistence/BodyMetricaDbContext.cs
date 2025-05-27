using Microsoft.EntityFrameworkCore;

namespace BodyMetrica.Features.Persistence;

public class BodyMetricaDbContext : DbContext
{
    public BodyMetricaDbContext(DbContextOptions<BodyMetricaDbContext> options)
        : base(options)
    {
    }

    public DbSet<WeightLogRecord> WeightLogs { get; set; }
    public DbSet<BloodPressureLogRecord> BloodPressureLogs { get; set; }
    public DbSet<BodyMetricaUserRecord> ApplicationUsers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<WeightLogRecord>(
            entity =>
            {
                entity.ToTable("WeightLogs");
                entity.HasKey(x => x.Id);
                entity.Property(o => o.Weight).HasColumnName("WeightInKg");
            });
        modelBuilder.Entity<BodyMetricaUserRecord>(
            entity =>
            {
                entity.ToTable("Users");
                entity.HasKey(x => x.Id);
            });

        modelBuilder.Entity<BloodPressureLogRecord>(
            entity =>
            {
                entity.ToTable("BloodPressureLogs");
                entity.HasKey(x => x.Id);
            });
    }
}