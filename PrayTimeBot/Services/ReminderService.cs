using Microsoft.EntityFrameworkCore;
using PrayTimeBot.Helpers;
using PrayTimeBot.Infrastructure;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;

namespace PrayTimeBot.Services;

public class ReminderService : IDisposable
{
    private readonly ITelegramBotClient _bot;
    private readonly Timer _timer;
    private readonly PrayerTimeService _prayerTimeService;

    public ReminderService(ITelegramBotClient bot, PrayerTimeService prayerTimeService)
    {
        _bot = bot;
        _prayerTimeService = prayerTimeService;

        // Run CheckReminders every minute
        _timer = new Timer(async _ => await CheckReminders(), null, TimeSpan.Zero, TimeSpan.FromMinutes(1));
    }

    private async Task CheckReminders()
    {
        using var db = new MainContext();
        var now = DateTime.UtcNow;

        // Get users whose reminder is set for this hour
        var users = await db.Users
            .Where(u => u.SendReminders && u.ReminderHour == now.Hour)
            .ToListAsync();

        foreach (var user in users)
        {
            try
            {
                var time = await _prayerTimeService.GetTodaysPrayTimeAsync(user.City!);

                if (time == null)
                {
                    await _bot.SendMessage(
                        user.ChatId,
                        "❌ Namoz vaqtlari olinmadi.\nIltimos, menudagi \"⏰Bugungi nomoz vaqtlari\" tugmasini qayta bosing",
                        replyMarkup: ReplyKeyboardHelper.GetMainMenuKeyboard(),
                        parseMode: ParseMode.Markdown
                    );
                    continue;
                }

                string text = user.Format switch
                {
                    2 => PrayerTimeFormatter.ElegantFormat(time),
                    3 => PrayerTimeFormatter.DuaFormat(time),
                    4 => PrayerTimeFormatter.MinimalFormat(time),
                    5 => PrayerTimeFormatter.DecorativeFormat(time),
                    _ => PrayerTimeFormatter.ClassicFormat(time)
                };

                await _bot.SendMessage(
                    user.ChatId,
                    text,
                    parseMode: ParseMode.Markdown,
                    replyMarkup: InlineKeyboardHelper.GetChangeFormatAndRegionKeyboard()
                );
            }
            catch
            {
                Console.WriteLine($"Notification failed for user {user.ChatId} at {now}");
            }
        }
    }

    public void Dispose() => _timer?.Dispose();
}
