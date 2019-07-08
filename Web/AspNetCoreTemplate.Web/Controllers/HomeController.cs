namespace Olympia.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using Olympia.Services.Contracts;

    public class HomeController : Controller
    {
        private readonly IBlogService blogsService;

        public HomeController(IBlogService blogsService)
        {
            this.blogsService = blogsService;
        }

        public IActionResult Index()
        {
            var articles = this.blogsService.GetTopFiveArticles();

            return this.View(articles);
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View();
        }
    }
}
