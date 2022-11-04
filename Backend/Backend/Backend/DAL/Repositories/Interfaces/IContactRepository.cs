using Backend.BL.utils;
using Backend.DAL.Entities;

namespace Backend.DAL.Repositories.Interfaces
{
    public interface IContactRepository
    {
        ServiceResult<Contact> GetItem(int id);

        ServiceResult<IEnumerable<Contact>> GetItems();


        ServiceResult CreateItem(Contact contact);


        ServiceResult UpdateItem(int id, Contact contact);


        ServiceResult DeleteItem(int id);

    }
}
