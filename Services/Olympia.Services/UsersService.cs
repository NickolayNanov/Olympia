namespace Olympia.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Olympia.Common;
    using Olympia.Data;
    using Olympia.Data.Domain;
    using Olympia.Data.Domain.Enums;
    using Olympia.Data.Models.BindingModels.Account;
    using Olympia.Data.Models.BindingModels.Client;
    using Olympia.Data.Models.ViewModels.AdminViewModels;
    using Olympia.Data.Models.ViewModels.BlogPartViewModels;
    using Olympia.Data.Models.ViewModels.Home;
    using Olympia.Services.Contracts;
    using Olympia.Services.Utilities;

    public class UsersService : IUsersService
    {
        private readonly OlympiaDbContext context;
        private readonly IMapper mapper;
        private readonly UserManager<OlympiaUser> userManager;
        private readonly RoleManager<OlympiaUserRole> roleManager;

        public UsersService(
            OlympiaDbContext context,
            IMapper mapper,
            UserManager<OlympiaUser> userManager,
            RoleManager<OlympiaUserRole> roleManager)
        {
            this.context = context;
            this.mapper = mapper;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public async Task<IEnumerable<OlympiaUser>> GetAllTrainersAsync()
        {
            IEnumerable<OlympiaUser> trainers = new List<OlympiaUser>();

            // var trainers = await this.userManager.GetUsersInRoleAsync(GlobalConstants.TrainerRoleName);
            // cannot include articles when using user manager
            await Task.Run(async () =>
            {
                var adminRole = await this.roleManager.GetRoleIdAsync(await this.roleManager.FindByNameAsync(GlobalConstants.TrainerRoleName));
                var trainerIds = this.context.UserRoles
                    .Where(ur => ur.RoleId == adminRole)
                    .Select(x => x.UserId)
                    .ToList();

                trainers = this.context
                    .Users
                    .Include(x => x.Articles)
                    .Where(id => trainerIds.Any(x => x == id.Id))
                    .OrderByDescending(trainer => trainer.Rating);
            });

            return trainers;
        }

        public async Task<IEnumerable<UserViewModel>> GetAllClientsByUserAsync(string trainerUsername)
        {
            if (string.IsNullOrEmpty(trainerUsername))
            {
                return new List<UserViewModel>();
            }

            IEnumerable<UserViewModel> clients = new List<UserViewModel>();

            await Task.Run(() =>
            {
                clients = this.context
                .Users
                .Where(x => x.Trainer.UserName == trainerUsername)
                .Select(x => this.mapper.Map<UserViewModel>(x));
            });

            return clients;
        }


        public async Task<OlympiaUser> GetUserByUsernameAsync(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                return null;
            }

            OlympiaUser userFromDb = null;

            await Task.Run(() =>
            {
                userFromDb = this.context
                .Users
                .Include(x => x.Clients)
                .Include(x => x.Trainer)
                .Include(x => x.ShoppingCart)
                .Include(x => x.FitnessPlan)
                .SingleOrDefault(user => user.UserName == username);
            });

            return userFromDb;
        }

        public async Task<bool> SetTrainerAsync(string trainerUsername, string clientUsername)
        {
            if(string.IsNullOrEmpty(trainerUsername) ||
                string.IsNullOrEmpty(clientUsername))
            {
                return false;
            }

            bool done = false;

            await Task.Run(async () =>
            {
                var trainer = await this.GetUserByUsernameAsync(trainerUsername);
                var client = await this.GetUserByUsernameAsync(clientUsername);

                trainer.Clients.Add(client);
                client.TrainerId = trainer.Id;

                trainer.Rating = trainer.Clients.Count * 0.4;

                this.context.Update(trainer);
                this.context.Update(client);
                this.context.SaveChanges();

                done = client.TrainerId == trainer.Id;
            });

            return done;
        }


        public async Task<bool> BecomeTrainerAsync(ClientToTrainerBindingModel model, string username)
        {
            OlympiaUser realUser = this.context.Users.Include(x => x.OlympiaUserRole).SingleOrDefault(user => user.UserName == username);

            realUser.Age = model.Age;
            realUser.Description = model.Description;
            realUser.Email = model.Email;
            realUser.FullName = model.FullName;
            realUser.Weight = model.Weight;
            realUser.Height = model.Height;

            if (model.ProfilePictureUrl != null)
            {                
                var url = MyCloudinary.UploadImage(model.ProfilePictureUrl, model.Username);
                realUser.ProfilePicturImgUrl = url ?? Constants.CloudinaryInvalidUrl;
            }

            await this.userManager.UpdateSecurityStampAsync(realUser);

            var roleHasChanged = await this.userManager.AddToRoleAsync(realUser, GlobalConstants.TrainerRoleName);

            if (!roleHasChanged.Succeeded)
            {
                return false;
            }

            await this.userManager.UpdateAsync(realUser);
            this.context.Update(realUser);
            await this.context.SaveChangesAsync();
            return true;
        }

        public async Task<OlympiaUser> GetUsersTrainerAsync(string username)
        {
            OlympiaUser trainer = null;

            await Task.Run(async () =>
            {
                trainer = await this.context
                .Users
                .Include(user => user.Trainer)
                .SingleOrDefaultAsync(x => x.UserName == username);
            });

            return trainer.Trainer;
        }

        public async Task<bool> UpdateUserHeightAndWeightAsync(ClientViewModel model, string username)
        {
            var user = await this.GetUserByUsernameAsync(username);

            user.Activity = model.Activity;
            user.Height = model.Height;
            user.Weight = model.Weight;

            this.context.Update(user);
            await this.context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteUserAsync(string username)
        {
            var userToDelete = await this.GetUserByUsernameAsync(username);

            if(userToDelete != null)
            {
                this.context.Articles.RemoveRange(this.context.Articles.Where(x => x.AuthorId == userToDelete.Id));
                if (this.context.UserRoles.Any(x => x.UserId == userToDelete.Id))
                {
                    this.context.UserRoles.Remove(this.context.UserRoles.FirstOrDefault(x => x.UserId == userToDelete.Id));
                    await this.context.SaveChangesAsync();
                }
                this.context.FitnessPlans.Remove(this.context.FitnessPlans.SingleOrDefault(x => x.OwnerId == userToDelete.Id));

                this.context.Users.Remove(userToDelete);

                await this.context.SaveChangesAsync();
            }

            return !this.userManager.Users.Contains(userToDelete);
        }
        public IEnumerable<ListedUserViewModel> GetAllUsers()
        {
            var users = this.userManager.Users;
            var userDtos = this.mapper.ProjectTo<ListedUserViewModel>(users).ToList();

            return userDtos;
        }

        public async Task<bool> UnsetTrainerAsync(string username, string trainerUsername)
        {
            var user = await this.GetUserByUsernameAsync(username);

            user.Trainer = null;
            user.TrainerId = null;

            this.context.Update(user);
            await this.context.SaveChangesAsync();

            return (await this.GetUserByUsernameAsync(username)).Trainer == null;
        }

        public int CalculateCalories(string username)
        {
            double result = 0;

            var realUser = this.context
                .Users
                .Include(user => user.FitnessPlan)
                .SingleOrDefault(user => user.UserName == username);

            if(realUser.Gender == Gender.Male)
            {
                result = (double)(66.4730 + (13.7516 * realUser.Weight) + (5.0033 * realUser.Height) - (6.7550 * realUser.Age));
            }
            else if(realUser.Gender == Gender.Female)
            {
                result = (double)(655.0955 + (9.5634 * realUser.Weight) + (1.8496 * realUser.Height) - (4.6756 * realUser.Age));
            }

            switch (realUser.Activity)
            {
                case ActityLevel.Zero:
                    result *= 1.2;
                    break;
                case ActityLevel.OneToThree:
                    result *= 1.375;
                    break;
                case ActityLevel.ThreeToFive:
                    result *= 1.55;
                    break;
                case ActityLevel.SixToServen:
                    result *= 1.725;
                    break;
                case ActityLevel.Beast:
                    result *= 1.9;
                    break;
            }

            realUser.FitnessPlan.CaloriesGoal = (int)result;

            return (int)result;
        }

        public async Task<ClientViewModel> GetUserWithFitnessPlanModelAsync(string username)
        {
            var user = await this.GetUserByUsernameAsync(username);

            var dto = this.mapper.Map<ClientViewModel>(user);
            return dto;
        }

        public bool SetFitnessPlanToUser(ClientViewModel model)
        {
            var user = this.GetUserByUsernameAsync(model.UserName).Result;

            var exercises = model.WorkoutViewModel.Exercises;
            var fitnessPlan = this.mapper.Map<FitnessPlan>(model);
            fitnessPlan.Workout.Exercises = exercises;
            fitnessPlan.WeekWorkoutDuration = model.WeekWorkoutDuration;
            user.FitnessPlan = fitnessPlan;

            this.context.Update(user);
            this.context.SaveChanges();

            return user.FitnessPlan != null;
        }

        public async Task<UserProfile> GetUserProfileModel(string username)
        {
            var userFromDb = this.mapper.Map<UserProfile>
                (await this.context.Users.FirstOrDefaultAsync(user => user.UserName == username));

            return userFromDb;
        }

        public void UpdateProfile(UserProfile model, string username)
        {
            var userFromDb = this.userManager.Users.FirstOrDefault(user => user.UserName == username);

            if(userFromDb != null)
            {
                userFromDb.Weight = model.Weight;
                userFromDb.Height = model.Height;
                userFromDb.Activity = model.Actity;
                userFromDb.Description = model.Description;
                userFromDb.Age = model.Age;

                this.context.Update(userFromDb);
                this.context.SaveChanges();
            }
        }
    }
}