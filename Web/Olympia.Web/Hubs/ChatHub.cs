namespace Olympia.Web.Hubs
{
    using Microsoft.AspNetCore.SignalR;
    using Olympia.Services.Contracts;
    using System.Threading.Tasks;


    public class ChatHub : Hub
    {
        private readonly IUsersService usersService;

        public ChatHub(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        public async Task SendMessage(string destuser, string message)
        {
            if (string.IsNullOrEmpty(message) || string.IsNullOrWhiteSpace(message))
            {
                return;
            }

            string currentUser = this.Context.GetHttpContext().User.Identity.Name;

            var userFromDb = await this.usersService.GetUserByUsernameAsync(destuser);
            var currentUserFormDb = await this.usersService.GetUserByUsernameAsync(currentUser);

            if (userFromDb == null)
            {
                throw new System.Exception();
            }

            await this.Clients.User(userFromDb.Id).SendAsync("ReceiveMessage", currentUser, message);
            await this.Clients.User(currentUserFormDb.Id).SendAsync("ReceiveMessage", currentUser, message);
        }
    }
}
