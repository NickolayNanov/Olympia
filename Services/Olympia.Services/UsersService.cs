namespace Olympia.Services
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
    using Olympia.Services.Contracts;

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

            await Task.Run(() =>
            {
                var trainerIds = this.context.UserRoles
                    .Where(ur => ur.RoleId == "e9e63982-0610-450d-9253-e66db344561b")
                    .Select(x => x.UserId)
                    .ToList();

                trainers = this.context
                    .Users
                    .Include(x => x.Articles)
                    .Where(id => trainerIds.Any(x => x == id.Id));
            });

            return trainers;
        }

        public async Task<IEnumerable<OlympiaUser>> GetAllClientsByUserAsync(string trainerUsername)
        {
            IEnumerable<OlympiaUser> clients = new List<OlympiaUser>();

            await Task.Run(() =>
            {
                clients = this.context.Users.Where(x => x.Trainer.UserName == trainerUsername);
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

            await this.userManager.UpdateSecurityStampAsync(realUser);          

            var roleHasChanged = await this.userManager.AddToRoleAsync(realUser, GlobalConstants.TrainerRoleName);

            if (!roleHasChanged.Succeeded)
            {
                return false;
            }
         
            await this.userManager.UpdateAsync(realUser);            

            return true;
        }
    }
}