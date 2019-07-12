namespace Olympia.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Olympia.Data;
    using Olympia.Data.Domain;
    using Olympia.Services.Contracts;

    public class UsersService : IUsersService
    {
        private readonly OlympiaDbContext context;

        public UsersService(
            OlympiaDbContext context)
        {
            this.context = context;
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
    }
}
