namespace Olympia.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Olympia.Data;
    using Olympia.Data.Domain;
    using Olympia.Services.Contracts;

    public class UsersService : IUsersService
    {
        private readonly OlympiaDbContext context;
        private readonly UserManager<OlympiaUser> userManager;

        public UsersService(OlympiaDbContext context, UserManager<OlympiaUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        public async Task<IEnumerable<OlympiaUser>> GetAllTrainers()
        {
            var tr = await this.userManager.GetUsersInRoleAsync("Trainer");

            var trainersIds =
                this.context.UserRoles.Where(x => x.RoleId == "ee916697-b589-4d1f-b656-784fb2baaf09")
                .Select(x => x.UserId)
                .ToList();

            var trainers = this.context.Users.Where(user => trainersIds.Contains(user.Id)).ToList();

            return trainers;
        }

        public IEnumerable<OlympiaUser> GetAllClientsByUser(string trainerUsername)
        {
            var clientsIds =
                this.context.UserRoles.Where(x => x.RoleId == "74ed18f3-d5b0-4b62-9a22-b9db77e473c9")
                .Select(x => x.UserId)
                .ToList();

            var clients = this.context
                .Users
                .Where(user => clientsIds.Contains(user.Id) && user.Trainer.UserName == trainerUsername).ToList();

            return clients;
        }

        public OlympiaUser GetUserByUsername(string username)
        {

            var userFromDb = this.context.Users.SingleOrDefault(user => user.UserName == username);
            return userFromDb;
        }
    }
}
