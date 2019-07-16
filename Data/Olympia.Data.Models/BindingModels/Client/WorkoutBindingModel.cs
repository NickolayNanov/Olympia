
namespace Olympia.Data.Models.BindingModels.Client
{
    using System.ComponentModel.DataAnnotations;
    using Olympia.Data.Domain.Enums;

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
