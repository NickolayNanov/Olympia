namespace Olympia.Data.Models.ViewModels.Fitness
{
    using Olympia.Data.Domain;
    using Olympia.Data.Domain.Enums;

    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class WorkoutViewModel
    {
        public WorkoutViewModel()
        {
            this.Exercises = new HashSet<Exercise>();
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string ImgUrl { get; set; }

        public ICollection<Exercise> Exercises { get; set; }

        public WorkoutDifficulty WorkoutDifficulty { get; set; }

        public WorkoutType WorkoutType { get; set; }
    }
}
