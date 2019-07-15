namespace Olympia.Data.Models.ViewModels.BlogPartViewModels
{
    using System;
    using System.Collections.Generic;

    using Olympia.Data.Common.Models;
    using Olympia.Data.Domain;

    public class ArticleViewModel : BaseModel<int>
    {
        public ArticleViewModel()
        {
            this.CreatedOn = DateTime.UtcNow;

            this.Comments = new HashSet<Comment>();
        }

        public string Content { get; set; }

        public string Title { get; set; }

        public OlympiaUser Author { get; set; }

        public string ImgUrl { get; set; }

        public string AuthorId { get; set; }

        public int TimesRead { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    }
}
