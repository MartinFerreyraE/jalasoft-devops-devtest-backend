
using Contacts.Models;

namespace Contacts.Repository
{
    public interface IUserRepository : BasicRepository<User, long> 
    { 
        User FindByUsernameAndPassword(string username, string password);

        User FindByCode(string code);
    }
}