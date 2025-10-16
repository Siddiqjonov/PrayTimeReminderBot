using Telegram.Bot.Types.ReplyMarkups;

namespace PrayTimeBot.Helpers;

public static class InlineKeyboardHelper
{
    public static InlineKeyboardMarkup GetChangeFormatAndRegionKeyboard(bool format = true, bool region = true)
    {
        if (format && region)
        {
            return new InlineKeyboardMarkup(
            [
            [InlineKeyboardButton.WithCallbackData("🕓 Formatni o‘zgartirish", "change_format")],
            [InlineKeyboardButton.WithCallbackData("📍 Hududni o‘zgartirish", "change_region")]
            ]);
        }
        else if (format && !region)
        {
            return new InlineKeyboardMarkup(
            [[InlineKeyboardButton.WithCallbackData("🕓 Formatni o‘zgartirish", "change_format")]]);
        }
        else
        {
            return new InlineKeyboardMarkup(
            [[InlineKeyboardButton.WithCallbackData("📍 Hududni o‘zgartirish", "change_region")]]);
        }
    }

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
