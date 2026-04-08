using MySqlConnector;

namespace DAO
{
    public class BaseDAO
    {
        protected string DatabaseName => DatabaseEnvironment.GetDatabaseName();
        protected string DatabaseIdentifier => $"`{DatabaseName}`";

        protected readonly string _serverConnectionString =
            Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection")
            ?? throw new InvalidOperationException(
                "A variavel de ambiente 'ConnectionStrings__DefaultConnection' nao foi configurada.");

        protected string DatabaseConnectionString => $"{_serverConnectionString};Database={DatabaseName};";

        protected MySqlConnection Connection => new(DatabaseConnectionString);
    }
}
