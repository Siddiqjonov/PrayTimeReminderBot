using PrayTimeBot.Domain;
using PrayTimeBot.Infrastructure;

namespace PrayTimeBot.Services;

public class UserService
{
    private readonly MainContext _db;
    public UserService(MainContext db) => _db = db;

    public User GetOrCreate(long chatId, string? username, string? firstName)
    {
        var user = _db.Users.FirstOrDefault(x => x.ChatId == chatId);
        if (user != null) return user;

        user = new User
        {
            ChatId = chatId,
            Username = username ?? "",
            FirstName = firstName ?? "",
            City = "Toshkent",
            Format = 1, // ✅ Default format
            CreatedAt = DateTime.UtcNow,
            SendReminders = true,
        };

        _db.Users.Add(user);
        _db.SaveChanges();
        return user;
    }

    public void UpdateRegion(long chatId, string city)
    {
        var user = _db.Users.FirstOrDefault(x => x.ChatId == chatId);
        if (user == null) return;

        user.City = city;
        _db.SaveChanges();
    }

    public void UpdateFormat(long chatId, int format)
    {
        var user = _db.Users.FirstOrDefault(x => x.ChatId == chatId);
        if (user == null) return;

        user.Format = format;
        _db.SaveChanges();
    }

    public void UpdateReminder(long chatId, int reminderHour)
    {
        var user = _db.Users.FirstOrDefault(x => x.ChatId == chatId);
        if (user == null) return;

        user.ReminderHour = reminderHour;
        _db.SaveChanges();
    }
}
