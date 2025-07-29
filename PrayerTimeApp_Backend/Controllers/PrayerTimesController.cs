//ROUTE HANDLER TO FETCH PRAYER TIMES
// This controller handles requests related to prayer times, fetching data from an external API and returning it

using Microsoft.AspNetCore.Mvc;
using System.Text.Json;


[ApiController] //attribute that indicates this class is an API controller
[Route("api/[controller]")] //attribute that defines the route for this controller
public class PrayerTimesController : ControllerBase  // This controller handles requests related to prayer times
{
    private readonly HttpClient _http;

    public PrayerTimesController(IHttpClientFactory httpClientFactory)
    {
        _http = httpClientFactory.CreateClient();
    }

    // Example: GET /api/prayertimes?zone=SGR01&year=2025&month=7
    [HttpGet] //attribute that indicates this method responds to GET requests
    public async Task<IActionResult> Get(string zone = "SGR01", int? year = null, int? month = null)
    {
        try
        {
            // 
            var apiUrl = $"https://api.waktusolat.app/solat/{zone}";
            if (year.HasValue && month.HasValue)
                apiUrl += $"?year={year}&month={month}";

            var response = await _http.GetAsync(apiUrl);

            if (!response.IsSuccessStatusCode)
                return StatusCode((int)response.StatusCode, "Failed to fetch from WaktuSolat API");

            var json = await response.Content.ReadAsStringAsync();
            return Content(json, "application/json");

        }
        catch (KeyNotFoundException ex)
        {
            return StatusCode(500, $"Key not found in JSON: {ex.Message}");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Something went wrong: {ex.Message}");
        }
    }
}
