namespace Olympia.Data.Domain
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Supplier
    {
        public Supplier()
        {
            this.Items = new HashSet<Item>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }


        [Required]
        public string Description { get; set; }

        public virtual ICollection<Item> Items { get; set; }
    }
}
