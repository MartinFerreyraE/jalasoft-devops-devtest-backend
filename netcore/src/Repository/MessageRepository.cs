
using Contacts.Models;
using System.Collections.Generic;
using System.Linq;

namespace Contacts.Repository
{
    public class MessageRepository : IMessageRepository
    {
        private List<Message> allMessages;

        public MessageRepository()
        {
            allMessages = new List<Message>();
        }

        public bool ExistsById(long id)
        {
            return allMessages.FirstOrDefault(x => x.Id == id) != null;
        }

        public IEnumerable<Message> FindAll()
        {
            return allMessages;
        }

        public Message FindById(long id)
        {
            return allMessages.FirstOrDefault(x => x.Id == id);
        }

        public Message Save(Message entity)
        {
            entity.Id = allMessages.Count + 1;
            entity.UserId = 1;
            allMessages.Add(entity);
            return entity;
        }
    }
}