using ContactBook.Data;
using ContactBook.DTO;
using ContactBook.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.Reflection.Metadata.Ecma335;

namespace ContactBook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactService _contactService;

        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }


        // To Add
        [HttpPost("add")]
        public IActionResult AddNewContact([FromBody] AddContactDto model)
        {
            if (ModelState.IsValid)
            {
                var contactToAdd = new Contact
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    Address = model.Address,
                    PhoneNumber = model.PhoneNumber,
                };

                if (_contactService.AddContact(contactToAdd))
                    return Ok("Contact added successfully");
                return BadRequest("Failed to add Contact");
            }

            return BadRequest(ModelState);
        }


        // To Get All
        [HttpGet("get-all")]
        public IActionResult GetAllContacts()
        {
            var contacts = _contactService.GetAllContacts();
            return Ok(contacts);
        }


        [HttpGet("single/{id}")]
        public IActionResult GetContact(int id)
        {
            var contact = _contactService.GetContact(id);

            if (contact != null)
            {
                var result = new ReturnContactDto
                {
                    FirstName = contact.FirstName,
                    LastName = contact.LastName,
                    PhoneNumber = contact.PhoneNumber,
                    Id = contact.Id
                };

                return Ok(result);
            }

            return NotFound($"Could not find contact for id {id}");

        }

        [HttpPut("update/{id}")]
        public IActionResult UpdateContact(int id, [FromBody] UpdateContactDto model)
        {
            var contact = _contactService.GetContact(id);

            if (contact != null)
            {
                contact.FirstName = model.FirstName;
                contact.LastName = model.LastName;
                contact.PhoneNumber = model.PhoneNumber;
                contact.Email = model.Email;
                contact.Address = model.Address;

                if (_contactService.UpdateContact(contact))
                {
                    return Ok("updated successfully");
                }
                return BadRequest("update failed");
            }
            return BadRequest($"update failed:could not get contact from id:{id}");


        }

        [HttpDelete("Delete/{id}")]
        public IActionResult DeleteContact(int id)
        {
            var contact = _contactService.GetContact(id);

            if (contact != null)
            {
                if (_contactService.DeleteContact(contact))
                {
                    return Ok("delete successfully");
                }
                return BadRequest("deleted failed");
            }
            return BadRequest($"delete failed:could not get contact from id:{id}");


        }





    }
}
       
           