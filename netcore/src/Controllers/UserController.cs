using Contacts.Exceptions;
using Contacts.Models;
using Contacts.Repository;
using Contacts.Services;
using Microsoft.AspNetCore.Mvc;

namespace Contacts.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private IEmailService _emailService;
        private IUserRepository _userRepository;
        
        public UserController(IUserRepository repository, IEmailService emailService) 
        {
            _userRepository = repository;
            _emailService = emailService;
        }

        [HttpPost("/users")]
        public ActionResult<string> SaveUser(User inputUser)
        {
            ValidateUserFields(inputUser);
            var user = _userRepository.Save(inputUser);
            var temporalCode = user.TemporalCode.ToString().Substring(0, 6);
            _emailService.SendEmail(inputUser.Username, $"this is your verification code: {temporalCode}");
            return temporalCode;
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
        public ActionResult<User> RetrieveUsers(string code) 
        {
            var user = _userRepository.FindByCode(code);
            return user;
        }

        private void ValidateUserFields(User user)
        {
            if (string.IsNullOrEmpty(user.Username)) {
                throw new RequiredFieldException("username");
            }            
        }
    }
}
