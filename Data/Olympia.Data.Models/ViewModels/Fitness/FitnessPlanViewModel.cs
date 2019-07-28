﻿using Olympia.Common;
using Olympia.Data.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Olympia.Data.Models.ViewModels.Fitness
{
    public class FitnessPlanViewModel
    {
        public int Id { get; set; }

        public WorkoutViewModel Workout { get; set; }

        public int Calories { get; set; }

        [Display(Name = GlobalConstants.DisplayDuration)]
        public WeekWorkoutDuration WeekWorkoutDuration { get; set; }
    }
}
