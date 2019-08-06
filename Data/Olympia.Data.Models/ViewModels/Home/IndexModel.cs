namespace Olympia.Data.Models.ViewModels.Home
{
    using Olympia.Data.Models.ViewModels.BlogPartViewModels;
    using Olympia.Data.Models.ViewModels.Shop;

    using System.Collections.Generic;

    public class IndexModel
    {
        public IndexModel()
        {
            this.Articles = new HashSet<ArticleViewModel>();
            this.Items = new HashSet<ItemViewModel>();
        }

        public string TrainerName { get; set; }

        public virtual IEnumerable<ArticleViewModel> Articles { get; set; }

        public virtual IEnumerable<ItemViewModel> Items { get; set; }

        public virtual IEnumerable<string> ClientNames { get; set; }
    }
}
