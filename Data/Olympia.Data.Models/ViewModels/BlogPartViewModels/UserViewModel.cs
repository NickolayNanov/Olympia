namespace Olympia.Data.Models.ViewModels.BlogPartViewModels
{
    using Olympia.Data.Domain.Enums;

    public class UserViewModel
    {
        public string Username { get; set; }

        public string FullName { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public string Email { get; set; }

        public int Age { get; set; }

        public Gender Gender { get; set; }
    }
}
