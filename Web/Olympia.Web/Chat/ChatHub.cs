namespace Olympia.Web.Chat
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

        public Task JoinRoom(string roomName)
        {
            return this.Groups.AddToGroupAsync(this.Context.ConnectionId, roomName);
        }
        public Task Leave(string roomName)
        {
            return this.Groups.RemoveFromGroupAsync(this.Context.ConnectionId, roomName);
        }

        public async Task SendMessage(string destuser, string message)
        {
            string currentUser = this.Context.GetHttpContext().User.Identity.Name;

            var userFromDb = await this.usersService.GetUserByUsernameAsync(destuser);

            if(userFromDb == null)
            {
                throw new System.Exception();
            }
            
            await this.Clients.All.SendAsync("ReceiveMessage", currentUser, destuser, message);
        }
    }
}
