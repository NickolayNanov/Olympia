namespace Olympia.Data.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Olympia.Data.Common.Models;

    public class Order : BaseModel<int>
    {
        public Order()
        {
            this.CreatedOn = DateTime.UtcNow;

            this.OrderDate = DateTime.UtcNow;
            this.ExpectedDeliveryDate = DateTime.UtcNow.AddDays(3);

            this.OrderItems = new HashSet<OrderItem>();
        }
        [Required]
        public string UserId { get; set; }

        public OlympiaUser User { get; set; }

        public DateTime OrderDate { get; set; }

        public decimal EndPrice { get; set; }

        public DateTime ExpectedDeliveryDate { get; set; }

        [Required]
        public string DeliveryAddress { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
