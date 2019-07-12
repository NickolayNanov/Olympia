namespace Olympia.Data.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Olympia.Data.Common.Models;
    using Olympia.Data.Domain.Enums;

    public class Workout : BaseModel<int>
    {
        public Workout()
        {
            this.Exercises = new List<Exercise>();

            this.CreatedOn = DateTime.UtcNow;
        }

        [Required]
        public string Name { get; set; }

        [Required]
        public WorkoutDifficulty WorkoutDifficulty { get; set; }

        [Required]
        public WorkoutType WorkoutType { get; set; }

        public virtual ICollection<Exercise> Exercises { get; set; }
    }
}
