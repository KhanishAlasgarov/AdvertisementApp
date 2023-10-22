using AdvertisementApp.Application.DependencyResolvers.Microsoft;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(opt =>
    {
        opt.Cookie.Name = "AdvertisementApp";
        opt.Cookie.HttpOnly = true;
        opt.Cookie.SameSite = SameSiteMode.Strict;
        opt.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
        opt.ExpireTimeSpan = TimeSpan.FromDays(30);
        opt.LoginPath = new PathString("/Account/SignIn");
        opt.LogoutPath = new PathString("/Account/LogOut");
        opt.AccessDeniedPath = new PathString("/Account/AccessDenied");

    });


builder.Services.AddDependencies(builder.Configuration);

var app = builder.Build();
app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

app.MapDefaultControllerRoute();

app.Run();
