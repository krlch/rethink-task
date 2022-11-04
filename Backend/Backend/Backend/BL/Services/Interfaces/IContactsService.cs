using Backend.BL.utils;
using Backend.DAL.Entities;
using Backend.PL.DTO;

namespace Backend.BL.Services.Interfaces
{
    public interface IContactsService
    {
        ServiceResult CreateContact(ContactDto contactDto);
        ServiceResult DeleteContact(int contactId);
        ServiceResult UpdateContact(ContactDto contactId);
        ServiceResult<Contact> GetContact(int contactId);
        ServiceResult<IEnumerable<Contact>> GetAllContacts();
    }
}