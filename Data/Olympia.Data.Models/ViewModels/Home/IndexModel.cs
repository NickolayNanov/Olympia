using Olympia.Data.Models.ViewModels.BlogPartViewModels;
using System.Collections.Generic;

namespace Olympia.Data.Models.ViewModels.Home
{
    public class IndexModel
    {
        public IEnumerable<ArticleViewModel> Articles { get; set; }

        public IEnumerable<string> ClientNames { get; set; }

        public string TrainerName { get; set; }
    }
}
