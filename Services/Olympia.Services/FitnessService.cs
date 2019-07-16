namespace Olympia.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using Olympia.Data;
    using Olympia.Data.Domain.Enums;
    using Olympia.Data.Models.BindingModels.Client;
    using Olympia.Data.Models.ViewModels.Fitness;
    using Olympia.Services.Contracts;


    public class FitnessService : IFitnessService
    {
        private readonly OlympiaDbContext context;
        private readonly IMapper mapper;

        public FitnessService(
            OlympiaDbContext context,
            IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public IEnumerable<WorkoutViewModel> GetWorkouts(WorkoutBindingModel model)
        {
            IEnumerable<WorkoutViewModel> workouts;

            if (model.WorkoutDifficulty == WorkoutDifficulty.Intermediate)
            {
                workouts = this.mapper.ProjectTo<WorkoutViewModel>(this.context
                .Workouts
                .Where(workout =>
                    (workout.WorkoutDifficulty == WorkoutDifficulty.Intermediate ||
                    workout.WorkoutDifficulty == WorkoutDifficulty.Beginners) &&
                    workout.WorkoutType == model.WorkoutType))
                .AsEnumerable();
            }
            else if (model.WorkoutDifficulty == WorkoutDifficulty.Advanced)
            {
                workouts = this.mapper.ProjectTo<WorkoutViewModel>(this.context
                .Workouts
                .Where(workout =>
                    (workout.WorkoutDifficulty == WorkoutDifficulty.Intermediate ||
                    workout.WorkoutDifficulty == WorkoutDifficulty.Beginners ||
                    workout.WorkoutDifficulty == WorkoutDifficulty.Intermediate ||
                    workout.WorkoutDifficulty == WorkoutDifficulty.Advanced) &&
                    workout.WorkoutType == model.WorkoutType))
                .AsEnumerable();
            }
            else 
            {
                workouts = this.mapper.ProjectTo<WorkoutViewModel>(this.context
                .Workouts
                .Where(workout =>
                    workout.WorkoutDifficulty == WorkoutDifficulty.Beginners &&
                    workout.WorkoutType == model.WorkoutType))
                .AsEnumerable();
            }            

            return workouts;
        }
    }
}
