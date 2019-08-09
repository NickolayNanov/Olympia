namespace Olympia.Web.Hubs
{
    using Microsoft.AspNetCore.SignalR;
    using Microsoft.EntityFrameworkCore;
    using Olympia.Data;
    using Olympia.Data.Domain;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class ChatHub : Hub
    {
        private readonly OlympiaDbContext context;
        private bool isFirstTime = true;

        public ChatHub(OlympiaDbContext context)
        {
            this.context = context;
        }

        public async Task SendMessage(string destuser, string message)
        {
            if (string.IsNullOrEmpty(message) || string.IsNullOrWhiteSpace(message))
            {
                return;
            }

            string senderUsername = this.Context.GetHttpContext().User.Identity.Name;

            var sender = this.context.Users
                .Include(x => x.Messages)
                .SingleOrDefault(x => x.UserName == senderUsername);

            var receiver = this.context.Users
                .Include(x => x.Messages)
                .SingleOrDefault(x => x.UserName == destuser);

            await AddMessageToDbAsync(message, sender, receiver);
            await this.Clients.User(sender.Id).SendAsync("ReceiveMessage", sender.UserName, message);
            await this.Clients.User(receiver.Id).SendAsync("ReceiveMessage", sender.UserName, message);
        }

        public async Task LoadPreviousMessages(string destuser)
        {
            var sender = this.context.Users
                .Include(x => x.Messages)
                .SingleOrDefault(x => x.UserName == this.Context.User.Identity.Name);

            var receiver = this.context.Users
                .Include(x => x.Messages)
                .SingleOrDefault(x => x.UserName == destuser);

            await this.LoadPreviousMessagesAsync(sender, receiver);
        }

        private async Task LoadPreviousMessagesAsync(OlympiaUser sender, OlympiaUser receiver)
        {
            List<Message> messages = new List<Message>(sender.Messages.Where(x => x.ReceiverId == receiver.Id));
            messages = messages.Concat(receiver.Messages.Where(x => x.ReceiverId == sender.Id)).OrderByDescending(x => x.Id).ToList();

            foreach (var message in messages.Take(5).OrderBy(x => x.Id))
            {
                await this.Clients.User(message.SenderId).SendAsync("LoadMessage", message.Sender.UserName, message.Content);
                await this.Clients.User(message.ReceiverId).SendAsync("LoadMessage", message.Sender.UserName, message.Content);
            }
        }

        private async Task AddMessageToDbAsync(string message, OlympiaUser sender, OlympiaUser receiver)
        {
            var currentMessage = new Message() { Content = message, ReceiverId = receiver.Id };
            sender.Messages.Add(currentMessage);
            this.context.Update(sender);
            await this.context.SaveChangesAsync();
        }
    }
}
