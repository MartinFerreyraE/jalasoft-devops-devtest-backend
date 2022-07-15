
using Contacts.Models;
using System.Collections.Generic;
using System.Linq;

namespace Contacts.Repository
{ 
    public class ContactRepository : IContactRepository
    {
        private IDictionary<string, Contact> allContacts;

        public ContactRepository() 
        {
            allContacts = new Dictionary<string, Contact>();
        }

        public bool ExistsById(long id)
        {
            var entry = allContacts.FirstOrDefault(x => x.Value.Id == id);

            return !entry.Equals(null) && entry.Value != null;
        }

        public IEnumerable<Contact> FindAll()
        {
            return allContacts.Values;
        }

        public Contact FindByEmail(string email) 
        {
            return allContacts.FirstOrDefault(x => x.Value.Email.Equals(email)).Value;
        }

        public Contact FindById(long id)
        {
            return allContacts.FirstOrDefault(x => x.Value.Id == id).Value;            
        }

        public Contact Save(Contact entity)
        {
            entity.Id = allContacts.Count + 1;
            entity.UserId = 1;
            allContacts.Add(entity.Email, entity);

            return entity;
        }

        public IEnumerable<Contact> FindByName(string name) 
        {
            return allContacts.Values.Where(x => x.Name.Contains(name));
        }        
    }
}