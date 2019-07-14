namespace Olympia.Web.Areas.Client.Controllers
{
    using AutoMapper;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Olympia.Common;
    using Olympia.Data.Models.BindingModels.Account;
    using Olympia.Services.Contracts;
    using Olympia.Web.Areas.Client.Models;
    using System.Threading.Tasks;

    [Area(GlobalConstants.ClientArea)]
    [Authorize(Roles = GlobalConstants.ClientRoleName)]
    public class ClientController : Controller
    {
        private readonly IUsersService usersService;
        private readonly IMapper mapper;

        public ClientController(IUsersService usersService, IMapper mapper)
        {
            this.usersService = usersService;
            this.mapper = mapper;
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

        public IActionResult BecomeTrainer(string username)
        {
            var user = this.usersService.GetUserByUsernameAsync(username).Result;
            var futureTrainer = this.mapper.Map<ClientToTrainerBindingModel>(user);

            return this.View(futureTrainer);
        }

        [HttpPost]
        public async Task<IActionResult> BecomeTrainer(ClientToTrainerBindingModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.Redirect($"Client/Client/BecomeTrainer?username={model.Username}");
            }

            var result = await this.usersService.BecomeTrainerAsync(model, this.User.Identity.Name);

            if (!result)
            {
                return this.Redirect($"Client/Client/BecomeTrainer?username={model.Username}");
            }

            return this.Redirect(RedirectRoutes.Index);
        }
    }
}