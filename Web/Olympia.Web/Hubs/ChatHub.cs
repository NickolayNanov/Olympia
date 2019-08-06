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

        private async Task AddMessageToDbAsync(string message, OlympiaUser receiver, OlympiaUser sender)
        {
            var messageForDb = new Message { Content = message, ReceiverId = receiver.Id, SenderId = sender.Id };
            this.context.Messages.Add(messageForDb);
            sender.Messages.Add(new UserMessages() { SenderId = sender.Id, Message = messageForDb });
            await this.context.SaveChangesAsync();
        }

        private async Task LoadPreviousMessagesAsync(OlympiaUser sender, OlympiaUser receiver)
        {
            List<Message> senderMessages = sender.Messages.Select(x => x.Message).Where(x => x.SenderId == sender.Id).ToList();
            List<Message> receiverMessages = sender.Messages.Select(x => x.Message).Where(x => x.ReceiverId == receiver.Id).ToList();

            foreach (var msg in receiverMessages)
            {
                senderMessages.Add(msg);
            }

            senderMessages = senderMessages.OrderByDescending(x => x.CreatedOn).ToList();

            foreach (var message in senderMessages)
            {
                await this.Clients.User(sender.Id).SendAsync("ReceiveMessage", sender.UserName, message);
                await this.Clients.User(receiver.Id).SendAsync("ReceiveMessage", receiver.UserName, message);
            }
        }
    }
}
