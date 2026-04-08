using DAO;
using WebUI.Configurations;

var builder = WebApplication.CreateBuilder(args);

var useStaticWebAssets = builder.Configuration.GetValue<bool>("ApplicationBehavior:UseStaticWebAssets");
var useExceptionHandler = builder.Configuration.GetValue<bool>("ApplicationBehavior:UseExceptionHandler");
var useHsts = builder.Configuration.GetValue<bool>("ApplicationBehavior:UseHsts");
var useHttpsRedirection = builder.Configuration.GetValue<bool>("ApplicationBehavior:UseHttpsRedirection");

if (useStaticWebAssets)
{
    builder.WebHost.UseStaticWebAssets();
}

builder.ConfigureDatabase();

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.ConfigureAutoMapper();
builder.Services.ConfigureDependencyInjectionServices(builder.Environment);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (useExceptionHandler)
{
    app.UseExceptionHandler("/Home/Error");
}

if (useHsts)
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

if (useHttpsRedirection)
{
    app.UseHttpsRedirection();
}

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
