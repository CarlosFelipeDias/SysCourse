using AutoMapper;
using DAO;
using DAO.Interfaces;
using DTO;
using Microsoft.Extensions.Logging.Abstractions;
using WebUI.Models;

var builder = WebApplication.CreateBuilder(args);

DatabaseInitializer.EnsureCreated();

// Add services to the container.
builder.Services.AddControllersWithViews();

var config = new MapperConfiguration(cfg =>
{
    cfg.CreateMap<ContactDTO, ContactViewModel>().ReverseMap();
    cfg.CreateMap<PhoneDTO, PhoneViewModel>().ReverseMap();
}, NullLoggerFactory.Instance);

IMapper mapper = config.CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddTransient<IContactDAO, ContactDAO>();
builder.Services.AddTransient<IPhoneDAO, PhoneDAO>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
