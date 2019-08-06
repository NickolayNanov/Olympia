namespace Olympia.Data.Models.BindingModels.Shop
{
    using Microsoft.AspNetCore.Http;

    using Olympia.Common;

    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class ItemBindingModel
    {
        private const double PriceMinValue = 0.01;
        private const double PriceMaxValue = 9999.99;

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
        [Range(PriceMinValue, PriceMaxValue, ErrorMessage = GlobalConstants.ItemPriceErrorMessage)]
        public double Price { get; set; }

        [Required]
        public string SupplierName { get; set; }

        [Required]
        [Display(Name = GlobalConstants.DisplayCategoryName)]
        public Category CategoryName { get; set; }

        public virtual IEnumerable<string> SupplierNames { get; set; }
    }
}
