using Contacts.Exceptions;
using Contacts.Models;
using Contacts.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Contacts.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    { 
        private IUserRepository _userRepository;
        
        public UserController(IUserRepository repository) 
        {
            _userRepository = repository;
        }

        [HttpPost("/users")]
        public ActionResult<string> SaveUser(User inputUser)
        {
            ValidateUserFields(inputUser);
            var user = _userRepository.Save(inputUser);
            return user.TemporalCode.ToString().Substring(0, 6);
        }

        [HttpPost("/sign-in")]
        public ActionResult<bool> AuthenticateUser(User user) 
        {
            var result = _userRepository.FindByUsernameAndPassword(user.Username, user.Password);
            if (result == null) 
            {
                throw new InvalidCredentialsException($"Invalid Username or Password for user.");
            }

            return true;
        }

        [HttpPut("/users")]
        public ActionResult<User> UpdateUser(User inputUser) 
        {
            ValidateUserFields(inputUser);
            var user = _userRepository.Save(inputUser);
            return user;
        }

        [HttpGet("/users/{code}")]
        public ActionResult<bool> RetrieveUsers(string code) 
        {
            return _userRepository.FindByCode(code) != null;
        }

        private void ValidateUserFields(User user)
        {
            if (string.IsNullOrEmpty(user.Username)) {
                throw new RequiredFieldException("username");
            }            
        }
    }
}
