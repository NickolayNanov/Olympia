namespace Olympia.Data.Domain
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class ParentCategory
    {
        public ParentCategory()
        {
            this.ChildCategories = new HashSet<ChildCategory>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<ChildCategory> ChildCategories { get; set; }
    }
}
