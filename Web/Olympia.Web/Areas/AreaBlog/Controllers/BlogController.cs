namespace Olympia.Web.Areas.Blog.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Olympia.Common;
    using Olympia.Data.Models.ViewModels.BlogPartViewModels;
    using Olympia.Services.Contracts;
    using System.Threading.Tasks;

    [Area(GlobalConstants.BlogArea)]
    [Authorize]
    public class BlogController : Controller
    {
        private readonly IBlogService blogService;

        public BlogController(IBlogService blogService)
        {
            this.blogService = blogService;
        }

        public async Task<IActionResult> ArticlesAll()
        {
            var articles = await this.blogService
                .GetAllArticlesAsync();

            return this.View(articles);
        }

        public async Task<IActionResult> ArticleDetails(int articleId)
        {
            var article = await this.blogService.GetArticleAndIncrementTimesReadAsync(articleId);

            return this.View(article);
        }

        public IActionResult CreateComment(CommentViewModel model)
        {
            return this.View();
        }
    }
}