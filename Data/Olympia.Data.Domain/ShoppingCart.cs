namespace Olympia.Data.Domain
{
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    using Olympia.Data.Common.Models;

    public class ShoppingCart : BaseModel<int>
    {
        public ShoppingCart(string userId)
        {
            this.UserId = userId;
            this.Items = new HashSet<Item>();
        }

        [Required]
        public string UserId { get; set; }

        public OlympiaUser User { get; set; }

        public decimal EndPrice => this.GetEndPrice();

        public virtual ICollection<Item> Items { get; set; }

        private decimal GetEndPrice()
        {
            return this.Items.Sum(x => x.Price);
        }
    }
}
