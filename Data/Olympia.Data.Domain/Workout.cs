namespace Olympia.Data.Domain
{
    using Olympia.Data.Common.Models;
    using Olympia.Data.Domain.Enums;

    using System;
    using System.Collections.Generic;

    public class Workout : BaseModel<int>
    {
        public Workout()
        {
            this.Exercises = new List<Exercise>();

            this.CreatedOn = DateTime.UtcNow;
        }

        public string ImgUrl { get; set; }


        public string Name { get; set; }


        public WorkoutDifficulty WorkoutDifficulty { get; set; }


        public WorkoutType WorkoutType { get; set; }

        public virtual ICollection<Exercise> Exercises { get; set; }
    }
}
