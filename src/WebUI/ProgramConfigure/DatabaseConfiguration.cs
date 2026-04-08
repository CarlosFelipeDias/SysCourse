using DAO;

namespace WebUI.Configurations
{
    public static class DatabaseConfiguration
    {
        public static WebApplicationBuilder ConfigureDatabase(this WebApplicationBuilder builder)
        {
            DatabaseSettings.Configure(
                builder.Configuration["DatabaseSettings:DatabaseName"]
                ?? throw new InvalidOperationException("DatabaseSettings:DatabaseName was not configured."));

            DatabaseInitializer.EnsureCreated();

            return builder;
        }
    }
}
