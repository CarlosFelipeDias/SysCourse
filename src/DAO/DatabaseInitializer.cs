using System;
using Dapper;
using MySqlConnector;

namespace DAO;

public static class DatabaseInitializer
{
    private const string DatabaseName = "Contact_Book";
    private static string DatabaseIdentifier => $"`{DatabaseName}`";

    public static void EnsureCreated()
    {
        var serverConnectionString =
            Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection")
            ?? throw new InvalidOperationException(
                "A variavel de ambiente 'ConnectionStrings__DefaultConnection' nao foi configurada.");

        var databaseConnectionString = $"{serverConnectionString};Database={DatabaseName};";

        using var serverConnection = new MySqlConnection(serverConnectionString);
        serverConnection.Open();
        serverConnection.Execute($"CREATE DATABASE IF NOT EXISTS {DatabaseIdentifier};");

        using var databaseConnection = new MySqlConnection(databaseConnectionString);
        databaseConnection.Open();
        databaseConnection.Execute($@"
            CREATE TABLE IF NOT EXISTS {DatabaseIdentifier}.`Contacts` (
                `Id` INT AUTO_INCREMENT PRIMARY KEY,
                `Name` VARCHAR(100) NOT NULL,
                `Surname` VARCHAR(100) NOT NULL,
                `Email` VARCHAR(255) NOT NULL
            );
        ");

        databaseConnection.Execute($@"
            CREATE TABLE IF NOT EXISTS {DatabaseIdentifier}.`Phones` (
                `Id` INT AUTO_INCREMENT PRIMARY KEY,
                `ContactId` INT NOT NULL,
                `PhoneNumber` VARCHAR(20) NOT NULL,
                CONSTRAINT `FK_Phones_Contacts`
                    FOREIGN KEY (`ContactId`) REFERENCES {DatabaseIdentifier}.`Contacts`(`Id`)
                    ON DELETE CASCADE
            );
        ");
    }
}
