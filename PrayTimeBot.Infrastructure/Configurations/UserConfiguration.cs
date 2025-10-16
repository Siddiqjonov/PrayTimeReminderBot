using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrayTimeBot.Domain;

namespace PrayTimeBot.Infrastructure.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> entity)
    {
        entity.HasKey(u => u.Id);
        entity.Property(u => u.ChatId).IsRequired();
        entity.Property(u => u.Username).HasMaxLength(100);
        entity.Property(u => u.FirstName).HasMaxLength(100);
        entity.Property(u => u.City).HasMaxLength(100);
        entity.Property(u => u.LanguageCode).HasMaxLength(10);
    }
}
