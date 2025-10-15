using PrayTimeBot.Domain;

namespace PrayTimeBot.Helpers;

public static class PrayerTimeFormatter
{
    public static string ClassicFormat(PrayerTime t)
    {
        string Trim(string time) => time.Length > 5 ? time[..5] : time;

        return $"""
            📅 *{t.City} — {t.Date:dd MMMM yyyy}*

            🌅 Bomdod:      {Trim(t.Fajr.ToString())}
            ☀️ Quyosh:       {Trim(t.Sunrise.ToString())}
            🕛 Peshin:        {Trim(t.Dhuhr.ToString())}
            🌇 Asr:              {Trim(t.Asr.ToString())}
            🌃 Shom:         {Trim(t.Maghrib.ToString())}
            🌙 Xufton:       {Trim(t.Isha.ToString())}
            """;
    }

    public static string ElegantFormat(PrayerTime t)
    {
        string Trim(string time) => time.Length > 5 ? time[..5] : time;

        return $"""
            🌸 *{t.City}* — 🗓 {t.Date:dd MMM yyyy}

            🕌 *Namoz vaqtlari:*
            🌅 Bomdod:      {Trim(t.Fajr.ToString())}
            ☀️ Quyosh:       {Trim(t.Sunrise.ToString())}
            🕛 Peshin:        {Trim(t.Dhuhr.ToString())}
            🌇 Asr:              {Trim(t.Asr.ToString())}
            🌃 Shom:         {Trim(t.Maghrib.ToString())}
            🌙 Xufton:       {Trim(t.Isha.ToString())}

            🤲 *Alloh namozimizni qabul qilsin!*
            """;
    }

    public static string DuaFormat(PrayerTime t)
    {
        string Trim(string time) => time.Length > 5 ? time[..5] : time;

        return $"""
            🕋 *{t.City}* — {t.Date:dd MMMM}

            🌅 Bomdod:      {Trim(t.Fajr.ToString())}
            ☀️ Quyosh:       {Trim(t.Sunrise.ToString())}
            🕛 Peshin:        {Trim(t.Dhuhr.ToString())}
            🌇 Asr:              {Trim(t.Asr.ToString())}
            🌃 Shom:         {Trim(t.Maghrib.ToString())}
            🌙 Xufton:       {Trim(t.Isha.ToString())}

            💫 *اللهم اجعلنا من المحافظين على الصلاة*  
            (Allohim, bizni namozni ado etuvchilar qatoriga qo‘shgin.)
            """;
    }


    public static string MinimalFormat(PrayerTime t)
    {
        string Trim(string time) => time.Length > 5 ? time[..5] : time;

        return $"""
            🗓 {t.Date:dd.MM.yyyy} | 📍 {t.City}

            Bomdod:      {Trim(t.Fajr.ToString())}
            Quyosh:       {Trim(t.Sunrise.ToString())}
            Peshin:        {Trim(t.Dhuhr.ToString())}
            Asr:              {Trim(t.Asr.ToString())}
            Shom:         {Trim(t.Maghrib.ToString())}
            Xufton:       {Trim(t.Isha.ToString())}
            """;
    }

    public static string DecorativeFormat(PrayerTime t)
    {
        string Trim(string time) => time.Length > 5 ? time[..5] : time;

        return $"""
            ╔══❀•°🕌°•❀══╗
            🌙 *{t.City}* — {t.Date:dd MMMM yyyy}
            ╚══❀•°•❀══╝

            🌅 Bomdod:      {Trim(t.Fajr.ToString())}
            ☀️ Quyosh:       {Trim(t.Sunrise.ToString())}
            🕛 Peshin:        {Trim(t.Dhuhr.ToString())}
            🌇 Asr:              {Trim(t.Asr.ToString())}
            🌃 Shom:         {Trim(t.Maghrib.ToString())}
            🌙 Xufton:       {Trim(t.Isha.ToString())}

            💖 *Alloh sizni va oilangizni asrasin!* 🤲
            """;
    }


    //public static string Format(PrayerTime t)
    //{
    //    string Trim(string time) => time.Length > 5 ? time[..5] : time;

    //    return $"""
    //            📅 *{t.City} — {t.Date:dd MMMM yyyy}*

    //            🌅 Bomdod:  {Trim(t.Fajr.ToString())} 
    //            ☀️ Quyosh:   {Trim(t.Sunrise.ToString())} 
    //            🕛 Peshin:     {Trim(t.Dhuhr.ToString())} 
    //            🌇 Asr:           {Trim(t.Asr.ToString())} 
    //            🌃 Shom:      {Trim(t.Maghrib.ToString())} 
    //            🌙 Xufton:     {Trim(t.Isha.ToString())} 
    //            """;
    //}
}