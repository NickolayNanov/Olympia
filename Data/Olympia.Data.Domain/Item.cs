﻿namespace Olympia.Data.Domain
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

            this.Reviews = new HashSet<Review>();
            this.OrderItems = new HashSet<OrderItem>();
            this.ItemCategories = new HashSet<ItemCategory>();

            this.CreatedOn = DateTime.UtcNow;
        }

        [Required]
        public string Description { get; set; }

        public string ImgUrl { get; set; }

        [Required]
        public string Name { get; set; }

        public decimal Price { get; set; }

        [Required]
        public int ShoppingCardId { get; set; }

        public int TimesBought { get; set; }

        public ShoppingCart ShoppingCart { get; set; }

        [Required]
        public Supplier Supplier { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; }

        public virtual ICollection<ItemCategory> ItemCategories { get; set; }
    }
}