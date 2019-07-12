namespace Olympia.Data.Domain
{
    using System.ComponentModel.DataAnnotations;

    public class Address
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Location { get; set; }

        public string UserId { get; set; }

        public OlympiaUser OlympiaUser { get; set; }
    }
}
