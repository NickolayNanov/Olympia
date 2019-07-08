namespace Olympia.Data.Domain
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Olympia.Data.Common.Models;

    public class Review : BaseModel<int>
    {
        public Review()
        {
            this.CreatedOn = DateTime.UtcNow;
        }

        public double Rating { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public int ItemId { get; set; }
        public Item Item { get; set; }
    }
}
