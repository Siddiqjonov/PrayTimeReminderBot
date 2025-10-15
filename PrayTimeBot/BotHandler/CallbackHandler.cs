using PrayTimeBot.Helpers;
using PrayTimeBot.Infrastructure;
using PrayTimeBot.Services;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace PrayTimeBot.BotHandler;

internal static class CallbackHandler
{
    private static UserService? _userService;
    private static ITelegramBotClient? _bot;
    private static PrayerTimeService? _prayerTimeService;

    public static void InitializePrayerTimeService(PrayerTimeService prayerTimeService)
       => _prayerTimeService = prayerTimeService ?? throw new ArgumentNullException(nameof(prayerTimeService));

    public static void InitializeUserService(UserService userService)
        => _userService = userService ?? throw new ArgumentNullException(nameof(userService));

    public static void InitializeBotClient(ITelegramBotClient bot)
        => _bot = bot ?? throw new ArgumentNullException(nameof(bot));

    public static async Task HandleRegionSelectionAsync(CallbackQuery query)
    {
        var chatId = query.Message!.Chat.Id;
        var city = query.Data!.Split("_")[1];

        _userService!.UpdateRegion(chatId, city);

        var time = await _prayerTimeService!.GetTodayAsync(city);
        if (time == null)
        {
            await _bot!.EditMessageText(chatId, query.Message.MessageId, "❌ Namoz vaqtlari olinmadi.");
            return;
        }

        var inlineButtons = new InlineKeyboardMarkup(new[]
        {
        new[] { InlineKeyboardButton.WithCallbackData("🕓 Formatni o‘zgartirish", "change_format") },
        new[] { InlineKeyboardButton.WithCallbackData("📍 Hududni o‘zgartirish", "change_region") }
        });

        var user = _userService.GetOrCreate(chatId, null, null);
        string text = user.Format switch
        {
            2 => PrayerTimeFormatter.ElegantFormat(time),
            3 => PrayerTimeFormatter.DuaFormat(time),
            4 => PrayerTimeFormatter.MinimalFormat(time),
            5 => PrayerTimeFormatter.DecorativeFormat(time),
            _ => PrayerTimeFormatter.ClassicFormat(time)
        };

        //await _bot!.EditMessageText(
        //    chatId,
        //    query.Message.MessageId,
        //    text,
        //    parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown,
        //    replyMarkup: inlineButtons
        //);

        await _bot!.DeleteMessage(chatId, query.Message.MessageId);

        await _bot!.SendMessage(
            chatId,
            text,
            parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown,
            replyMarkup: inlineButtons
        );

    }

    public static async Task HandleRegionChangeRequestAsync(CallbackQuery query)
    {
        var chatId = query.Message!.Chat.Id;

        await _bot!.EditMessageText(
            chatId,
            query.Message.MessageId,
            "♻️ Iltimos, yangi viloyatni tanlang:",
            replyMarkup: KeyboardHelper.GetRegionKeyboard()
        );
    }

    public static async Task HandleFormatSelectionAsync(CallbackQuery query)
    {
        var chatId = query.Message!.Chat.Id;

        var formatKeyboard = new InlineKeyboardMarkup(new[]
        {
        new[] { InlineKeyboardButton.WithCallbackData("1️⃣ Klassik format", "format_1") },
        new[] { InlineKeyboardButton.WithCallbackData("2️⃣ Nafis format", "format_2") },
        new[] { InlineKeyboardButton.WithCallbackData("3️⃣ Dua formati", "format_3") },
        new[] { InlineKeyboardButton.WithCallbackData("4️⃣ Minimal format", "format_4") },
        new[] { InlineKeyboardButton.WithCallbackData("5️⃣ Bezatilgan format", "format_5") },
    });

        await _bot!.EditMessageText(
            chatId,
            query.Message.MessageId,
            "🕓 Iltimos, yangi formatni tanlang:",
            replyMarkup: formatKeyboard
        );
    }

    public static async Task HandleFormatChosenAsync(CallbackQuery query)
    {
        var chatId = query.Message!.Chat.Id;
        var selected = int.Parse(query.Data!.Split("_")[1]);

        _userService!.UpdateFormat(chatId, selected);

        var user = _userService.GetOrCreate(chatId, null, null);
        var time = await _prayerTimeService!.GetTodayAsync(user.City);

        if (time == null)
        {
            await _bot!.EditMessageText(chatId, query.Message.MessageId, "❌ Namoz vaqtlari olinmadi.");
            return;
        }

        // Prepare inline buttons for further actions
        var inlineButtons = new InlineKeyboardMarkup(new[]
        {
        new[] { InlineKeyboardButton.WithCallbackData("🕓 Formatni o‘zgartirish", "change_format") },
        new[] { InlineKeyboardButton.WithCallbackData("📍 Hududni o‘zgartirish", "change_region") }
        });

        // Format prayer times based on the selected format
        string text = selected switch
        {
            2 => PrayerTimeFormatter.ElegantFormat(time),
            3 => PrayerTimeFormatter.DuaFormat(time),
            4 => PrayerTimeFormatter.MinimalFormat(time),
            5 => PrayerTimeFormatter.DecorativeFormat(time),
            _ => PrayerTimeFormatter.ClassicFormat(time)
        };

        // Show prayer times in the selected format
        await _bot!.EditMessageText(
            chatId,
            query.Message.MessageId,
            text,
            parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown,
            replyMarkup: inlineButtons
        );
    }

    public static async Task HandleReminderSelectionAsync(CallbackQuery query)
    {
        var chatId = query.Message!.Chat.Id;
        var userId = query.From.Id;
        var data = query.Data!; // e.g. "reminder_14"

        if (!data.StartsWith("reminder_"))
            return;

        // Extract selected hour
        var hour = int.Parse(data.Replace("reminder_", ""));

        // ✅ Update user reminder time in DB
        _userService!.UpdateReminder(chatId, hour);

        // ✉️ Confirm update
        await _bot!.EditMessageText(
            chatId,
            query.Message.MessageId,
            $"✅ Eslatma vaqti {hour:00}:00 ga o‘rnatildi."
        );
    }
}
