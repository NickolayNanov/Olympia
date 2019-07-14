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

        private const int PasswordMaxLength = 50;
        private const int PasswordMinLength = 6;

        private const int AgeMaxNumber = 65;
        private const int AgeMinNumber = 12;

        private const int DescriptionMinNumber = 5;
        private const int DescriptionMaxNumber = 255;

        [Required]
        [Display(Name = DisplayModelConstatnts.DisplayUsername)]
        public string Username { get; set; }

        [Required]
        [StringLength(FullnameMaxLength, ErrorMessage = ErrorConstants.ErrorInputMessage, MinimumLength = FullnameMinLength)]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Range(minimum: AgeMinNumber, maximum: AgeMaxNumber, ErrorMessage = ErrorConstants.AgeErrorMessage)]
        public int Age { get; set; }

        [Required]
        public Gender Gender { get; set; }

        [Display(Name = DisplayModelConstatnts.DisplayProfilePic)]
        [DataType(DataType.Upload)]
        public IFormFile ProfilePictureUrl;

        public double Weight { get; set; }

        public double Height { get; set; }

        [Required]
        [StringLength(DescriptionMaxNumber, ErrorMessage = ErrorConstants.ErrorInputMessage, MinimumLength = DescriptionMinNumber)]
        public string Description { get; set; }
    }
}
