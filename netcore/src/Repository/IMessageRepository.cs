
using Contacts.Models;

namespace Contacts.Repository
{
    public interface IMessageRepository : BasicRepository<Message, long> 
    { 
    }
}