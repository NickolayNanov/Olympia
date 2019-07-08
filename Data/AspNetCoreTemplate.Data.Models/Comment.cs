namespace Olympia.Data.Domain
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Olympia.Data.Common.Models;

    public class Comment : BaseModel<int>
    {
        public Comment()
        {
            this.CreatedOn = DateTime.UtcNow;
        }

        [Required]
        public string Content { get; set; }

        public int ArticleId { get; set; }

        [Required]
        public Article Article { get; set; }

        [Required]
        public string AuthorId { get; set; }

        public OlympiaUser Author { get; set; }
    }
}
