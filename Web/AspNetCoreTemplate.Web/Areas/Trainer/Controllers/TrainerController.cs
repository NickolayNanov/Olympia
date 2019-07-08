namespace Olympia.Web.Areas.Trainer.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Olympia.Data.Models.BindingModels.Blogs;
    using Olympia.Services.Contracts;

    [Area("Trainer")]
    [Authorize(Roles = "Trainer")]
    public class TrainerController : Controller
    {
        private readonly IBlogService blogService;
        private readonly IUsersService usersService;

        public TrainerController(IBlogService blogService, IUsersService usersService)
        {
            this.blogService = blogService;
            this.usersService = usersService;
        }

        public IActionResult ClientsAll()
        {
            var clients = this.usersService.GetAllClientsByUser(this.User.Identity.Name);

            return this.View(clients);
        }

        public IActionResult MyArticles()
        {
            var currentUserArticles = this.blogService.GetAllByUserId(this.User.Identity.Name);

            return this.View(currentUserArticles);
        }

        public IActionResult CreateArticle()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult CreateArticle(CreateArticleBindingModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.Redirect("/Users/CreateArticle");
            }

            this.blogService.CreateArticle(model, this.User.Identity.Name);

            return this.Redirect("/Trainer/Trainer/MyArticles");
        }

        public IActionResult DeleteArticle(int articleId)
        {
            this.blogService.DeleteArticleById(articleId);

            return this.Redirect("/Trainer/Trainer/MyArticles");
        }
    }
}