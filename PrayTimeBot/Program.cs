using PrayTimeBot.Configuraitons;
using PrayTimeBot.Infrastructure;
using PrayTimeBot.Services;
using Telegram.Bot;

namespace PrayTimeBot;

public static class Program
{
    static void Main(string[] args)
    {
        var settings = new BotSettings
        {
            Token = Environment.GetEnvironmentVariable("BOT_TOKEN") ?? "8201869983:AAFqvD_PUmWfCb_Ub_V-NN1oIiKt2APb1YI"
        };

        var bot = new TelegramBotClient(settings.Token);
        using var db = new MainContext();

        var userService = new UserService(db);
        var prayService = new PrayerTimeService();
        var botService = new TelegramBotService(bot, userService, prayService);

        bot.StartReceiving(
            async (client, update, ct) => await botService.HandleUpdate(update),
            (_, ex, _) => Console.WriteLine($"Error: {ex.Message}")
        );

        Console.WriteLine("✅ PrayTimeBot running...");
        Console.ReadLine();
    }
}
