﻿namespace Olympia.Web.Areas.Administration.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using Olympia.Common;
    using Olympia.Data.Models.BindingModels.Blogs;
    using Olympia.Data.Models.BindingModels.Shop;
    using Olympia.Services.Contracts;

    using System.Linq;
    using System.Threading.Tasks;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area(GlobalConstants.AdministrationArea)]
    public class AdministrationController : Controller
    {
        private readonly IUsersService usersService;
        private readonly IBlogService blogService;
        private readonly IShopService shopService;
        private readonly IFitnessService fitnessService;

        public AdministrationController(
            IUsersService usersService,
            IBlogService blogService,
            IShopService shopService,
            IFitnessService fitnessService)
        {
            this.usersService = usersService;
            this.blogService = blogService;
            this.shopService = shopService;
            this.fitnessService = fitnessService;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        public IActionResult CreateArticle()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateArticle(CreateArticleBindingModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            await this.blogService.CreateArticleAsync(model, this.User.Identity.Name);
            var articles = await this.blogService.GetAllArticlesAsync();
            return this.View("ArticlesAll", articles);
        }

        public async Task<IActionResult> UsersAll()
        {
            var users = await this.usersService.GetAllUsersAsync();
            return this.View(users);
        }

        public async Task<IActionResult> ArticlesAll()
        {
            var articles = await this.blogService.GetAllArticlesAsync();
            return this.View(articles);
        }

        public async Task<IActionResult> ItemsAll()
        {
            var items = await this.fitnessService.GetAllItemsAsync();
            return this.View(items);
        }

        public async Task<IActionResult> DeleteUser(string username)
        {
            await this.usersService.DeleteUserAsync(username);
            var users = await this.usersService.GetAllUsersAsync();

            return this.View("UsersAll", users);
        }

        public async Task<IActionResult> DeleteArticle(int articleId)
        {
            await this.blogService.DeleteArticleByIdAsync(articleId);
            return this.Redirect(GlobalConstants.AdministrationArticles);
        }

        public async Task<IActionResult> AddItem()
        {
            var names = (await this.fitnessService.GetAllSuppliersAsync()).Select(x => x.Name);
            return this.View(new ItemBindingModel() { SupplierNames = names });
        }

        public IActionResult CreateItem()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateItem(ItemBindingModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View("AddItem", model);
            }

            await this.shopService.CreateItemAsync(model);
            return this.View("Success");
        }

        public IActionResult AddSupplier()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddSupplier(SupplierBindingModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var action = await this.fitnessService.AddSupplierAsync(model);

            if (!action)
            {
                this.ViewData["Errors"] = GlobalConstants.SameSupplierErrorMessage;
                return this.View(model);
            }

            var suppliers = await this.fitnessService.GetAllSuppliersAsync();
            return this.View("SuppliersAll", suppliers);
        }

        public async Task<IActionResult> SuppliersAll()
        {
            var suppliers = await this.shopService.GetAllSuppliersAsync();
            return this.View(suppliers);
        }

        public async Task<IActionResult> DeleteItem(int itemId)
        {
            await this.shopService.DeleteItemAsync(itemId);
            var items = await this.fitnessService.GetAllItemsAsync();

            return this.View("ItemsAll", items);
        }
        public async Task<IActionResult> UserDetails(string username)
        {
            var user = await this.usersService.GetUserByUsernameAsync(username);
            return this.View(user);
        }
    }
}
