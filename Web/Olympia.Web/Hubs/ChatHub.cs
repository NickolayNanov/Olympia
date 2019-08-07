namespace Olympia.Web.Hubs
{
    using Microsoft.AspNetCore.SignalR;
    using Olympia.Data;
    using Olympia.Data.Domain;
    using Olympia.Services.Contracts;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;


    public class ChatHub : Hub
    {
        private readonly IUsersService usersService;
        private readonly OlympiaDbContext context;

        public ChatHub(IUsersService usersService,
            OlympiaDbContext context)
        {
            this.usersService = usersService;
            this.context = context;
        }

        public async Task SendMessage(string destuser, string message)
        {
            if (string.IsNullOrEmpty(message) || string.IsNullOrWhiteSpace(message))
            {
                return;
            }

            string senderUsername = this.Context.GetHttpContext().User.Identity.Name;

            var userFromDb = await this.usersService.GetUserByUsernameAsync(destuser);
            var currentUserFormDb = await this.usersService.GetUserByUsernameAsync(senderUsername);

            await this.Clients.User(userFromDb.Id).SendAsync("ReceiveMessage", senderUsername, message);
            await this.Clients.User(currentUserFormDb.Id).SendAsync("ReceiveMessage", senderUsername, message);
        }
    }
}
