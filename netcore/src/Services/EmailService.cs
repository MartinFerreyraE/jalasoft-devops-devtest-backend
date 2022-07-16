using Microsoft.Extensions.Configuration;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Contacts.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        private readonly SmtpClient smtpClient;
        public string From { get; set; }
        public int Port { get; set; }
        public string Host { get; set; }
        public bool EnableSSL { get; set; }
        public bool UseDefaultCredentials { get; set; }

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
            From = _configuration.GetSection("Smtp").GetSection("Email").Value;
            smtpClient = new SmtpClient();
            smtpClient.Port = 587;
            smtpClient.Host = _configuration.GetSection("Smtp").GetSection("Host").Value;   
            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential(From, _configuration.GetSection("Smtp").GetSection("Password").Value);
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
        }
        public Task SendEmail(string email, string body)
        {
            try
            {
                var mail = new MailMessage();
                mail.From = new MailAddress(From);
                mail.To.Add(email);
                mail.Subject = "Verification code";
                mail.IsBodyHtml = false;
                mail.Body = body;
                smtpClient.Send(mail);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return Task.CompletedTask;
        }
    }
}
