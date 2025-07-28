// 

using System.Net.Http; //class for making HTTP requests
using System.Threading.Tasks; // used for asynchronous programming
using System.Text.Json; // This namespace is used for JSON serialization and deserialization

public class PrayerTimeService
{
    private readonly HttpClient _httpClient;

    public PrayerTimeService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<PrayerTime> GetPrayerTimesByZoneAsync(string? zone) // This method fetches prayer times for a specific zone code
    {
        if (string.IsNullOrWhiteSpace(zone))
            throw new ArgumentException("Zone code cannot be null or empty.", nameof(zone));

        var data = await _httpClient.GetFromJsonAsync<PrayerTimeResponse>($"https://api.waktusolat.app/v2/solat/{zone}"); //HTTP GET request to prayer times API

        // var response = await _httpClient.GetAsync($"https://api.waktusolat.app/v2/solat/{zone}"); //HTTP GET request to prayer times API
        // response.EnsureSuccessStatusCode();

        // var content = await response.Content.ReadAsStringAsync();     //store the response content as a string

        // var options = new JsonSerializerOptions
        // {
        //     PropertyNameCaseInsensitive = true
        // };

        // var data = JsonSerializer.Deserialize<PrayerTimeResponse>(content, options);

        if (data?.PrayerTime == null)
            throw new Exception("prayer times not found for the specified zone.");

        return data.PrayerTime;
    }
}
