using System.Net.Http.Json;
using PrayTimeBot.Domain;

namespace PrayTimeBot.Services;

public class PrayerTimeService
{
    private readonly HttpClient _http = new();

    // ✅ Gets today’s prayer times from islomapi.uz
    public async Task<PrayerTime?> GetTodayAsync(string city)
    {
        var url = $"https://islomapi.uz/api/present/day?region={city}";

        try
        {
            var response = await _http.GetFromJsonAsync<IslomApiResponse>(url);
            if (response == null || response.times == null)
                return null;

            return new PrayerTime
            {
                City = response.region,
                Country = "Uzbekistan",
                Date = DateTime.Now,
                Method = "islomapi.uz",
                Fajr = TimeSpan.Parse(response.times.tong_saharlik),
                Sunrise = TimeSpan.Parse(response.times.quyosh),
                Dhuhr = TimeSpan.Parse(response.times.peshin),
                Asr = TimeSpan.Parse(response.times.asr),
                Maghrib = TimeSpan.Parse(response.times.shom_iftor),
                Isha = TimeSpan.Parse(response.times.hufton)
            };
        }
        catch
        {
            return null;
        }
    }
}

// === Response Models for islomapi.uz ===
public class IslomApiResponse
{
    public string region { get; set; } = "";
    public Times times { get; set; } = new();
}

public class Times
{
    public string tong_saharlik { get; set; } = "";
    public string quyosh { get; set; } = "";
    public string peshin { get; set; } = "";
    public string asr { get; set; } = "";
    public string shom_iftor { get; set; } = "";
    public string hufton { get; set; } = "";
}
