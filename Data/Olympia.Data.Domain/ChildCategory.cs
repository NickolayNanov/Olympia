namespace Olympia.Data.Domain
{
    using Olympia.Data.Common.Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

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


        public virtual ICollection<ItemCategory> ItemCategories { get; set; }
    }
}
