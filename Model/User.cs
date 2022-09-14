using System;
using System.Collections.Generic;

namespace ChatApp.Model
{
    public partial class User
    {
        public User()
        {
            Contacts = new HashSet<Contact>();
            MessageReceivers = new HashSet<Message>();
            MessageSenders = new HashSet<Message>();
        }

        public int Id { get; set; }
        public string? FullName { get; set; }
        public string? MailId { get; set; }
        public string? Mobile { get; set; }
        public string? Password { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsLoggedIn { get; set; }
        public string? ImageUrl { get; set; }

        public virtual ICollection<Contact> Contacts { get; set; }
        public virtual ICollection<Message> MessageReceivers { get; set; }
        public virtual ICollection<Message> MessageSenders { get; set; }
    }
}
