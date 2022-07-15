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
    public class MessageController : ControllerBase
    { 
        private IMessageRepository _messageRepository;
        
        public MessageController(IMessageRepository repository) 
        {
            _messageRepository = repository;
        }

        [HttpGet("/messages")]
        public ActionResult<IEnumerable<Message>> RetrieveAllMessages()
        {
            return _messageRepository.FindAll().ToList();
        }

        [HttpPost("/messages")]
        public  ActionResult<Message> SaveContact(Message message)
        {
            ValidateContactFields(message);
            return _messageRepository.Save(message);
        }

        private void ValidateContactFields(Message message)
        {
            if (string.IsNullOrEmpty(message.Content)) 
            {
                throw new RequiredFieldException("Content of the message is required.");
            }

            if (!message.Contacts.Any())
            {
                throw new RequiredFieldException("At least one contact should be added to the message");
            }
        }
    }
}