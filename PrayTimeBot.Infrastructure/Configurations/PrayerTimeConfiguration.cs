using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrayTimeBot.Domain;

namespace PrayTimeBot.Infrastructure.Configurations;

public class PrayerTimeConfiguration : IEntityTypeConfiguration<PrayerTime>
{
    public void Configure(EntityTypeBuilder<PrayerTime> entity)
    {
        entity.HasKey(p => p.Id);
        entity.Property(p => p.City).HasMaxLength(100).IsRequired();
        entity.Property(p => p.Country).HasMaxLength(100);
        entity.Property(p => p.Date).IsRequired();
        entity.Property(p => p.Method).HasMaxLength(100);

        entity.Property(p => p.Fajr).IsRequired();
        entity.Property(p => p.Sunrise).IsRequired();
        entity.Property(p => p.Dhuhr).IsRequired();
        entity.Property(p => p.Asr).IsRequired();
        entity.Property(p => p.Maghrib).IsRequired();
        entity.Property(p => p.Isha).IsRequired();
    }
}
