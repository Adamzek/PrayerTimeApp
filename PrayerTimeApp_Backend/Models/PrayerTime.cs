// THE MODEL FOR PRAYER TIMES
// This file defines the structure of the prayer times data that will be used in the application.
public class PrayerTime  //JSON structure to deserialize the prayer times from the prayer times API
{
    public string? Fajr { get; set; }
    public string? Syuruk { get; set; }
    public string? Dhuhr { get; set; }
    public string? Asr { get; set; }
    public string? Maghrib { get; set; }
    public string? Isha { get; set; }
}

public class PrayerTimeResponse //JSON structure to serialize the response from the prayer times API
{
    public string? Zone { get; set; }
    public string? Date { get; set; }
    public PrayerTime? PrayerTime { get; set; }
}