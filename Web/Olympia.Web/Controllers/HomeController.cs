﻿namespace Olympia.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using Olympia.Common;
    using Olympia.Data.Models.ViewModels.Home;
    using Olympia.Services.Contracts;

    using System.Linq;
    using System.Threading.Tasks;

    public class HomeController : Controller
    {
        private readonly IBlogService blogsService;
        private readonly IUsersService usersService;
        private readonly IShopService shopService;

        public HomeController(
            IBlogService blogsService,
            IUsersService usersService,
            IShopService shopService)
        {
            this.blogsService = blogsService;
            this.usersService = usersService;
            this.shopService = shopService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var articles = await this.blogsService.GetTopThreeArticlesAsync();
            var items = await this.shopService.GetTopFiveItemsAsync();

            IndexModel model = new IndexModel();
            model.Articles = articles;
            model.Items = items;

            if (this.User.IsInRole(GlobalConstants.TrainerRoleName))
            {
                model.ClientNames = (await this.usersService
                    .GetUserByUsernameAsync(this.User.Identity.Name)).Clients.Select(client => client.UserName);
            }
            else if (this.User.IsInRole(GlobalConstants.ClientRoleName))
            {
                model.TrainerName = (await this.usersService
                    .GetUserByUsernameAsync(this.User.Identity.Name)).Trainer?.UserName;
            }

            return this.View(model);
        }

        [AllowAnonymous]
        public IActionResult FAQ()
        {
            return this.View();
        }

        [AllowAnonymous]
        public IActionResult Error()
        {
            return this.View();
        }

        [Authorize]
        public async Task<IActionResult> Chat()
        {
            IndexModel model = await this.usersService.GetIndexModelAsync(this.User);
            return this.View(model);
        }
    }
}
