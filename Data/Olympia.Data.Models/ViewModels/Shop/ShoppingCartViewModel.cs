namespace Olympia.Data.Models.ViewModels.Shop
{
    using System.Collections.Generic;
    using System.Linq;

    public class ShoppingCartViewModel
    {
        public ShoppingCartViewModel()
        {
            this.Items = new HashSet<ItemViewModel>();
        }

        public int Id { get; set; }

        public decimal EndPrice => this.GetEndPrice();

        public ICollection<ItemViewModel> Items { get; set; }

        private decimal GetEndPrice()
        {
            return this.Items.Sum(x => x.Price * x.TimesBought);
        }
    }
}
