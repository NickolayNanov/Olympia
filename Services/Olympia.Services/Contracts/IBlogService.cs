namespace Olympia.Services.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Olympia.Data.Models.BindingModels.Blogs;
    using Olympia.Data.Models.ViewModels.BlogPartViewModels;

    public interface IBlogService
    {
        Task<IEnumerable<ArticleViewModel>> GetAllArticlesAsync();

        Task<IEnumerable<ArticleViewModel>> GetAllByUserIdAsync(string authorName);

        Task<IEnumerable<ArticleViewModel>> GetTopThreeArticlesAsync();

        Task<ArticleViewModel> CreateArticleAsync(CreateArticleBindingModel model, string usersName);

        Task<ArticleViewModel> GetArticleByIdAsync(int articleId);

        Task<bool> DeleteArticleByIdAsync(int articleId);

        Task<ArticleViewModel> GetArticleAndIncrementTimesReadAsync(int articleId);
    }
}
