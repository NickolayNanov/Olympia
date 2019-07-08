namespace Olympia.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using Olympia.Data.Models.BindingModels.Blogs;
    using Olympia.Services.Contracts;

    public class UsersController : Controller
    {
        private readonly IBlogService blogService;
        private readonly IUsersService usersService;

        public UsersController(IBlogService blogService, IUsersService usersService)
        {
            this.blogService = blogService;
            this.usersService = usersService;
        }

        [Authorize]
        public IActionResult Profile()
        {
            return this.View();
        }

        // [Authorize(Roles = "Trainer")]
        // public IActionResult ClientsAll()
        // {
        //     return this.View();
        // }

        // [Authorize(Roles = "Trainer")]
        // public IActionResult MyArticles()
        // {
        //     var currentUserArticles = this.blogService.GetAllByUserId(this.User.Identity.Name);

        //     return this.View(currentUserArticles);
        // }

        // [Authorize(Roles = "Trainer")]
        // public IActionResult CreateArticle()
        // {
        //     return this.View();
        // }

        // [HttpPost]
        // public IActionResult CreateArticle(CreateArticleBindingModel model)
        // {
        //     if (!this.ModelState.IsValid)
        //     {
        //         return this.Redirect("/Users/CreateArticle");
        //     }

        //     this.blogService.CreateArticle(model, this.User.Identity.Name);

        //     return this.Redirect("/Users/MyArticles");
        // }

        // public IActionResult DeleteArticle(int articleId)
        // {
        //     this.blogService.DeleteArticleById(articleId);

        //     return this.Redirect("/Users/MyArticles");
        // }
    }
}