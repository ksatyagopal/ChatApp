using System;
using System.Collections.Generic;

namespace ChatApp.Model
{
    public partial class Message
    {
        public int Id { get; set; }
        public int? SenderId { get; set; }
        public int? ReceiverId { get; set; }
        public string? MessageBody { get; set; }
        public string? SentTime { get; set; }

        public virtual User? Receiver { get; set; }
        public virtual User? Sender { get; set; }
    }
}
