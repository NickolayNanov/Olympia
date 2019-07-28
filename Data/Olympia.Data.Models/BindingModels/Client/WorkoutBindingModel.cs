
namespace Olympia.Data.Models.BindingModels.Client
{
    using Olympia.Data.Domain.Enums;
    using System.ComponentModel.DataAnnotations;

    public class WorkoutBindingModel
    {
        public WorkoutBindingModel()
        {

        }

        [Required]
        public WorkoutDifficulty WorkoutDifficulty { get; set; }

        [Required]
        public WeekWorkoutDuration Duration { get; set; }

        [Required]
        public WorkoutType WorkoutType { get; set; }
    }
}
