namespace Olympia.Web.Areas.Administration.Controllers
{
    using AutoMapper;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Olympia.Common;
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

        public IActionResult UsersAll()
        {
            var users = this.usersService.GetAllUsers();
            return this.View(users);
        }

        public async Task<IActionResult> ArticlesAll()
        {
            var articles = await this.blogService.GetAllArticlesAsync();

            return this.View(articles);
        }

        public IActionResult ItemsAll()
        {
            var items = this.fitnessService.GetAllItems();
            
            return this.View(items);
        }


        public async Task<IActionResult> DeleteUser(string username)
        {
            await this.usersService.DeleteUserAsync(username);
            var users = this.usersService.GetAllUsers();

            return this.View("UsersAll", users);
        }


        public async Task<IActionResult> DeleteArticle(int articleId)
        {
            await this.blogService.DeleteArticleByIdAsync(articleId);

            return this.Redirect(GlobalConstants.AdministrationArticles);
        }

        public IActionResult CreateArticle()
        {
            return this.View();
        }

        public IActionResult AddItem()
        {
            var names = this.fitnessService.GetAllSuppliers().Select(x => x.Name);

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
                return this.View(model);
            }

            var item = await this.shopService.CreateItemAsync(model);

            return this.View();
        }

        public IActionResult AddSupplier()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> AddSupplier(SupplierBindingModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var action = await this.fitnessService.AddSupplierAsync(model);

            if (!action)
            {
                this.ViewData["Errors"] = "A supplier with the same name has already been added or the name is invalid! Please try again.";
                return this.View(model);
            }

            var suppliers = this.fitnessService.GetAllSuppliers();

            return this.View("SuppliersAll", suppliers);
        }

        public IActionResult SuppliersAll()
        {
            var suppliers = this.shopService.GetAllSuppliers();

            return this.View(suppliers);
        }
    }
}
