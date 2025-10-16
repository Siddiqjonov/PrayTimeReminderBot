using Microsoft.EntityFrameworkCore;
using PrayTimeBot.Domain;
using PrayTimeBot.Infrastructure;
using Telegram.Bot.Types;

namespace PrayTimeBot.Services;

public class UserService
{
    private readonly MainContext _mainContext;
    public UserService(MainContext mainContext)
    {
        _mainContext = mainContext;
    }

    public async Task<long> CreateUser(Message message)
    {
        if (await _mainContext.Users.AnyAsync(u => u.ChatId == message.Chat.Id))
            Console.WriteLine($"❌ User with {message.Chat.Id} already exsists!");

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
        };

        await _mainContext.Users.AddAsync(user);
        await _mainContext.SaveChangesAsync();
        return user.ChatId;
    }

    public Domain.User GetOrCreate(long chatId, string? username, string? firstName)
    {
        var user = _mainContext.Users.FirstOrDefault(x => x.ChatId == chatId);
        if (user != null) return user;

        user = new Domain.User
        {
            ChatId = chatId,
            Username = username ?? "",
            FirstName = firstName ?? "",
            City = "Toshkent",
            Format = 1,
            CreatedAt = DateTime.UtcNow,
            SendReminders = true,
        };

        _mainContext.Users.Add(user);
        _mainContext.SaveChanges();
        return user;
    }

    public Domain.User? GetUserById(long chatId)
    {
        return _mainContext.Users.FirstOrDefault(u => u.ChatId == chatId);
    }

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
