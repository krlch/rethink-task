using Backend.BL.Enums;
using Backend.BL.Services.Interfaces;
using Backend.DAL.Entities;
using Backend.PL.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Backend.PL.Controllers
{
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactsService contactsService; 
        ILogger<ContactController> _logger = null;
        public ContactController(IContactsService contactsService, ILogger<ContactController> logger)
        {
            this.contactsService = contactsService; 
            _logger = logger;
        }

        [HttpGet("api/contacts/")]
        public ActionResult GetAllContacts()
        {
            var result = contactsService.GetAllContacts();
            
            return Ok(result.Data);
        }

        [HttpGet("api/contacts/{id}")]
        public ActionResult<Contact> GetSingleContacts(int id)
        {
            var result = contactsService.GetContact(id);
           
            return result.Data;
        }

        [HttpPost("api/contacts/")]
        public ActionResult CreateContact([FromBody] ContactDto contactDto)
        {
            var result = contactsService.CreateContact(contactDto);
            
            return Created("/api/contacts", result);
        }

        [HttpDelete("api/contacts/{id}")]
        public ActionResult DeleteContact( int id)
        {
            var result = contactsService.DeleteContact(id);
            
            return NoContent();
        }

        [HttpPut("api/contacts")]
        public ActionResult UpdateContact([FromBody] ContactDto contactDto)
        {
            var result = contactsService.UpdateContact(contactDto);
            
            return NoContent();
        }

    }
}
