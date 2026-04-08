using System;
using Dapper;
using MySqlConnector;

namespace DAO;

public static class DatabaseInitializer
{
    public static void EnsureCreated()
    {
        var databaseName = DatabaseSettings.DatabaseName;
        var databaseIdentifier = $"`{databaseName}`";

        var serverConnectionString =
            Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection")
            ?? throw new InvalidOperationException(
                "A variavel de ambiente 'ConnectionStrings__DefaultConnection' nao foi configurada.");

        var databaseConnectionString = $"{serverConnectionString};Database={databaseName};";

        using var serverConnection = new MySqlConnection(serverConnectionString);
        serverConnection.Open();
        serverConnection.Execute($"CREATE DATABASE IF NOT EXISTS {databaseIdentifier};");

        using var databaseConnection = new MySqlConnection(databaseConnectionString);
        databaseConnection.Open();
        databaseConnection.Execute($@"
            CREATE TABLE IF NOT EXISTS {databaseIdentifier}.`Contacts` (
                `Id` INT AUTO_INCREMENT PRIMARY KEY,
                `Name` VARCHAR(100) NOT NULL,
                `Surname` VARCHAR(100) NOT NULL,
                `Email` VARCHAR(255) NOT NULL
            );
        ");

        databaseConnection.Execute($@"
            CREATE TABLE IF NOT EXISTS {databaseIdentifier}.`Phones` (
                `Id` INT AUTO_INCREMENT PRIMARY KEY,
                `ContactId` INT NOT NULL,
                `PhoneNumber` VARCHAR(20) NOT NULL,
                CONSTRAINT `FK_Phones_Contacts`
                    FOREIGN KEY (`ContactId`) REFERENCES {databaseIdentifier}.`Contacts`(`Id`)
                    ON DELETE CASCADE
            );
        ");
    }
}
