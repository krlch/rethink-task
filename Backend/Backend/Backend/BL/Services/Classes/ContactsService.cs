using AutoMapper;
using Backend.BL.Enums;
using Backend.BL.Services.Interfaces;
using Backend.BL.utils;
using Backend.DAL.Entities;
using Backend.DAL.Repositories.Classes;
using Backend.DAL.Repositories.Interfaces;
using Backend.PL.DTO;
using Backend.PL.Exceptions;
using Backend.PL.Extensions;

namespace Backend.BL.Services.Classes
{
    public class ContactsService : IContactsService
    {
        private readonly IContactRepository _contactRepository; 
        private readonly IMapper _mapper;
        private void checkId (int id)
        {
            if (id < 0 || id > int.MaxValue)
            {
                throw new ApplicationHelperException(ServiceResultType.InvalidData, "Id format error");
            }
        }

        public ContactsService(IMapper mapper, IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
            _mapper = mapper;
        }

        public ServiceResult CreateContact(ContactDto contactDto)
        {
            checkId(contactDto.Id);

            var contact = _mapper.Map<Contact>(contactDto);

            return _contactRepository.CreateItem(contact);
        }

        public ServiceResult DeleteContact(int contactId)
        {
            checkId(contactId);

            return _contactRepository.DeleteItem(contactId);

        }

        public ServiceResult<IEnumerable<Contact>> GetAllContacts()
        {
            return _contactRepository.GetItems();
        }

        public ServiceResult<Contact> GetContact(int contactId)
        {

            if (contactId < 0 || contactId > int.MaxValue)
            {
                throw new ApplicationHelperException(ServiceResultType.InvalidData, "Id format error");
            }

            return _contactRepository.GetItem(contactId);
        }

        public ServiceResult UpdateContact(ContactDto contactDto)
        {
            checkId(contactDto.Id);

            var contact = _mapper.Map<Contact>(contactDto);

            return _contactRepository.UpdateItem(contact.Id, contact);
        }
    }
}
