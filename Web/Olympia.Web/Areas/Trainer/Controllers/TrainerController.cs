﻿namespace Olympia.Web.Areas.Trainer.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Olympia.Common;
    using Olympia.Data.Models.BindingModels.Blogs;
    using Olympia.Services.Contracts;

    [Area(GlobalConstants.TrainerArea)]
    [Authorize(Roles = GlobalConstants.TrainerRoleName)]
    public class TrainerController : Controller
    {
        private readonly IBlogService blogService;
        private readonly IUsersService usersService;

        public TrainerController(IBlogService blogService, IUsersService usersService)
        {
            this.blogService = blogService;
            this.usersService = usersService;
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

            return this.Redirect(RedirectRoutes.TrainerMyArticles);
        }

        public async Task<IActionResult> DeleteArticle(int articleId)
        {
            await this.blogService.DeleteArticleByIdAsync(articleId);

            return this.Redirect(RedirectRoutes.TrainerMyArticles);
        }
    }
}