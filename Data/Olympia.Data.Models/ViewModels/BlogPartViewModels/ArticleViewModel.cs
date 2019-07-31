namespace Olympia.Data.Models.ViewModels.BlogPartViewModels
{
    using Olympia.Data.Common.Models;
    using Olympia.Data.Domain;

    using System;

    public class ArticleViewModel : BaseModel<int>
    {
        public ArticleViewModel()
        {
            this.CreatedOn = DateTime.UtcNow;
        }

        public string Content { get; set; }

        public string Title { get; set; }

        public OlympiaUser Author { get; set; }

        public string ImgUrl { get; set; }

        public string AuthorId { get; set; }

        public int TimesRead { get; set; }

    }
}
