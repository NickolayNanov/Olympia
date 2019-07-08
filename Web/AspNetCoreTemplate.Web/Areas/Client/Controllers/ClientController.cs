namespace Olympia.Web.Areas.Client.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Olympia.Services.Contracts;

    [Area("Client")]
    [Authorize(Roles = "Client")]
    public class ClientController : Controller
    {
        private readonly IUsersService usersService;

        public ClientController(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        public IActionResult TrainersAll()
        {
            var trainers = this.usersService.GetAllTrainers();

            return this.View(trainers);
        }
    }
}