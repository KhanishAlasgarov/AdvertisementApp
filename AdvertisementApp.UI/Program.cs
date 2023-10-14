using AdvertisementApp.Application.DependencyResolvers.Microsoft;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddDependencies(builder.Configuration);

var app = builder.Build();
app.UseStaticFiles();
app.MapDefaultControllerRoute();

app.Run();
