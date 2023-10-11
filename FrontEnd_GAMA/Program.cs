global using Microsoft.AspNetCore.Components.Authorization;
global using Blazored.LocalStorage;

using FrontEnd_GAMA;
using FrontEnd_GAMA.Data;
using FrontEnd_GAMA.Singleton;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Blazored.Modal;
using Blazored.Toast;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<HttpClient>(sp => new HttpClient {BaseAddress = new Uri("http://localhost:5042/api/") });
builder.Services.AddBlazoredModal();
builder.Services.AddSingleton<AdmOperationEntity>();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddScoped<AuthenticationStateProvider, AuthProvider>();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddAuthenticationCore();
builder.Services.AddBlazoredToast();


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

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
