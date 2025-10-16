using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PrayTimeBot.Domain;
using PrayTimeBot.Infrastructure.Configurations;

namespace PrayTimeBot.Infrastructure;

public class MainContext : DbContext
{
    public DbSet<User> Users => Set<User>();
    public DbSet<PrayerTime> PrayerTimes => Set<PrayerTime>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var conn = Environment.GetEnvironmentVariable("DATABASE_URL")?.Trim();

        if (string.IsNullOrEmpty(conn))
            Console.WriteLine("❌ DATABASE_URL not found. Please set it in Railway Variables.");

        optionsBuilder.UseNpgsql(conn);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new PrayerTimeConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}
