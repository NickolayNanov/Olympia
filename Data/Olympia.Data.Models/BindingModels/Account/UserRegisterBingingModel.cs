namespace Olympia.Data.Models.BindingModels.Account
{
    using Microsoft.AspNetCore.Http;
    using Olympia.Common;
    using Olympia.Data.Domain.Enums;
    using System.ComponentModel.DataAnnotations;

    public class UserRegisterBingingModel
    {
        private const int FullnameMaxLength = 100;
        private const int FullnameMinLength = 3;

        private const int PasswordMaxLength = 50;
        private const int PasswordMinLength = 6;

        private const int AgeMaxNumber = 65;
        private const int AgeMinNumber = 12;

        [Required]
        [Display(Name = GlobalConstants.DisplayUsername)]
        public string Username { get; set; }

        [Required]
        [StringLength(FullnameMaxLength, ErrorMessage = GlobalConstants.ErrorInputMessage, MinimumLength = FullnameMinLength)]
        public string FullName { get; set; }

        [Required]
        [StringLength(PasswordMaxLength, ErrorMessage = GlobalConstants.ErrorInputMessage, MinimumLength = PasswordMinLength)]
        [DataType(DataType.Password)]
        [Display(Name = GlobalConstants.DisplayPassword)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = GlobalConstants.DisplayConfirmPassword)]
        [Compare(GlobalConstants.DisplayPassword, ErrorMessage = GlobalConstants.ConfirmPasswordErrorMessage)]
        public string ConfirmPassword { get; set; }

        [Required]
        [StringLength(FullnameMaxLength, ErrorMessage = GlobalConstants.ErrorInputMessage, MinimumLength = FullnameMinLength)]
        public string Email { get; set; }

        [Required]
        [StringLength(300, ErrorMessage = GlobalConstants.ErrorInputMessage, MinimumLength = FullnameMinLength)]
        public string Address { get; set; }

        [Required]
        [Range(minimum: AgeMinNumber, maximum: AgeMaxNumber, ErrorMessage = GlobalConstants.AgeErrorMessage)]
        public int Age { get; set; }

        [Required]
        public Gender Gender { get; set; }

        [Display(Name = GlobalConstants.DisplayProfilePic)]
        [DataType(DataType.Upload)]
        public IFormFile ProfilePicturImgUrl { get; set; }
    }
}
