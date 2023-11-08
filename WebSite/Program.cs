using BlazorIdentity;
using BlazorStrap;
using Elfie.Serialization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RoslynLibrary.Extensions;
using RoslynLibrary.Sections;
using Syncfusion.Blazor;
using System;
using System.Configuration;
using WebSite.Data;
using WebSite.Middleware;
using WebSite.Models;
using WebSite.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();

builder.Services.AddDbContext<DataBaseContextService>(options =>
        options.UseNpgsql(
                    builder.Configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(DataBaseContextService).Assembly.FullName)
                )
);

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddBlazorServerIdentity<IdentityUser, IdentityRole>(options => 
{
    options.SignIn.RequireConfirmedAccount = false;
    options.SignIn.RequireConfirmedPhoneNumber = false;
    options.SignIn.RequireConfirmedEmail = false;
    options.Password.RequiredUniqueChars = 0;
    options.Password.RequiredLength = 2;
    options.Password.RequireDigit = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
})
.AddEntityFrameworkStores<DataBaseContextService>()
.AddDefaultTokenProviders();

builder.Services.Configure<SecurityStampValidatorOptions>(options =>
{
    // enables immediate logout, after updating the user's security stamp.
    options.ValidationInterval = TimeSpan.Zero;
});
builder.Services.AddServerSideBlazor();
builder.Services.AddSyncfusionBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddScoped<ChatGptService>();
builder.Services.AddScoped<ResourceAnalyzeService>();
builder.Services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();

builder.Services.AddBlazorStrap();
builder.Services.AddPluginAnalyzer();

builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
builder.Services.AddControllersWithViews()
    .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix) 
    .AddDataAnnotationsLocalization();

builder.Services.Configure<ManagedSection>(builder.Configuration.GetSection("ManagedSection"));

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

app.UseAuthentication();
app.UseAuthorization();

app.MapBlazorServerIdentityApi<IdentityUser>();

app.UseMiddleware<CheckUserMiddleware>();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.UseMiddleware<WebSiteSettingMiddleware>();


app.MapControllers();

app.Run();