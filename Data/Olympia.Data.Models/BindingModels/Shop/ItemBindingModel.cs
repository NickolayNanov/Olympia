namespace Olympia.Data.Models.BindingModels.Shop
{
    using Microsoft.AspNetCore.Http;

    using Olympia.Common;

    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class ItemBindingModel
    {
        public ItemBindingModel()
        {
            this.SupplierNames = new HashSet<string>();
        }

        [Required]
        public string Description { get; set; }

        [Required]
        public IFormFile ImgUrl { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Range(0.01, 9999.99, ErrorMessage = GlobalConstants.ItemPriceErrorMessage)]
        public double Price { get; set; }

        [Required]
        [Display(Name = GlobalConstants.DisplayCategoryName)]
        public Category CategoryName { get; set; }

        [Required]
        public string SupplierName { get; set; }

        public IEnumerable<string> SupplierNames { get; set; }
    }
}
