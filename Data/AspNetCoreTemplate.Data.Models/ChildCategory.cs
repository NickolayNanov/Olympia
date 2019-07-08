namespace Olympia.Data.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Olympia.Data.Common.Models;

    public class ChildCategory : BaseModel<int>
    {
        public ChildCategory()
        {
            this.ItemCategories = new HashSet<ItemCategory>();
            this.CreatedOn = DateTime.UtcNow;
        }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public string ImageUrl { get; set; }

        [Required]
        public ParentCategory ParentCategory { get; set; }

        public virtual ICollection<ItemCategory> ItemCategories { get; set; }
    }
}
