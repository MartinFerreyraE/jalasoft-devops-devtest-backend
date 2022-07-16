using System.Threading.Tasks;

namespace Contacts.Services
{
    public interface IEmailService
    {
        Task SendEmail(string email, string body);
    }
}
