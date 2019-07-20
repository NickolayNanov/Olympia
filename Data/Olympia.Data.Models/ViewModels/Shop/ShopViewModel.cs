namespace Olympia.Data.Models.ViewModels.Shop
{
    using System.Collections.Generic;

    public class ShopViewModel
    {

        public ShopViewModel()
        {
            this.Items = new HashSet<ItemViewModel>();
        }

        public IEnumerable<ItemViewModel> Items { get; set; }
    }
}
