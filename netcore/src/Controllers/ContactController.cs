using System.Collections.Generic;
using System.Linq;
using Contacts.Exceptions;
using Contacts.Models;
using Contacts.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Contacts.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContactController : ControllerBase
    { 
        private IContactRepository _contactRepository;
        
        public ContactController(IContactRepository repository) 
        {
            _contactRepository = repository;
        }

        [HttpGet("/contacts")]
        public ActionResult<IEnumerable<Contact>> RetrieveAllContacts()
        {
            return _contactRepository.FindAll().ToList();
        }

        [HttpGet("/contacts/{id}")]
        public ActionResult<Contact> RetrieveContactById(long id)
        {
            if (!_contactRepository.ExistsById(id)) 
            {
                throw new ContactNotFoundException($"Unable to find contact with Id: {id}");
            }
            return _contactRepository.FindById(id);
        }

        [HttpPost("/contacts")]
        public  ActionResult<Contact> SaveContact(Contact contact) 
        {
            ValidateContactFields(contact);
            return _contactRepository.Save(contact);
        }

        private void ValidateContactFields(Contact contact) 
        {
            if (string.IsNullOrEmpty(contact.Email)) 
            {
                throw new RequiredFieldException("email");
            }

            if (string.IsNullOrEmpty(contact.Name)) 
            {
                throw new RequiredFieldException("name");
            }

            var existingContact = _contactRepository.FindByEmail(contact.Email);
            if (existingContact != null) 
            {
                throw new DuplicatedContactException($"Unable to create contact due duplicated email: {contact.Email}");
            }
        }
    }
}
