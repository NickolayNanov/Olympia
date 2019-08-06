namespace Olympia.Data.Domain
{
    using Olympia.Data.Common.Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Message : BaseModel<int>
    {
        public Message()
        {
            this.UserMessages = new HashSet<UserMessages>();
            this.CreatedOn = DateTime.Now;
        }


        [Required]
        public string Content { get; set; }

        public string SenderId { get; set; }

        public OlympiaUser Sender { get; set; }

        public string ReceiverId { get; set; }

        public OlympiaUser Receiver { get; set; }

        public virtual ICollection<UserMessages> UserMessages { get; set; }
    }
}
