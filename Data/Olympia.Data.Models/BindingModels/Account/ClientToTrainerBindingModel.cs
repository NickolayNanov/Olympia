namespace Olympia.Data.Models.BindingModels.Account
{
    using Microsoft.AspNetCore.Http;

    using Olympia.Common;
    using Olympia.Data.Domain.Enums;

    using System.ComponentModel.DataAnnotations;

    public class ClientToTrainerBindingModel
    {
        private const int FullnameMaxLength = 100;
        private const int FullnameMinLength = 3;

        private const int AgeMaxNumber = 65;
        private const int AgeMinNumber = 12;

        private const int DescriptionMinNumber = 5;
        private const int DescriptionMaxNumber = 255;

        private const double WeightMinNumber = 1;
        private const double WeightMaxNumber = 999;

        private const double HeightMinNumber = 1;
        private const double HeightMaxNumber = 500;

        [Required]
        [Display(Name = GlobalConstants.DisplayUsername)]
        public string Username { get; set; }

        [Required]
        [StringLength(FullnameMaxLength, ErrorMessage = GlobalConstants.ErrorInputMessage, MinimumLength = FullnameMinLength)]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = GlobalConstants.AgeErrorMessage)]
        [Range(minimum: AgeMinNumber, maximum: AgeMaxNumber, ErrorMessage = GlobalConstants.AgeErrorMessage)]
        public int Age { get; set; }

        [Required]
        public Gender Gender { get; set; }

        [Display(Name = GlobalConstants.DisplayProfilePic)]
        [DataType(DataType.Upload)]
        public IFormFile ProfilePictureUrl { get; set; }

        [Required]
        [Range(WeightMinNumber, WeightMaxNumber)]
        public double Weight { get; set; }

        [Required]
        [Range(HeightMinNumber, HeightMaxNumber)]
        public double Height { get; set; }

        [Required]
        [StringLength(DescriptionMaxNumber, ErrorMessage = GlobalConstants.ErrorDescriptionMessage, MinimumLength = DescriptionMinNumber)]
        public string Description { get; set; }
    }
}
