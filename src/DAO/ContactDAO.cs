using Dapper;
using MySqlConnector;
using DTO;

namespace DAO;




public class ContactDAO : IContactDAO
{
    private const string DatabaseName = "Contact_Book";
    protected string DatabaseIdentifier => $"`{DatabaseName}`";

    private readonly string _serverConnectionString =
        Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection")
        ?? throw new InvalidOperationException(
            "A variavel de ambiente 'ConnectionStrings__DefaultConnection' nao foi configurada.");

    private string DatabaseConnectionString => $"{_serverConnectionString};Database={DatabaseName};";

    private MySqlConnection Connection => new(DatabaseConnectionString);

    public ContactDAO()
    {
        EnsureDatabaseExists();
    }

    public void CreateContact(ContactDTO contact)
    {
        using var connection = Connection;
        connection.Open();
        connection.Execute($@"
                INSERT INTO {DatabaseIdentifier}.`Contacts` (Name, Surname, Email)
                     VALUES (@Name, @Surname, @Email);
            ", contact);
    }

    public IEnumerable<ContactDTO> GetAllContacts()
    {
        using var connection = Connection;
        connection.Open();
        var result = connection.Query<ContactDTO>($@"
                SELECT Id, Name, Surname, Email
                  FROM {DatabaseIdentifier}.`Contacts`;
            ").ToList();
        return result;
    }

    public ContactDTO? GetContactById(int id)
    {
        using var connection = Connection;
        connection.Open();
        var result = connection.QuerySingleOrDefault<ContactDTO>($@"
                SELECT Id, Name, Surname, Email
                  FROM {DatabaseIdentifier}.`Contacts`
                 WHERE Id = @Id;
            ", new { Id = id });
        return result;
    }

    public void UpdateContact(ContactDTO contact)
    {
        using var connection = Connection;
        connection.Open();
        connection.Execute($@"
                UPDATE {DatabaseIdentifier}.`Contacts`
                   SET Name = @Name,
                       Surname = @Surname,
                       Email = @Email
                 WHERE Id = @Id;
            ", contact);
    }

    public void DeleteContact(int id)
    {
        using var connection = Connection;
        connection.Open();
        connection.Execute($@"
                DELETE FROM {DatabaseIdentifier}.`Contacts`
                 WHERE Id = @Id;
            ", new { Id = id });
    }

    private void EnsureDatabaseExists()
    {
        using var connection = new MySqlConnection(_serverConnectionString);
        connection.Open();
        connection.Execute($"CREATE DATABASE IF NOT EXISTS {DatabaseName};");
        CreateContactsTableIfNotExists();
    }

    private void CreateContactsTableIfNotExists()
    {
        using var connection = Connection;
        connection.Open();
        connection.Execute($@"
       CREATE TABLE IF NOT EXISTS {DatabaseIdentifier}.`Contacts` (
                                  `Id` INT AUTO_INCREMENT PRIMARY KEY,
                                `Name` VARCHAR(100) NOT NULL,
                             `Surname` VARCHAR(100) NOT NULL,
                               `Email` VARCHAR(255) NOT NULL
            );
        ");
    }


}
