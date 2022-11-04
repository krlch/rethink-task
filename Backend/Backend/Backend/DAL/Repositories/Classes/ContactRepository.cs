using Backend.BL.Enums;
using Backend.BL.Services.Interfaces;
using Backend.BL.utils;
using Backend.DAL.Entities;
using Backend.DAL.Repositories.Interfaces;
using Backend.PL.Exceptions;
using Backend.PL.Extensions;
using System.Linq;

namespace Backend.DAL.Repositories.Classes
{
    public class ContactRepository : BaseRepository, IContactRepository
    {
        ILogger<ContactRepository> _logger;

        public ContactRepository(ILogger<ContactRepository> logger)
        {
            _logger = logger;
        }

        public ServiceResult<Contact> GetItem(int id)
        {
            var items = GetAllObjects();
            

            var contacts = items.Data;
            var resultContact = contacts.SingleOrDefault(it=> it.Id == id);

            if (resultContact is null)
            {
                throw new ApplicationHelperException(ServiceResultType.NotFound, "Contact not found");
            }
            _logger.LogInformation("Get item");
            return new(ServiceResultType.Ok, resultContact); 
        }
        public ServiceResult<IEnumerable<Contact>> GetItems()
        {
            var items = GetAllObjects();

            var contacts = items.Data;
            _logger.LogInformation("Get items");

            return new(ServiceResultType.Ok, contacts);
        }

        public ServiceResult CreateItem(Contact contact)
        {
            var items = GetAllObjects();

            var lastId = items.Data.Any() ? items.Data.Max(con => con.Id) : 0;
            
            var contactList = items.Data;

            contact.Id = ++lastId;
            contactList.Add(contact);

            SaveDatabase(contactList);
            _logger.LogInformation("Create item");

            return new(ServiceResultType.Ok);

        }

        public ServiceResult UpdateItem(int id, Contact contact)
        {
            var items = GetAllObjects();

            var foundContact = items.Data.SingleOrDefault(cont => cont.Id == id);

            if(foundContact is null)
            {
                throw new ApplicationHelperException(ServiceResultType.NotFound, "Contact not found");
            }

            items.Data[items.Data.IndexOf(foundContact)] = contact;  // ignoring error handling

            SaveDatabase(items.Data);
            _logger.LogInformation("Update item");

            return new(ServiceResultType.Ok);
        }

        public ServiceResult DeleteItem(int id)
        {
            var items = GetAllObjects();
           

            var foundContact = items.Data.SingleOrDefault(cont => cont.Id == id);

            if (foundContact is null)
            {
                throw new ApplicationHelperException(ServiceResultType.NotFound, "Contact not found");
            }

            items.Data.Remove(foundContact);

            SaveDatabase(items.Data);

            _logger.LogInformation("Delete item");
            return new(ServiceResultType.Ok);
        }
    }
}
