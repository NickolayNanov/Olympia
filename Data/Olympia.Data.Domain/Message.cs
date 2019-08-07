namespace Olympia.Data.Domain
{
    using Olympia.Data.Common.Models;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Message : BaseModel<int>
    {
        public Message()
        {
            this.CreatedOn = DateTime.Now;
        }
        public string SenderId { get; set; }

        public OlympiaUser Sender { get; set; }

        public string ReceiverId { get; set; }

        [NotMapped]
        public OlympiaUser Receiver { get; set; }

        [Required]
        public string Content { get; set; }
    }
}
