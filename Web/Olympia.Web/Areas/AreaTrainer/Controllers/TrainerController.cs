namespace Olympia.Web.Areas.Trainer.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Olympia.Common;
    using Olympia.Data.Models.BindingModels.Blogs;
    using Olympia.Data.Models.BindingModels.Client;
    using Olympia.Data.Models.ViewModels.Fitness;
    using Olympia.Services.Contracts;

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
            var clients = await this.usersService
                .GetAllClientsByUserAsync(this.User.Identity.Name);

            return this.View(clients);
        }

        public async Task<IActionResult> MyArticles()
        {
            var currentUserArticles = await this.blogService
                .GetAllByUserIdAsync(this.User.Identity.Name);

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
            var model = await this.usersService.GetFitnessPlanModelAsync(username);

            return this.View(model);
        }

        public IActionResult ChooseWorkout(ClientViewModel model)
        {
            return this.View(model);
        }

        [HttpPost]
        public IActionResult FileterWorkouts(ClientViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View("ChooseWorkout", model);
            }

            model.Workouts = this.fitnessService.GetWorkouts(model.WorkoutInputModel);
            return this.View("Workouts", model);
        }

        public IActionResult CalculateCalories(ClientViewModel user)
        {
            var calories = this.usersService.CalculateCalories(user.UserName);
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
            this.usersService.SetFitnessPlanToUser(model);

            var clients = await this.usersService.GetAllClientsByUserAsync(this.User.Identity.Name);

            return this.View("ClientsAll", clients);
        }
    }
}
