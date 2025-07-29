// THE SETUP
using Microsoft.OpenApi.Models; // This namespace provides classes for setting up Swagger documentation for your API

var builder = WebApplication.CreateBuilder(args);  //Think of it like gathering all the tools and materials before building a house.

//DI CONTAINER = TOOLBOX
//SERVICES = TOOLS 

//builder.Services.AddControllers();  // ← this tells .NET to look for controller classes
builder.Services.AddEndpointsApiExplorer(); // defines the API endpoints and their metadata, making it easier to explore and document the API.
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "PrayerTime API", Description = "Display prayer time", Version = "v1" });
});

builder.Services.AddHttpClient(); // register HttpClient
//builder.Services.AddHttpClient("InsecureHttp")  // Required to make HTTP requests
    //.ConfigurePrimaryHttpMessageHandler(() =>
    //{
    //    return new HttpClientHandler
    //    {
    //        ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
    //    };
    //});
//builder.Services.AddHttpClient<PrayerTimeService>(); // Provides the PrayerTimeService with an HttpClient instance, which is used to make HTTP requests.

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        policy => policy.WithOrigins("http://localhost:3000")
                        .AllowAnyMethod()
                        .AllowAnyHeader());
}); 

var app = builder.Build();  // actually builds the app using the tools prepared earlier.

app.UseCors("AllowReactApp");
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "PrayerTime API V1");
    });
}

//ENDPOINTS = ROUTES = PATH = MIDDLEWARE 

//app.MapControllers(); // ← this maps your PrayerTimesController to the routing system

app.MapGet("/", () => "Hello World!"); //It's like setting a rule: "If someone knocks on the front door, say Hello!"

// Example: GET /api/prayertimes?zone=SGR01&year=2025&month=7
app.MapGet("/api/prayertimes", async (HttpClient http, string zone = "SGR01", int? year = null, int? month = null) => {
    try
    {
        var apiUrl = $"https://api.waktusolat.app/solat/{zone}";
        if (year.HasValue && month.HasValue)
            apiUrl += $"?year={year}&month={month}";

        var response = await http.GetAsync(apiUrl);

        if (!response.IsSuccessStatusCode)
            return Results.StatusCode((int)response.StatusCode);

        var json = await response.Content.ReadAsStringAsync();
        return Results.Content(json, "application/json");
    }
    catch (KeyNotFoundException ex) {
        return Results.Problem($"key not found in JSON: {ex.Message}", statusCode: 500);
    }
    catch (Exception ex)
    {
        return Results.Problem($"Something went wrong: {ex.Message}", statusCode: 500);
    }
});

//START THE SERVER = LISTEN FOR REQUESTS = OPEN THE DOOR FOR VISITORS
app.Run();

