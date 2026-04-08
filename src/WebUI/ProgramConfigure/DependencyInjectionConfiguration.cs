using DAO;
using DAO.Interfaces;
using Microsoft.AspNetCore.Hosting;

namespace WebUI.Configurations
{
    public static class DependencyInjectionConfiguration
    {
        public static IServiceCollection ConfigureDependencyInjectionServices(this IServiceCollection services, IWebHostEnvironment environment)
        {
            if (environment.IsDevelopment())
            {
                services.AddScoped<IContactDAO, ContactDAO>();
                services.AddScoped<IPhoneDAO, PhoneDAO>();
                return services;
            }

            if (environment.IsStaging())
            {
                services.AddScoped<IContactDAO, ContactDAO>();
                services.AddScoped<IPhoneDAO, PhoneDAO>();
                return services;
            }

            services.AddScoped<IContactDAO, ContactDAO>();
            services.AddScoped<IPhoneDAO, PhoneDAO>();
            return services;
        }
    }
}
