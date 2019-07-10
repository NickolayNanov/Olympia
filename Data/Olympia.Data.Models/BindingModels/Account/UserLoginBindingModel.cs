namespace Olympia.Data.Models.BindingModels.Account
{
    using Olympia.Common;
    using System.ComponentModel.DataAnnotations;

    public class UserLoginBindingModel
    {
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = DisplayModelConstatnts.DisplayUsername)]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = DisplayModelConstatnts.DisplayPassword)]
        public string Password { get; set; }

        [Display(Name = DisplayModelConstatnts.DisplayRememberMe)]
        public bool RememberMe { get; set; }
    }
}
