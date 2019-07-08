namespace Olympia.Data.Domain
{
    using System.ComponentModel.DataAnnotations;

    public class Interest
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
