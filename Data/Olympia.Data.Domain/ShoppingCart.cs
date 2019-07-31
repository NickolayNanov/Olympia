namespace Olympia.Data.Domain
{
    using Olympia.Data.Common.Models;

    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public class ShoppingCart : BaseModel<int>
    {
        public ShoppingCart(string userId)
        {
            this.UserId = userId;
            this.ShoppingCartItems = new HashSet<ShoppingCartItem>();
        }

        [Required]
        public string UserId { get; set; }

        public OlympiaUser User { get; set; }

        public decimal EndPrice => this.GetEndPrice();

        public virtual ICollection<ShoppingCartItem> ShoppingCartItems { get; set; }

        private decimal GetEndPrice()
        {
            return this.ShoppingCartItems.Select(x => x.Item).Sum(x => x.Price * x.TimesBought);
        }
    }
}
