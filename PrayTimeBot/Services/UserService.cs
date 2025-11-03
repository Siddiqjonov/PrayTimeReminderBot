using Microsoft.EntityFrameworkCore;
using PrayTimeBot.Domain;
using PrayTimeBot.Infrastructure;
using Telegram.Bot.Types;

namespace PrayTimeBot.Services;

public class UserService(MainContext mainContext)
{
    private readonly MainContext _mainContext = mainContext;

    public async Task<long> CreateUser(Message message)
    {
        var user = new Domain.User
        {
            ChatId = message.Chat.Id,
            Username = message.From?.Username,
            FirstName = message.From?.FirstName,
            City = "Toshkent",
            LanguageCode = message.From?.LanguageCode,
            SendReminders = true,
            Format = 1,
            ReminderHour = 4,
            CreatedAt = DateTime.UtcNow,
        };

        await _mainContext.Users.AddAsync(user);
        await _mainContext.SaveChangesAsync();
        return user.ChatId;
    }

    public async Task<Domain.User?> GetUserById(long chatId) =>
         await _mainContext.Users.FirstOrDefaultAsync(u => u.ChatId == chatId);
    

    public void UpdateRegion(long chatId, string city)
    {
        var user = _mainContext.Users.FirstOrDefault(x => x.ChatId == chatId);
        if (user == null) return;

        user.City = city;
        _mainContext.SaveChanges();
    }

    public void UpdateFormat(long chatId, int format)
    {
        var user = _mainContext.Users.FirstOrDefault(x => x.ChatId == chatId);
        if (user == null) return;

        user.Format = format;
        _mainContext.SaveChanges();
    }

    public void UpdateReminder(long chatId, int reminderHour)
    {
        var user = _mainContext.Users.FirstOrDefault(x => x.ChatId == chatId);
        if (user == null) return;

        user.ReminderHour = reminderHour;
        _mainContext.SaveChanges();
    }
}
