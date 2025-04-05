using Microsoft.EntityFrameworkCore;
using WEATHER.API.BackgroundServices;
using WEATHER.API.Configuration;
using WEATHER.API.Data;
using WEATHER.API.Services.Contracts;
using WEATHER.API.Services.Implementation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

// Add services to the container.could be in separate extension methods for separation
builder.Services.AddTransient<IWeatherService, WeatherService>();
builder.Services.AddTransient<IBackgroundWeatherJobService, BackgroundWeatherJobService>();
builder.Services.AddHttpClient();
builder.Services.AddAutoMapper(typeof(WeatherMapperProfile));

builder.Services.AddHostedService<ScheduledWeatherFetcherBackgroundService>();


builder.Services.AddDbContext<WeatherDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("WeatherDB")));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});



var app = builder.Build();

app.UseCors("AllowAll");
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
