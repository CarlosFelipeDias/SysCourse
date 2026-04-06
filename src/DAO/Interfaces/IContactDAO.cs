using DTO;

namespace DAO.Interfaces;
public interface IContactDAO
{
    void CreateContact(ContactDTO contact);
    void DeleteContact(int id);
    IEnumerable<ContactDTO> GetAllContacts();
    ContactDTO? GetContactById(int id);
    void UpdateContact(ContactDTO contact);
}