namespace Olympia.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using Olympia.Services.Contracts;

    public class BlogsController : Controller
    {
        private readonly IBlogService blogService;

        public BlogsController(IBlogService blogService)
        {
            this.blogService = blogService;
        }

        [Authorize]
        public IActionResult ArticlesAll()
        {
            var articles = this.blogService.GetAllArticles();

            return this.View(articles);
        }

        [Authorize]
        public IActionResult ArticleDetails(int articleId)
        {
            var article = this.blogService.GetArticleAndIncrementTimesRead(articleId);

            return this.View(article);
        }
    }
}