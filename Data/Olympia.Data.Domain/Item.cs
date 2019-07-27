namespace Olympia.Data.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Olympia.Data.Common.Models;

    public class Item : BaseModel<int>
    {
        public Item(string name, decimal price)
        {
            this.Name = name;
            this.Price = price;

            this.OrderItems = new HashSet<OrderItem>();
            this.ItemCategories = new HashSet<ItemCategory>();
            this.ShoppingCartItems = new HashSet<ShoppingCartItem>();

            this.CreatedOn = DateTime.UtcNow;
        }

        [Required]
        public string Description { get; set; }

        public string ImgUrl { get; set; }

        [Required]
        public string Name { get; set; }

        public decimal Price { get; set; }

        public int TimesEverBought { get; set; }

        public int TimesBought { get; set; }

        [Required]
        public Supplier Supplier { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; }

        public virtual ICollection<ItemCategory> ItemCategories { get; set; }

        public virtual ICollection<ShoppingCartItem> ShoppingCartItems { get; set; }
    }
}
