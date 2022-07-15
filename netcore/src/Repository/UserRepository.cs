using Contacts.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Contacts.Repository
{
    public class UserRepository : IUserRepository
    {
        private HashSet<User> allUsers;

        public UserRepository() 
        {
            allUsers = new HashSet<User>();
        }

        public bool ExistsById(long id)
        {
            return allUsers.Any(x => x.Id == id);
        }

        User IUserRepository.FindByCode(string code)
        {
            return allUsers.FirstOrDefault(x => x.TemporalCode.ToString().Substring(0, 6).Equals(code));
        }

        public IEnumerable<User> FindAll()
        {
            return allUsers;
        }

        public User FindById(long id)
        {
            return allUsers.FirstOrDefault(x => x.Id == id);
        }

        public User FindByUsernameAndPassword(string username, string password)
        {
            return allUsers.FirstOrDefault(x => x.Username.Equals(username) && x.Password.Equals(password));
        }

        public User Save(User entity)
        {
            var existentUser = FindById(entity.Id);
            if (existentUser != null) 
            {
                allUsers.RemoveWhere(x => x.Id == entity.Id);                
            }
            else 
            {
                entity.Id = allUsers.Count + 1;
                entity.TemporalCode = Guid.NewGuid();
            }

            allUsers.Add(entity);

            return entity;
        }

        
    }
}