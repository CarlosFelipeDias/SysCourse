using Dapper;
using MySqlConnector;
using DTO;
using DAO.Interfaces;

namespace DAO;




public class ContactDAO : BaseDAO, IContactDAO
{ 



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

   


}
