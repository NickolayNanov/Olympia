namespace Olympia.Web.Areas.Client.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Olympia.Common;
    using Olympia.Services.Contracts;
    using Olympia.Web.Areas.Client.Models;
    using System.Threading.Tasks;

    [Area(GlobalConstants.ClientArea)]
    [Authorize(Roles = GlobalConstants.ClientRoleName)]
    public class ClientController : Controller
    {
        private readonly IUsersService usersService;

        public ClientController(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        public async Task<IActionResult> TrainersAll()
        {
            var trainers = await this.usersService.GetAllTrainersAsync();

            UsernamesAndTrainerNameModel model = new UsernamesAndTrainerNameModel
            {
                Trainers = trainers,
                TrainerName = string.Empty,
            };

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ChooseTrainer(UsernamesAndTrainerNameModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            await this.usersService.SetTrainerAsync(model.TrainerName, this.User.Identity.Name);

            return this.View("SuccessfullSignInTrainer");
        }
    }
}