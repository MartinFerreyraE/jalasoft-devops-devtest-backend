namespace Contacts.Models
{
    public class Contact
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}