using DAO;
using DAO.Interfaces;

namespace WebUI.Configurations
{
    public static class DependencyInjectionConfiguration
    {
        public static IServiceCollection ConfigureDependencyInjectionServices(this IServiceCollection services)
        {
            services.AddScoped<IContactDAO, ContactDAO>();
            services.AddScoped<IPhoneDAO, PhoneDAO>();
            return services;
        }
    }
}
