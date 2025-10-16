using System.ComponentModel.DataAnnotations;

namespace PrayTimeBot.Domain;

public class PrayerTime
{
    [Key]
    public int Id { get; set; }
    public string City { get; set; } = "";
    public string Country { get; set; } = "";
    public DateTime Date { get; set; }
    public string? Method { get; set; } 
    public TimeSpan Fajr { get; set; }
    public TimeSpan Sunrise { get; set; }
    public TimeSpan Dhuhr { get; set; }
    public TimeSpan Asr { get; set; }
    public TimeSpan Maghrib { get; set; }
    public TimeSpan Isha { get; set; }
}
