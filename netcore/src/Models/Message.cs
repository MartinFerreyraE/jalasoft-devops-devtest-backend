using System.Collections.Generic;

namespace Contacts.Models
{
    public class Message
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public string Content { get; set; }
        public HashSet<long> Contacts { get; set; }
    }
}