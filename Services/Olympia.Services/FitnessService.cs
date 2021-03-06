﻿namespace Olympia.Services
{
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

    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;


    public class FitnessService : IFitnessService
    {
        private readonly OlympiaDbContext context;
        private readonly IMapper mapper;
        private readonly IUsersService usersService;

        public FitnessService(
            OlympiaDbContext context,
            IMapper mapper,
            IUsersService usersService)
        {
            this.context = context;
            this.mapper = mapper;
            this.usersService = usersService;
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

        public async Task<ShopViewModel> GetAllItemsAsync()
        {
            ShopViewModel model = new ShopViewModel();

            await Task.Run(() =>
            {
                model.Items = this.mapper.ProjectTo<ItemViewModel>(this.context.Items.Include(x => x.Supplier)).AsEnumerable();
            });

            return model;
        }

        public async Task<IEnumerable<Supplier>> GetAllSuppliersAsync()
        {
            IEnumerable<Supplier> suppliers = new List<Supplier>();

            await Task.Run(() =>
            {
                suppliers = this.context
                .Suppliers
                .Include(supplier => supplier.Items)
                .AsEnumerable();
            });

            return suppliers;
        }

        public async Task<FitnessPlanViewModel> GetFitnessPlanByUsernameAsync(string username)
        {
            var user = await this.usersService.GetUserByUsernameAsync(username);

            if (user == null)
            {
                return null;
            }

            var fitnessPlanFromDb = await this.context
                .FitnessPlans
                .Include(x => x.Workout)
                .ThenInclude(x => x.Exercises)
                .SingleOrDefaultAsync(fitnessPlan => fitnessPlan.OwnerId == user.Id);


            var dto = this.mapper.Map<FitnessPlanViewModel>(fitnessPlanFromDb);
            return dto;
        }

        public async Task<WorkoutViewModel> GetWorkoutByIdAsync(int workoutId)
        {
            WorkoutViewModel model = new WorkoutViewModel();

            await Task.Run(() =>
            {
                model = this.mapper.Map<WorkoutViewModel>(this.context
                .Workouts
                .Include(workout => workout.Exercises)
                .SingleOrDefault(workout => workout.Id == workoutId));
            });

            return model;
        }

        public async Task<IEnumerable<WorkoutViewModel>> GetWorkoutsAsync(WorkoutBindingModel model)
        {
            IEnumerable<WorkoutViewModel> workouts = new List<WorkoutViewModel>();

            await Task.Run(() =>
            {
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
            });

            return workouts;
        }
    }
}
