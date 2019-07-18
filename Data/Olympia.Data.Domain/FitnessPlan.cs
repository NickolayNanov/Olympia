namespace Olympia.Data.Domain
{
    using System.ComponentModel.DataAnnotations;

    using Olympia.Data.Domain.Enums;

    public class FitnessPlan
    {
        public FitnessPlan()
        {
            this.Workout = new Workout();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public int CaloriesGoal { get; set; }

        [Required]
        public Workout Workout { get; set; }

        [Required]
        public WeekWorkoutDuration WeekWorkoutDuration { get; set; }
    }
}
