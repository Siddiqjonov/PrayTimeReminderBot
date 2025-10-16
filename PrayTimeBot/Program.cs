using PrayTimeBot.Infrastructure;
using PrayTimeBot.Services;
using Telegram.Bot;

namespace PrayTimeBot;

public static class Program
{
    public static async Task Main(string[] args)
    {
        var token = Environment.GetEnvironmentVariable("BOT_TOKEN")?.Trim();

        if (string.IsNullOrEmpty(token))
        {
            Console.WriteLine("❌ BOT_TOKEN missing or invalid. Set BOT_TOKEN env var (no quotes).");
            return;
        }

        var bot = new TelegramBotClient(token);

        try
        {
            var me = await bot.GetMe();
            Console.WriteLine($"✅ Bot ok: @{me.Username} (id: {me.Id})");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ GetMe failed: {ex.Message}");
            return;
        }

        try
        {
            var wh = await bot.GetWebhookInfo();
            Console.WriteLine($"🔗 Webhook URL: {wh.Url ?? "<none>"}  Pending: {wh.PendingUpdateCount}");
            if (!string.IsNullOrEmpty(wh.Url))
            {
                Console.WriteLine("🗑 Deleting existing webhook to allow polling...");
                await bot.DeleteWebhook();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Webhook check/delete failed: {ex.Message}");
        }

        using var db = new MainContext();
        var userService = new UserService(db);
        var prayService = new PrayerTimeService();
        var botService = new TelegramBotService(bot, userService, prayService);

        Console.WriteLine("▶️ Starting receiving...");
        bot.StartReceiving(
            async (client, update, ct) =>
            {
                Console.WriteLine($"📩 Update received: {update?.Type}");
                await botService.HandleUpdate(update!);
            },
            async (client, exception, errorSource, ct) =>
            {
                Console.WriteLine($"Error in receiver: {exception.Message} (source: {errorSource})");
                await Task.CompletedTask;
            }
        );
        Console.WriteLine("✅ StartReceiving called.");

        Thread.Sleep(Timeout.Infinite);
    }
}
