using Microsoft.EntityFrameworkCore;
using PrayTimeBot.Domain;
using PrayTimeBot.Infrastructure.Configurations;

namespace PrayTimeBot.Infrastructure;

public class MainContext : DbContext
{
    public DbSet<User> Users => Set<User>();
    public DbSet<PrayerTime> PrayerTimes => Set<PrayerTime>();

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        var conn = Environment.GetEnvironmentVariable("DATABASE_URL");

        if (string.IsNullOrEmpty(conn))
            throw new Exception("DATABASE_URL not found. Please set it in Railway Variables.");

        options.UseNpgsql(conn);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new PrayerTimeConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}
