namespace PrayTimeBot.Configuraitons;

public class BotSettings
{
    public string Token { get; set; } = "";
    public string DefaultRegion { get; set; } = "Toshkent";
    public TimeSpan DefaultTime { get; set; } = new(4, 0, 0); // 4:00 AM
}
