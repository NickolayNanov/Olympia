namespace Olympia.Services.Contracts
{
    using System.Collections.Generic;

    using Olympia.Data.Models.BindingModels.Blogs;
    using Olympia.Data.Models.ViewModels.BlogPartViewModels;

    public interface IBlogService
    {
        IEnumerable<ArticleViewModel> GetAllArticles();

        IEnumerable<ArticleViewModel> GetAllByUserId(string authorName);

        IEnumerable<ArticleViewModel> GetTopFiveArticles();

        int CreateArticle(CreateArticleBindingModel model, string usersName);

        ArticleViewModel GetArticleById(int articleId);

        bool DeleteArticleById(int articleId);

        ArticleViewModel GetArticleAndIncrementTimesRead(int articleId);
    }
}
