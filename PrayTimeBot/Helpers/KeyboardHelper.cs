using Telegram.Bot.Types.ReplyMarkups;

namespace PrayTimeBot.Helpers;

public static class KeyboardHelper
{
    // Inline menu: only format & region
    public static InlineKeyboardMarkup GetInlineMenu()
    {
        return new InlineKeyboardMarkup(new[]
        {
            new []
            {
                InlineKeyboardButton.WithCallbackData("O’zgarish format", "menu_change_format"),
                InlineKeyboardButton.WithCallbackData("Hududni o’zgartirish", "menu_change_region")
            }
        });
    }

    // Non-inline buttons (just simple ReplyKeyboardMarkup)
    public static ReplyKeyboardMarkup GetNonInlineMenu()
    {
        return new ReplyKeyboardMarkup(new KeyboardButton[][]
        {
        new KeyboardButton[]
        {
            new KeyboardButton("⏰ Namoz vaqtini o’zgartirish"),
            new KeyboardButton("📅 Bugungi namoz vaqtlari")
        }
        })
        {
            ResizeKeyboard = true
        };
    }

    // Region keyboard for first selection
    public static InlineKeyboardMarkup GetRegionKeyboard()
    {
        var rows = new List<InlineKeyboardButton[]>
        {
            new [] { InlineKeyboardButton.WithCallbackData("🕌 Farg‘ona", "region_Fargona"), InlineKeyboardButton.WithCallbackData("🕌 Xiva", "region_Xiva") },
            new [] { InlineKeyboardButton.WithCallbackData("🕌 Toshkent", "region_Toshkent"), InlineKeyboardButton.WithCallbackData("🕌 Namangan", "region_Namangan") },
            new [] { InlineKeyboardButton.WithCallbackData("🕌 Buxoro", "region_Buxoro"), InlineKeyboardButton.WithCallbackData("🕌 Guliston", "region_Guliston") },
            new [] { InlineKeyboardButton.WithCallbackData("🕌 Jizzax", "region_Jizzax"), InlineKeyboardButton.WithCallbackData("🕌 Zarafshon", "region_Zarafshon") },
            new [] { InlineKeyboardButton.WithCallbackData("🕌 Qarshi", "region_Qarshi"), InlineKeyboardButton.WithCallbackData("🕌 Navoiy", "region_Navoiy") },
            new [] { InlineKeyboardButton.WithCallbackData("🕌 Nukus", "region_Nukus"), InlineKeyboardButton.WithCallbackData("🕌 Samarqand", "region_Samarqand") },
            new [] { InlineKeyboardButton.WithCallbackData("🕌 Termiz", "region_Termiz"), InlineKeyboardButton.WithCallbackData("🕌 Urganch", "region_Urganch") },
            new [] { InlineKeyboardButton.WithCallbackData("🕌 Andijon", "region_Andijon") }
        };
        return new InlineKeyboardMarkup(rows);
    }
}
