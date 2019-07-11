namespace Olympia.Data.Models.BindingModels.Account
{
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
        [Display(Name = DisplayModelConstatnts.DisplayUsername)]
        public string Username { get; set; }

        [Required]
        [StringLength(FullnameMaxLength, ErrorMessage = ErrorConstants.ErrorInputMessage, MinimumLength = FullnameMinLength)]
        public string FullName { get; set; }

        [Required]
        [StringLength(PasswordMaxLength, ErrorMessage = ErrorConstants.ErrorInputMessage, MinimumLength = PasswordMinLength)]
        [DataType(DataType.Password)]
        [Display(Name = DisplayModelConstatnts.DisplayPassword)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = DisplayModelConstatnts.DisplayConfirmPassword)]
        [Compare(DisplayModelConstatnts.DisplayPassword, ErrorMessage = ErrorConstants.ConfirmPasswordErrorMessage)]
        public string ConfirmPassword { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Range(minimum: AgeMinNumber, maximum: AgeMaxNumber, ErrorMessage = ErrorConstants.AgeErrorMessage)]
        public int Age { get; set; }

        [Required]
        public Gender Gender { get; set; }
    }
}
