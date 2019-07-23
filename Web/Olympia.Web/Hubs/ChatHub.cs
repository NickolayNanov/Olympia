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
            string currentUser = this.Context.GetHttpContext().User.Identity.Name;
            string connection = this.Context.ConnectionId;
            var userFromDb = await this.usersService.GetUserByUsernameAsync(destuser);


            var user = this.Clients.User(userFromDb.Id);

            if (userFromDb == null)
            {
                throw new System.Exception();
            }
            
            await this.Clients.All.SendAsync("ReceiveMessage", currentUser, destuser, message);
        }
    }
}
