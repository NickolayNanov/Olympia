namespace Olympia.Web.Areas.Administration.Controllers
{
    using AutoMapper;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Olympia.Common;
    using Olympia.Services.Contracts;
    using System.Threading.Tasks;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area(GlobalConstants.AdministrationArea)]
    public class AdministrationController : Controller
    {
        private readonly IUsersService usersService;
        private readonly IBlogService blogService;
        private readonly IMapper mapper;

        public AdministrationController(
            IUsersService usersService,
            IBlogService blogService,
            IMapper mapper)
        {
            this.usersService = usersService;
            this.blogService = blogService;
            this.mapper = mapper;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        public async Task<IActionResult> UsersAll()
        {
            var users = this.usersService.GetAllUsers();
            return this.View(users);
        }

        public async Task<IActionResult> ArticlesAll()
        {
            var articles = await this.blogService.GetAllArticlesAsync();

            return this.View(articles);
        }

        // TODO: GetAllItems
        public async Task<IActionResult> ItemsAll()
        {
            return this.View();
        }
       

        public async Task<IActionResult> DeleteUser(string username)
        {
            await this.usersService.DeleteUser(username);

            return this.Redirect(GlobalConstants.AdministrationUsers);
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
    }
}
