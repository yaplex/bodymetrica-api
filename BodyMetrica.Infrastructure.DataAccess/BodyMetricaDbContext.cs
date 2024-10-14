using BodyMetrica.Infrastructure.DataAccess.Weight;
using Microsoft.EntityFrameworkCore;

namespace BodyMetrica.Infrastructure.DataAccess;

public class BodyMetricaDbContext : DbContext
{
    public BodyMetricaDbContext(DbContextOptions<BodyMetricaDbContext> options)
        : base(options)
    {
    }

    public DbSet<WeightRecord> Weights { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<WeightRecord>(
            dob =>
            {
                dob.ToTable("WeightRecords");
                dob.HasKey(x => x.Id);
                dob.Property(o => o.WeightInKg).HasColumnName("WeightInKg");
                dob.Property(o => o.RecordDate).HasColumnName("RecordDate");
            });
    }
}