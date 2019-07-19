namespace Olympia.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using Microsoft.EntityFrameworkCore;
    using Olympia.Data;
    using Olympia.Data.Domain;
    using Olympia.Data.Domain.Enums;
    using Olympia.Data.Models.BindingModels.Client;
    using Olympia.Data.Models.BindingModels.Shop;
    using Olympia.Data.Models.ViewModels.Fitness;
    using Olympia.Data.Models.ViewModels.Shop;
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

        public async Task<bool> AddSupplierAsync(SupplierBindingModel model)
        {
            if (string.IsNullOrEmpty(model.Name) ||
                this.context.Suppliers.FirstOrDefault(sup => sup.Name == model.Name) != null)
            {
                return false;
            }

            var supplier = this.mapper.Map<Supplier>(model);

            this.context.Suppliers.Add(supplier);
            await this.context.SaveChangesAsync();

            return await this.context.Suppliers.ContainsAsync(supplier);
        }

        public IEnumerable<ItemViewModel> GetAllItems()
        {
            var items = this.mapper.ProjectTo<ItemViewModel>(this.context.Items.Include(x => x.Supplier)).AsEnumerable();

            return items;
        }

        public IEnumerable<Supplier> GetAllSuppliers()
        {
            var suppliers = this.context
                .Suppliers
                .Include(supplier => supplier.Items)
                .AsEnumerable();

            return suppliers;
        }

        public WorkoutViewModel GetWorkoutById(int workoutId)
        {
            return this.mapper.Map<WorkoutViewModel>(this.context
                .Workouts
                .Include(workout => workout.Exercises)
                .SingleOrDefault(workout => workout.Id == workoutId));
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
