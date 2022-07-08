var uiFolder = WebApplication.CreateBuilder(args).Configuration.GetValue<string>("UiFolder");
var builder = WebApplication.CreateBuilder(new WebApplicationOptions()
{
    WebRootPath = uiFolder
});

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var uiUrl = builder.Configuration.GetValue<string>("UiUrl");

string UiCorsPolicy = "UiApiCalls";
if (uiUrl != null)
{
    builder.Services.AddCors(options =>
    {
        options.AddPolicy(name: UiCorsPolicy,
            policy  =>
            {
                policy.AllowAnyHeader()
                    .AllowAnyMethod()
                    .WithOrigins(builder.Configuration.GetValue<string>("UiUrl"))
                    .AllowCredentials();
            });
    });
}

var app = builder.Build();

if (uiUrl != null)
{
    app.UseCors(UiCorsPolicy);    
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
if (uiUrl == null)
{
    app.UseDefaultFiles();
    app.UseStaticFiles();
}
var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/api/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateTime.Now.AddDays(index),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

app.Run();

record WeatherForecast(DateTime Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}