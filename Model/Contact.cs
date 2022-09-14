using System;
using System.Collections.Generic;

namespace ChatApp.Model
{
    public partial class Contact
    {
        public int Id { get; set; }
        public int? OwnerId { get; set; }
        public string? ContactName { get; set; }
        public string? Mobile { get; set; }
        public string? Address { get; set; }
        public string? ImageUrl { get; set; }

        public virtual User? Owner { get; set; }
    }
}
