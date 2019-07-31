namespace Olympia.Data.Domain
{
    using Olympia.Data.Domain.Enums;

    using System.ComponentModel.DataAnnotations;

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

        public OlympiaUser Owner { get; set; }

        public string OwnerId { get; set; }
    }
}
