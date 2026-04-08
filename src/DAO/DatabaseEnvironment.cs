namespace DAO;

public static class DatabaseEnvironment
{
    public static string GetDatabaseName()
    {
        var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        return environmentName switch
        {
            "Development" => "Contact_Book_Dev",
            "Staging" => "Contact_Book_Stg",
            _ => "Contact_Book"
        };
    }
}
