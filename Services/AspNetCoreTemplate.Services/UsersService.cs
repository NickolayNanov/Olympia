namespace Olympia.Services
{
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Olympia.Data;
    using Olympia.Data.Domain;
    using Olympia.Services.Contracts;

    public class UsersService : IUsersService
    {
        private readonly OlympiaDbContext context;
        private readonly UserManager<OlympiaUser> userManager;
        private readonly RoleManager<OlympiaUserRole> roleManager;

        public UsersService(OlympiaDbContext context, UserManager<OlympiaUser> userManager, RoleManager<OlympiaUserRole> roleManager)
        {
            this.context = context;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public IEnumerable<OlympiaUser> GetAllTrainers()
        {
            // var trainers = await this.userManager.GetUsersInRoleAsync(GlobalConstants.TrainerRoleName);

            var trainerIds = this.context.UserRoles
                .Where(ur => ur.RoleId == "e9e63982-0610-450d-9253-e66db344561b")
                .Select(x => x.UserId)
                .ToList();

            var trainers = this.context
                .Users
                .Include(x => x.Articles)
                .Where(id => trainerIds.Any(x => x == id.Id));

            return trainers;
        }

        public IEnumerable<OlympiaUser> GetAllClientsByUser(string trainerUsername)
        {
            var clients = this.context.Users.Where(x => x.Trainer.UserName == trainerUsername);

            return clients;
        }

        public OlympiaUser GetUserByUsername(string username)
        {
            var userFromDb = this.context.Users.SingleOrDefault(user => user.UserName == username);
            return userFromDb;
        }

        public bool SetTrainer(string trainerUsername, string clientUsername)
        {
            var trainer = this.GetUserByUsername(trainerUsername);
            var client = this.GetUserByUsername(clientUsername);

            trainer.Clients.Add(client);
            client.TrainerId = trainer.Id;

            this.context.Update(trainer);
            this.context.Update(client);
            this.context.SaveChanges();

            return client.TrainerId == trainer.Id;
        }
    }
}
