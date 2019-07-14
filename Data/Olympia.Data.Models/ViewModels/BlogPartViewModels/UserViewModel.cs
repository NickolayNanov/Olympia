namespace Olympia.Data.Models.ViewModels.BlogPartViewModels
{
    using Olympia.Data.Domain.Enums;

    public class UserViewModel
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public string FullName { get; set; }

        public string Weight { get; set; }

        public string Height { get; set; }

        public int Age { get; set; }

        public Gender Gender { get; set; }
    }
}
