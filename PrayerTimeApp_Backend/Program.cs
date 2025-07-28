// THE SETUP
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);  //Think of it like gathering all the tools and materials before building a house.

// builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "PrayerTime API", Description = "Display prayer time", Version = "v1" });
});
builder.Services.AddHttpClient("InsecureHttp")  // Required to make HTTP requests
    .ConfigurePrimaryHttpMessageHandler(() =>
    {
        return new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
        };
    });
builder.Services.AddControllers();  // ← this tells .NET to look for controller classes
builder.Services.AddHttpClient<PrayerTimeService>(); // Provides the PrayerTimeService with an HttpClient instance, which is used to make HTTP requests.
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

app.MapControllers(); // ← this maps your PrayerTimesController to the routing system
app.MapGet("/", () => "Hello World!"); //It's like setting a rule: "If someone knocks on the front door, say Hello!"

app.Run(); //This starts the web server and begins listening for requests (like when someone visits your site).Like opening your house for visitors.


