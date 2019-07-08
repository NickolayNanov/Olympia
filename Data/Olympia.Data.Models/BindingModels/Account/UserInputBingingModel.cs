namespace Olympia.Data.Models.BindingModels.Account
{
    using Olympia.Common;
    using Olympia.Data.Domain.Enums;
    using System.ComponentModel.DataAnnotations;

    public class UserInputBingingModel
    {
        [Required]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = GlobalConstants.ErrorInputMessage, MinimumLength = 3)]
        public string FullName { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = GlobalConstants.ErrorInputMessage, MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = GlobalConstants.ConfirmPasswordErrorMessage)]
        public string ConfirmPassword { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Range(12, 96, ErrorMessage = GlobalConstants.AgeErrorMessage)]
        public int Age { get; set; }

        [Required]
        public Gender Gender { get; set; }
    }
}
