namespace Olympia.Data.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Olympia.Data.Common.Models;

    public class Article : BaseModel<int>
    {
        public Article()
        {
            this.Comments = new HashSet<Comment>();
            this.CreatedOn = DateTime.UtcNow;
        }

        [Required]
        public string Content { get; set; }

        [Required]
        public string Title { get; set; }

        public int TimesRead { get; set; }

        public string ImgUrl { get; set; }
        public OlympiaUser Author { get; set; }

        public string AuthorId { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    }
}
