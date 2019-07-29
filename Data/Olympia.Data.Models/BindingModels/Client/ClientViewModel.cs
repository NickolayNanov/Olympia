namespace Olympia.Data.Models.BindingModels.Client
{
    using Olympia.Data.Domain.Enums;
    using Olympia.Data.Models.ViewModels.Fitness;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class ClientViewModel
    {
        public ClientViewModel()
        {
            this.WorkoutInputModel = new WorkoutBindingModel();
            this.WorkoutViewModel = new WorkoutViewModel();
            this.Workouts = new HashSet<WorkoutViewModel>();
        }

        public string FullName { get; set; }

        public string UserName { get; set; }

        public double Weight { get; set; }

        public double Height { get; set; }

        public int Age { get; set; }

        public string TrainerName { get; set; }

        public ActityLevel Activity { get; set; }

        public Gender Gender { get; set; }

        public WeekWorkoutDuration WeekWorkoutDuration { get; set; }

        [Required]
        public int Calories { get; set; }


        public WorkoutViewModel WorkoutViewModel { get; set; }

        public WorkoutBindingModel WorkoutInputModel { get; set; }

        public IEnumerable<WorkoutViewModel> Workouts { get; set; }
    }
}
