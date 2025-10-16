using PrayTimeBot.Helpers;
using PrayTimeBot.Services;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace PrayTimeBot.BotHandler;

internal static class CommandHandler
{
    private static UserService? _userService;
    private static ITelegramBotClient? _bot;
    private static PrayerTimeService? _prayerTimeService;

    public static void InitializeServices(ITelegramBotClient? bot, UserService? userService, PrayerTimeService prayerTimeService)
    { _prayerTimeService = prayerTimeService; _bot = bot; _userService = userService; }

    public static async Task HandleStartAsync(Message message)
    {
        var firstName = message.From?.FirstName ?? "Do’st";
        var telegramId = message.From?.Id;
        var chatId = message.Chat.Id;

        // 1️⃣ Greeting message
        await _bot!.SendMessage(
            chatId,
            $"👋 Assalomu alaykum [{firstName}](tg://user?id={telegramId})!",
            replyMarkup: ReplyKeyboardHelper.GetMainMenuKeyboard(),
            parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown
        );

        // 2️⃣ Prompt with inline region keyboard
        await _bot!.SendMessage(
            chatId,
            "O‘zingizga kerakli viloyatni tanlang:",
            replyMarkup: InlineKeyboardHelper.GetRegionKeyboard()
        );
    }

    public static async Task HandleTodaysPrayTimeAsync(Message message)
    {
        var chatId = message.Chat.Id;

        var user = _userService!.GetOrCreate(chatId, null, null);
        var time = await _prayerTimeService!.GetTodayAsync(user.City);

        if (time == null)
        {
            await _bot!.SendMessage(chatId, "❌ Namoz vaqtlari olinmadi.");
            return;
        }

        string text = user.Format switch
        {
            2 => PrayerTimeFormatter.ElegantFormat(time),
            3 => PrayerTimeFormatter.DuaFormat(time),
            4 => PrayerTimeFormatter.MinimalFormat(time),
            5 => PrayerTimeFormatter.DecorativeFormat(time),
            _ => PrayerTimeFormatter.ClassicFormat(time)
        };

        var inlineButtons = new InlineKeyboardMarkup(new[]
        {
        new[] { InlineKeyboardButton.WithCallbackData("🕓 Formatni o‘zgartirish", "change_format") },
        new[] { InlineKeyboardButton.WithCallbackData("📍 Hududni o‘zgartirish", "change_region") }
        });

        await _bot!.SendMessage(
            chatId,
            text,
            parseMode: ParseMode.Markdown,
            replyMarkup: inlineButtons
        );
    }
    public static async Task HandleChangeReminderTimeAsync(Message message)
    {
        var chatId = message.Chat.Id;

        var buttons = Enumerable.Range(0, 24)
            .Select(h => InlineKeyboardButton.WithCallbackData($"{h:00}:00", $"reminder_{h}"))
            .Chunk(4) // 🔥 6 rows, each with 4 buttons
            .Select(chunk => chunk.ToArray())
            .ToArray();

        var keyboard = new InlineKeyboardMarkup(buttons);

        await _bot!.SendMessage(
            chatId,
            "⏱️ Iltimos, eslatma vaqtini tanlang:",
            replyMarkup: keyboard
        );
    }
}
