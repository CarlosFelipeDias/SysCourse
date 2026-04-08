namespace DAO;

public static class DatabaseSettings
{
    private static string? _databaseName;

    public static string DatabaseName =>
        _databaseName ?? throw new InvalidOperationException(
            "Database name was not configured. Call DatabaseSettings.Configure before using DAO classes.");

    public static void Configure(string databaseName)
    {
        if (string.IsNullOrWhiteSpace(databaseName))
        {
            throw new InvalidOperationException("Database name cannot be null or empty.");
        }

        _databaseName = databaseName;
    }
}
