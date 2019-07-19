namespace Olympia.Data.Models.BindingModels.Shop
{
    using System.ComponentModel.DataAnnotations;

    public class SupplierBindingModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
