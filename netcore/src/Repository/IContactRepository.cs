
using Contacts.Models;
using System.Collections.Generic;

namespace Contacts.Repository
{ 
    public interface IContactRepository : BasicRepository<Contact, long> 
    { 
        Contact FindByEmail(string email);

        IEnumerable<Contact> FindByName(string name);
    }
}