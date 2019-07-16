namespace Olympia.Web.Areas.Trainer.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Olympia.Common;
    using Olympia.Data.Models.BindingModels.Blogs;
    using Olympia.Data.Models.BindingModels.Client;
    using Olympia.Services.Contracts;

    [Area(GlobalConstants.TrainerArea)]
    [Authorize(Roles =  GlobalConstants.TrainerAdministratorRoleName)]
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

        public IActionResult CalculateCalories(ClientViewModel user)
        {
            var calories = this.usersService.CalculateCalories(user.UserName);
            user.Calories = calories;

            return this.View("CreateFitnessPlan", user);
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

        [Authorize(Roles = "Administrator")]
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

        public IActionResult ChooseWorkout()
        {
            return this.View(new WorkoutBindingModel());
        }

        [HttpPost]
        public IActionResult FileterWorkouts(WorkoutBindingModel model)
         {
            if (!this.ModelState.IsValid)
            {
                return this.View("ChooseWorkout", model);
            }

            var workouts = this.fitnessService.GetWorkouts(model);
            return this.View("Workouts", workouts);
        }
    }
}
