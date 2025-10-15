using Telegram.Bot.Types.ReplyMarkups;

namespace PrayTimeBot.Helpers;

public static class ReplyKeyboardHelper
{
    public static ReplyKeyboardMarkup GetMainMenuKeyboard()
    {
        return new ReplyKeyboardMarkup(new KeyboardButton[][]
        {
                new KeyboardButton[] { new KeyboardButton("⏰ Bugungi namoz vaqtlari") },
                new KeyboardButton[] { new KeyboardButton("⏱️ Eslatma vaqtini o‘zgartirish") }
        })
        {
            ResizeKeyboard = true,
            OneTimeKeyboard = false
        };
    }
}
