namespace PrayTimeBot.Helpers;

public static class CityCoordinates
{
    // keys must match what you store (e.g. "Toshkent", "Samarqand", "Buxoro", "ToshkentRegion", etc.)
    private static readonly Dictionary<string, (double Lat, double Lng)> _cities = new(StringComparer.OrdinalIgnoreCase)
    {
        ["Toshkent"] = (41.2995, 69.2401),
        ["ToshkentRegion"] = (40.3936, 69.6487), // example coord for region center
        ["Samarqand"] = (39.6542, 66.9597),
        ["Buxoro"] = (39.768, 64.455),
        ["Andijon"] = (40.7821, 72.3442),
        ["Namangan"] = (40.9983, 71.6726),
        ["Fargona"] = (40.3890, 71.7864),
        ["Navoiy"] = (40.0844, 65.3792),
        ["Qashqadaryo"] = (38.8610, 65.7890),
        ["Surxondaryo"] = (37.8156, 67.2790),
        ["Sirdaryo"] = (40.3716, 68.7811),
        ["Xorazm"] = (41.5553, 60.6316),
        ["Karakalpakstan"] = (42.4600, 59.6100),
        ["Qarshi"] = (38.8606, 65.7890),
        ["Termiz"] = (37.2242, 67.2783),
        ["Urganch"] = (41.5500, 60.6333),
        ["Nukus"] = (42.4600, 59.6100)
    };

    public static (double Lat, double Lng)? Get(string? cityKey)
    {
        if (string.IsNullOrWhiteSpace(cityKey)) return null;

        // Normalize callback keys like "ToshkentRegion" or "Toshkent"
        var key = cityKey.Replace(" ", "").Replace("’", "'").Replace("‘", "'");

        return _cities.TryGetValue(key, out var coord) ? coord : null;
    }
}
