using Olympia.Common;
using Olympia.Data.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Olympia.Data.Models.ViewModels.Fitness
{
    public class FitnessPlanViewModel
    {
        public WorkoutViewModel Workout { get; set; }

        public int Calories { get; set; }

        [Display(Name = GlobalConstants.DisplayDuration)]
        public WeekWorkoutDuration WeekWorkoutDuration { get; set; }
    }
}
