using PrayTimeBot.BotHandler;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace PrayTimeBot.Services;

public class TelegramBotService(ITelegramBotClient _bot, UserService _userService, PrayerTimeService _prayService)
{
    public async Task HandleUpdate(Update update)
    {
        Console.WriteLine($"📩 Update received: {update.Type}");

        if (update.Message != null)
            await HandleMessage(update.Message);
        else if (update.CallbackQuery != null)
            await HandleCallback(update.CallbackQuery);
    }

    private async Task HandleMessage(Message message)
    {
        CommandHandler.InitializeServices(_bot, _userService, _prayService);

        if (message.Text == "/start")
            await CommandHandler.HandleStartAsync(message);
        else if (message.Text == "⏰ Bugungi namoz vaqtlari")
            await CommandHandler.HandleTodaysPrayTimeAsync(message);
        else if (message.Text == "⏱️ Eslatma vaqtini o‘zgartirish")
            await CommandHandler.HandleChangeReminderTimeAsync(message);
    }

    private async Task HandleCallback(CallbackQuery query)
    {
        CallbackHandler.InitializeServices(_bot, _userService, _prayService);

        if (query.Data!.StartsWith("region_"))
            await CallbackHandler.HandleRegionSelectionAsync(query);
        else if (query.Data == "change_region")
            await CallbackHandler.HandleRegionChangeRequestAsync(query);
        else if (query.Data == "change_format")
            await CallbackHandler.HandleFormatSelectionAsync(query);
        else if (query.Data.StartsWith("format_"))
            await CallbackHandler.HandleFormatChosenAsync(query);
        if (query.Data!.StartsWith("reminder_"))
            await CallbackHandler.HandleReminderSelectionAsync(query);
    }
}
