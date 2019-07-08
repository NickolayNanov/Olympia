namespace Olympia.Web.Areas.Blog.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Olympia.Data.Models.ViewModels.BlogPartViewModels;
    using Olympia.Services.Contracts;

    [Area("Blog")]
    [Authorize]
    public class BlogController : Controller
    {
        private readonly IBlogService blogService;

        public BlogController(IBlogService blogService)
        {
            this.blogService = blogService;
        }

        public IActionResult ArticlesAll()
        {
            var articles = this.blogService.GetAllArticles();

            return this.View(articles);
        }

        public IActionResult ArticleDetails(int articleId)
        {
            var article = this.blogService.GetArticleAndIncrementTimesRead(articleId);

            return this.View(article);
        }

        
        public IActionResult CreateComment(CommentViewModel model)
        {
            return this.View();
        }
    }
}