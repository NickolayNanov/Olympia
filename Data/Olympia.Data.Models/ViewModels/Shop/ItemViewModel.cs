namespace Olympia.Data.Models.ViewModels.Shop
{
    using Olympia.Data.Domain;

    using System.Collections.Generic;

    public class ItemViewModel
    {
        public ItemViewModel()
        {
            OrderItems = new HashSet<OrderItem>();
            ItemCategories = new HashSet<ItemCategory>();
        }

        public int Id { get; set; }

        public string Description { get; set; }


        public string ImgUrl { get; set; }


        public string Name { get; set; }


        public decimal Price { get; set; }


        public int ShoppingCardId { get; set; }


        public int TimesBought { get; set; }


        public ShoppingCart ShoppingCart { get; set; }


        public Supplier Supplier { get; set; }


        public virtual ICollection<OrderItem> OrderItems { get; set; }


        public ICollection<ItemCategory> ItemCategories { get; set; }
    }
}
