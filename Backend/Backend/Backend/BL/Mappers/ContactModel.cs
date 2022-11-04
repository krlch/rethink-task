using AutoMapper;
using Backend.DAL.Entities;
using Backend.PL.DTO;

namespace Backend.BL.Mappers
{
    public class ContactProfile : Profile
    {
        public ContactProfile()
        {
            CreateMap<ContactDto, Contact>().ReverseMap();
        }
    }

}
