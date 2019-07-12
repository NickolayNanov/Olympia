namespace Olympia.Data.Domain
{
    using System.ComponentModel.DataAnnotations;

    using Olympia.Data.Domain.Enums;

    public class FitnessPlan
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DietPlan DietPlan { get; set; }

        [Required]
        public Workout Workout { get; set; }

        [Required]
        public WeekWorkoutDuration WeekWorkoutDuration { get; set; }
    }
}
