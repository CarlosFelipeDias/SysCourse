using Dapper;
using DAO.Interfaces;
using DTO;

namespace DAO;

public class PhoneDAO : BaseDAO, IPhoneDAO
{
    public void CreatePhone(PhoneDTO phone)
    {
        using var connection = Connection;
        connection.Open();
        connection.Execute($@"
                INSERT INTO {DatabaseIdentifier}.`Phones` (ContactId, PhoneNumber)
                     VALUES (@ContactId, @PhoneNumber);
            ", phone);
    }

    public IEnumerable<PhoneDTO> GetPhonesByContactId(int contactId)
    {
        using var connection = Connection;
        connection.Open();
        var result = connection.Query<PhoneDTO>($@"
                SELECT Id, ContactId, PhoneNumber
                  FROM {DatabaseIdentifier}.`Phones`
                 WHERE ContactId = @ContactId;
            ", new { ContactId = contactId }).ToList();
        return result;
    }

    public PhoneDTO? GetPhoneById(int id)
    {
        using var connection = Connection;
        connection.Open();
        var result = connection.QuerySingleOrDefault<PhoneDTO>($@"
                SELECT Id, ContactId, PhoneNumber
                  FROM {DatabaseIdentifier}.`Phones`
                 WHERE Id = @Id;
            ", new { Id = id });
        return result;
    }

    public void UpdatePhone(PhoneDTO phone)
    {
        using var connection = Connection;
        connection.Open();
        connection.Execute($@"
                UPDATE {DatabaseIdentifier}.`Phones`
                   SET ContactId = @ContactId,
                       PhoneNumber = @PhoneNumber
                 WHERE Id = @Id;
            ", phone);
    }

    public void DeletePhone(int id)
    {
        using var connection = Connection;
        connection.Open();
        connection.Execute($@"
                DELETE FROM {DatabaseIdentifier}.`Phones`
                 WHERE Id = @Id;
            ", new { Id = id });
    }
}
