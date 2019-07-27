namespace Olympia.Web.Areas.Client.Controllers
{
    using AutoMapper;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Olympia.Common;
    using Olympia.Data.Domain;
    using Olympia.Data.Models.BindingModels.Account;
    using Olympia.Data.Models.BindingModels.Client;
    using Olympia.Services.Contracts;
    using Olympia.Web.Areas.Client.Models;
    using System.Linq;
    using System.Threading.Tasks;

    [Area(GlobalConstants.ClientArea)]
    [Authorize(Roles = GlobalConstants.ClientRoleName)]
    public class ClientController : Controller
    {
        private readonly IUsersService usersService;
        private readonly IMapper mapper;
        private readonly IFitnessService fitnessService;
        private readonly SignInManager<OlympiaUser> signInManager;

        public ClientController(
            IUsersService usersService,
            IMapper mapper,
            IFitnessService fitnessService,
            SignInManager<OlympiaUser> signInManager)
        {
            this.usersService = usersService;
            this.mapper = mapper;
            this.fitnessService = fitnessService;
            this.signInManager = signInManager;
        }

        public async Task<IActionResult> TrainersAll()
        {
            var trainers = await this.usersService.GetAllTrainersAsync();

            UsernamesAndTrainerNameModel model = new UsernamesAndTrainerNameModel
            {
                Trainers = trainers,
                TrainerName = string.Empty,
            };

            if(trainers.Count() == 0)
            {
                this.ViewData["Errors"] = "There are no trainers yet";
            }

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ChooseTrainer(UsernamesAndTrainerNameModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var currnetUser = await this.usersService.GetUserByUsernameAsync(this.User.Identity.Name);

            if (currnetUser.Weight == null || currnetUser.Height == null)
            {
                return this.View("EditWeightHeight");
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
                return this.Redirect($"AreaClient/Client/BecomeTrainer?username={model.Username}");
            }

            var result = await this.usersService.BecomeTrainerAsync(model, this.User.Identity.Name);

            if (!result)
            {
                return this.Redirect($"AreaClient/Client/BecomeTrainer?username={model.Username}");
            }

            await this.signInManager.SignOutAsync();

            return this.Redirect(GlobalConstants.Index);
        }

        public async Task<IActionResult> MyTrainer(string username)
        {
            var trainer = await this.usersService.GetUsersTrainerAsync(username);

            return this.View(trainer);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateWeightHeight(ClientViewModel model)
        {
          
            await this.usersService.UpdateUserHeightAndWeightAsync(model, this.User.Identity.Name);

            return this.Redirect(GlobalConstants.ClientTrainersAll);
        }

        public async Task<IActionResult> LeaveTrainer(string trainerUsername)
        {
            await this.usersService.UnsetTrainerAsync(this.User.Identity.Name, trainerUsername);

            return this.Redirect(GlobalConstants.Index);
        }

     
        public async Task<IActionResult> MyFitnessPlan(string username)
        {
            var fitnessPlan = await this.fitnessService.GetFitnessPlanByUsername(username);

            return this.View(fitnessPlan);
        }
    }
}