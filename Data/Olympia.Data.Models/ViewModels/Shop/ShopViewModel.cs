namespace Olympia.Data.Models.ViewModels.Shop
{
    using Olympia.Data.Domain;

    using System.Collections.Generic;

    public class ShopViewModel
    {

        public ShopViewModel()
        {
            this.Items = new HashSet<ItemViewModel>();
        }

        public ShoppingCart ShoppingCart { get; set; }

        public virtual IEnumerable<ItemViewModel> Items { get; set; }
    }
}
