﻿namespace Olympia.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Olympia.Common;
    using Olympia.Data;
    using Olympia.Data.Domain;
    using Olympia.Data.Models.BindingModels.Account;
    using Olympia.Data.Models.BindingModels.Client;
    using Olympia.Data.Models.ViewModels.AdminViewModels;
    using Olympia.Data.Models.ViewModels.BlogPartViewModels;
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
            await Task.Run(() =>
            {
                var trainerIds = this.context.UserRoles
                    .Where(ur => ur.RoleId == "e9a584d9-3bcd-439b-ac73-aa996070897e")
                    .Select(x => x.UserId)
                    .ToList();

                trainers = this.context
                    .Users
                    .Include(x => x.Articles)
                    .Where(id => trainerIds.Any(x => x == id.Id));
            });

            return trainers;
        }

        public async Task<IEnumerable<UserViewModel>> GetAllClientsByUserAsync(string trainerUsername)
        {
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
            OlympiaUser userFromDb = null;

            await Task.Run(() =>
            {
                userFromDb = this.context.Users.SingleOrDefault(user => user.UserName == username);
            });

            return userFromDb;
        }

        public async Task<bool> SetTrainerAsync(string trainerUsername, string clientUsername)
        {
            bool done = false;

            await Task.Run(async () =>
            {
                var trainer = await this.GetUserByUsernameAsync(trainerUsername);
                var client = await this.GetUserByUsernameAsync(clientUsername);

                trainer.Clients.Add(client);
                client.TrainerId = trainer.Id;

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

        public async Task<OlympiaUser> GetUsersTrainer(string username)
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

        public async Task<bool> UpdateUserHeightAndWeight(ClientHeightWeightBindingModel model, string username)
        {
            var user = await this.GetUserByUsernameAsync(username);

            user.Activity = model.Activity;
            user.Height = model.Height;
            user.Weight = model.Weight;

            this.context.Update(user);
            await this.context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteUser(string username)
        {
            var userToDelete = await this.GetUserByUsernameAsync(username);

            this.context.Articles.RemoveRange(this.context.Articles.Where(x => x.AuthorId == userToDelete.Id));
            this.context.UserRoles.Remove(this.context.UserRoles.FirstOrDefault(x => x.UserId == userToDelete.Id));
            this.context.Users.Remove(userToDelete);

            await this.context.SaveChangesAsync();

            return !this.userManager.Users.Contains(userToDelete);
        }

        public Task<FitnessPlan> CreateFitnessPlanAsync()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<ListedUserViewModel> GetAllUsers()
        {
            var users = this.userManager.Users;
            var userDtos = this.mapper.ProjectTo<ListedUserViewModel>(users).ToList();

            return userDtos;
        }
    }
}