namespace Olympia.Data.Models.ViewModels.Fitness
{
    using Olympia.Common;
    using Olympia.Data.Domain.Enums;

    using System.ComponentModel.DataAnnotations;

    public class FitnessPlanViewModel
    {
        public int Id { get; set; }

        public int Calories { get; set; }

        public int LosingWeightCalories => this.Calories - 400;

        public int GaingingWeightCalories => this.Calories + 400;

        public WorkoutViewModel Workout { get; set; }

        [Display(Name = GlobalConstants.DisplayDuration)]
        public WeekWorkoutDuration WeekWorkoutDuration { get; set; }
    }
}
