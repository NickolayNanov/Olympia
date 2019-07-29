namespace Olympia.Web.Areas.Trainer.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using Olympia.Common;
    using Olympia.Data.Models.BindingModels.Blogs;
    using Olympia.Data.Models.BindingModels.Client;
    using Olympia.Data.Models.ViewModels.Fitness;
    using Olympia.Services.Contracts;

    using System.Linq;
    using System.Threading.Tasks;

    [Area(GlobalConstants.TrainerArea)]
    [Authorize(Roles = GlobalConstants.TrainerAdministratorRoleName)]
    public class TrainerController : Controller
    {
        private readonly IBlogService blogService;
        private readonly IUsersService usersService;
        private readonly IFitnessService fitnessService;

        public TrainerController(
            IBlogService blogService,
            IUsersService usersService,
            IFitnessService fitnessService)
        {
            this.blogService = blogService;
            this.usersService = usersService;
            this.fitnessService = fitnessService;
        }

        public async Task<IActionResult> ClientsAll()
        {
            var clients = await this.usersService.GetAllClientsByUserAsync(this.User.Identity.Name);

            if (clients.Count() == 0)
            {
                this.ViewData["Errors"] = "You do not have any clients yet";
            }

            return this.View(clients);
        }

        public async Task<IActionResult> MyArticles()
        {
            var currentUserArticles = await this.blogService.GetAllByUserIdAsync(this.User.Identity.Name);
            return this.View(currentUserArticles);
        }

        public IActionResult CreateArticle()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateArticle(CreateArticleBindingModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            await this.blogService.CreateArticleAsync(model, this.User.Identity.Name);
            return this.Redirect(GlobalConstants.TrainerMyArticles);
        }

        public async Task<IActionResult> DeleteArticle(int articleId)
        {
            await this.blogService.DeleteArticleByIdAsync(articleId);
            return this.Redirect(GlobalConstants.TrainerMyArticles);
        }

        public async Task<IActionResult> ClientDetails(string username)
        {
            var user = await this.usersService.GetUserByUsernameAsync(username);
            return this.View(user);
        }

        public async Task<IActionResult> CreateFitnessPlan(string username)
        {
            var model = await this.usersService.GetUserWithFitnessPlanModelAsync(username);
            return this.View(model);
        }

        public IActionResult ChooseWorkout(ClientViewModel model)
        {
            return this.View(model);
        }

        [HttpPost]
        public IActionResult FileterWorkouts(ClientViewModel model)
        {
            if (model.WorkoutInputModel.Duration == 0 ||
                model.WorkoutInputModel.WorkoutDifficulty == 0 ||
                model.WorkoutInputModel.WorkoutType == 0)
            {
                return this.View("ChooseWorkout", model);
            }

            model.Workouts = this.fitnessService.GetWorkouts(model.WorkoutInputModel);
            model.WeekWorkoutDuration = model.WorkoutInputModel.Duration;

            return this.View("Workouts", model);
        }

        public async Task<IActionResult> CalculateCalories(ClientViewModel user)
        {
            var calories = await this.usersService.CalculateCaloriesAsync(user.UserName);
            user.Calories = calories;

            return this.View("CreateFitnessPlan", user);
        }

        public IActionResult AssignFitnessPlan(ClientViewModel user, int workoutId)
        {
            WorkoutViewModel workout = this.fitnessService.GetWorkoutById(workoutId);
            user.WorkoutViewModel = workout;

            return this.View("CreateFitnessPlan", user);
        }

        [HttpPost]
        public async Task<IActionResult> SetFitnessPlan(ClientViewModel model, int workoutId)
        {
            model.WorkoutViewModel = this.fitnessService.GetWorkoutById(workoutId);

            if (model.Calories == 0 || model.WorkoutViewModel == null)
            {
                model.WorkoutViewModel = new WorkoutViewModel() { Name = "", ImgUrl = "" };
                this.ViewData["Errors"] = "In order to assign a fitness plan both calories and workout must be filled!";
                return this.View("CreateFitnessPlan", model);
            }

            await this.usersService.SetFitnessPlanToUserAsync(model);
            var clients = await this.usersService.GetAllClientsByUserAsync(this.User.Identity.Name);

            return this.View("ClientsAll", clients);
        }
    }
}
