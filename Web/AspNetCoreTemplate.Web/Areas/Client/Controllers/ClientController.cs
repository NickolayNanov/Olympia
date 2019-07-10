namespace Olympia.Web.Areas.Client.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Olympia.Services.Contracts;
    using Olympia.Web.Areas.Client.Models;

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

            UsernamesAndTrainerNameModel model = new UsernamesAndTrainerNameModel
            {
                Trainers = trainers,
                TrainerName = string.Empty,
            };

            return this.View(model);
        }

        [HttpPost]
        public IActionResult ChooseTrainer(UsernamesAndTrainerNameModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.Redirect("/Client/Client/TrainersAll");
            }

            this.usersService.SetTrainer(model.TrainerName, this.User.Identity.Name);

            return this.View("SuccessfullSignInTrainer");
        }
    }
}