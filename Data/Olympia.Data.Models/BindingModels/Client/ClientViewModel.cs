namespace Olympia.Data.Models.BindingModels.Client
{
    using Olympia.Data.Domain.Enums;

    public class ClientViewModel
    {
        public string FullName { get; set; }

        public string UserName { get; set; }

        public double Weight { get; set; }

        public double Height { get; set; }

        public int Age { get; set; }

        public ActityLevel Activity { get; set; }

        public Gender Gender { get; set; }

        public int Calories { get; set; }
    }
}
