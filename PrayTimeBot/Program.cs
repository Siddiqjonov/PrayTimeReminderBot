using PrayTimeBot.Configuraitons;
using PrayTimeBot.Infrastructure;
using PrayTimeBot.Services;
using Telegram.Bot;

namespace PrayTimeBot;

public static class Program
{
    public static async Task Main(string[] args)
    {
        var settings = new BotSettings
        {
            Token = Environment.GetEnvironmentVariable("BOT_TOKEN") ?? "your_fallback_token_here"
        };

        var bot = new TelegramBotClient(settings.Token);

        try
        {
            var me = await bot.GetMe();
            Console.WriteLine($"✅ Bot ok: @{me.Username} (id: {me.Id})");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ GetMe failed: {ex.Message}");
            throw;
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
            Console.WriteLine($"❌ GetWebhookInfo/DeleteWebhook failed: {ex.Message}");
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
             await botService.HandleUpdate(update);
         },
         async (client, exception, errorSource, ct) =>
         {
             Console.WriteLine($"Error in receiver: {exception.Message} (source: {errorSource})");
             await Task.CompletedTask;
         });

        Console.WriteLine("✅ StartReceiving called.");

        // keep container alive
        Thread.Sleep(Timeout.Infinite);
    }
}