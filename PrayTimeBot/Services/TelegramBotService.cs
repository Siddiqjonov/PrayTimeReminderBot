using PrayTimeBot.BotHandler;
using PrayTimeBot.Configuraitons;
using PrayTimeBot.Helpers;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace PrayTimeBot.Services;

public class TelegramBotService(ITelegramBotClient bot, UserService userService, PrayerTimeService prayService)
{
    private readonly ITelegramBotClient _bot = bot;
    private readonly UserService _userService = userService;
    private readonly PrayerTimeService _prayService = prayService;

    public async Task HandleUpdate(Update update)
    {
        if (update.Message != null)
            await HandleMessage(update.Message);
        else if (update.CallbackQuery != null)
            await HandleCallback(update.CallbackQuery);
    }

    private async Task HandleMessage(Message message)
    {
        CommandHandler.InitializeBotClient(_bot);
        CommandHandler.InitializeUserService(_userService);
        CommandHandler.InitializePrayerTimeService(_prayService);

        if (message.Text == "/start")
            await CommandHandler.HandleStartAsync(message);
        else if(message.Text == "⏰ Bugungi namoz vaqtlari")
            await CommandHandler.HandleTodaysPrayTimeAsync(message);
        else if(message.Text == "⏱️ Eslatma vaqtini o‘zgartirish")
            await CommandHandler.HandleChangeReminderTimeAsync(message);
    }

    private async Task HandleCallback(CallbackQuery query)
    {
        CallbackHandler.InitializePrayerTimeService(_prayService);
        CallbackHandler.InitializeBotClient(_bot);
        CallbackHandler.InitializeUserService(_userService);

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
