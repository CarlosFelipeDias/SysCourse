using DTO;

namespace DAO.Interfaces;

public interface IPhoneDAO
{
    void CreatePhone(PhoneDTO phone);
    void DeletePhone(int id);
    PhoneDTO? GetPhoneById(int id);
    IEnumerable<PhoneDTO> GetPhonesByContactId(int contactId);
    void UpdatePhone(PhoneDTO phone);
}
