
using Blazored.LocalStorage;
using EventManager;
using EventManager.Web.Services.Implementation;
using EventManager.Web.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddServerComponents();

builder.Services.AddHttpClient<IAccount, Account>(client => { client.BaseAddress = new Uri("https://localhost:7117/"); });
builder.Services.AddHttpClient<IEventPlannerOrganization, EventPlannerOrganization>(client => { client.BaseAddress = new Uri("https://localhost:7117/"); });

builder.Services.AddBlazoredLocalStorage();
//builder.Services.AddSingleton<WeatherForecastService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.MapRazorComponents<App>();

app.Run();
