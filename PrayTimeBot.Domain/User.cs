using System.ComponentModel.DataAnnotations;

namespace PrayTimeBot.Domain;

public class User
{
    [Key]
    public int Id { get; set; }                  // Primary key
    public long ChatId { get; set; }             // Telegram chat ID
    public string? Username { get; set; }        // @username
    public string? FirstName { get; set; }       // User’s first name
    public string? City { get; set; }            // For prayer or timezone
    public string? LanguageCode { get; set; }    // e.g. "uz", "ru", "en"
    public bool SendReminders { get; set; }      // Namoz or daily reminder setting
    public int Format { get; set; } = 1;         // Time format
    public int? ReminderHour { get; set; }       // ⏱️ Reminder hour (0–23)
    public DateTime CreatedAt { get; set; }      // When user joined
}
