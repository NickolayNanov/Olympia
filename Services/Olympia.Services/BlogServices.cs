namespace Olympia.Services
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper;
    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Olympia.Common;
    using Olympia.Data;
    using Olympia.Data.Domain;
    using Olympia.Data.Models.BindingModels.Blogs;
    using Olympia.Data.Models.ViewModels.BlogPartViewModels;
    using Olympia.Services.Contracts;
    using Olympia.Services.Utilities;

    public class BlogServices : IBlogService
    {
        private readonly OlympiaDbContext context;
        private readonly IMapper mapper;
        private readonly IUsersService usersService;
        private readonly IConfiguration configuration;

        public BlogServices(
            OlympiaDbContext context,
            IMapper mapper,
            IUsersService usersService,
            IConfiguration configuration)
        {
            this.context = context;
            this.mapper = mapper;
            this.usersService = usersService;
            this.configuration = configuration;
        }

        public async Task<IEnumerable<ArticleViewModel>> GetAllArticlesAsync()
        {
            IEnumerable<ArticleViewModel> articlesFromDb = new List<ArticleViewModel>();

            await Task.Run(() =>
            {
                articlesFromDb = this.context.Articles
                .Select(ar => this.mapper.Map<ArticleViewModel>(ar))
                .ToList();
            });

            return articlesFromDb;
        }

        public async Task<IEnumerable<ArticleViewModel>> GetAllByUserIdAsync(string authorName)
        {
            IEnumerable<ArticleViewModel> articlesFromDb = new List<ArticleViewModel>();

            await Task.Run(() =>
            {
                articlesFromDb = this.context.Articles
                .Where(article => article.Author.UserName == authorName)
                .Select(ar => this.mapper.Map<ArticleViewModel>(ar))
                .ToList();
            });

            return articlesFromDb;
        }

        public async Task<IEnumerable<ArticleViewModel>> GetTopFiveArticlesAsync()
        {
            IEnumerable<ArticleViewModel> mostPopularArticles = new List<ArticleViewModel>();

            await Task.Run(() =>
            {
                mostPopularArticles = this.context.Articles
                .OrderByDescending(article => article.TimesRead)
                .Take(5)
                .Select(article => this.mapper.Map<ArticleViewModel>(article))
                .ToList();
            });

            return mostPopularArticles;
        }

        public async Task<int> CreateArticleAsync(CreateArticleBindingModel model, string usersName)
        {
            Article articleForDb = null;

            await Task.Run(async () =>
            {
                var cloudinaryAccount = this.SetCloudinary();
                var user = await this.usersService.GetUserByUsernameAsync(usersName);
                var url = this.UploadImage(cloudinaryAccount, model.ImgUrl, model.Title);

                articleForDb = this.mapper.Map<Article>(model);
                articleForDb.ImgUrl = url ?? Constants.CloudinaryInvalidUrl;
                articleForDb.AuthorId = user.Id;

                this.context.Articles.Add(articleForDb);
                this.context.SaveChanges();
            });

            var articleToReturn = this.mapper.Map<ArticleViewModel>(articleForDb);

            return articleToReturn.Id;
        }

        public async Task<ArticleViewModel> GetArticleByIdAsync(int articleId)
        {
            ArticleViewModel articleViewModel = null;

            await Task.Run(() =>
            {
                var articleFromDb = this.context
                .Articles
                .Include(article => article.Author)
                .ThenInclude(author => author.Articles)
                .SingleOrDefault(article => article.Id == articleId);

                articleViewModel = this.mapper.Map<ArticleViewModel>(articleFromDb);
            });

            return articleViewModel;
        }

        public async Task<ArticleViewModel> GetArticleAndIncrementTimesReadAsync(int articleId)
        {
            ArticleViewModel articleViewModel = null;

            await Task.Run(() =>
            {
                var articleFromDb = this.context
                .Articles
                .Include(article => article.Author)
                .ThenInclude(author => author.Articles)
                .SingleOrDefault(article => article.Id == articleId);

                articleFromDb.TimesRead++;
                this.context.Update(articleFromDb);
                this.context.SaveChanges();
                articleViewModel = this.mapper.Map<ArticleViewModel>(articleFromDb);
            });

            return articleViewModel;
        }

        public async Task<bool> DeleteArticleByIdAsync(int articleId)
        {
            bool doesContain = true;

            await Task.Run(() =>
            {
                var articleFromDb = this.context
                .Articles
                .SingleOrDefault(article => article.Id == articleId);

                this.context.Articles.Remove(articleFromDb);
                this.context.SaveChanges();
                doesContain = this.context.Articles.Contains(articleFromDb);
            });

            return doesContain;
        }

        private string UploadImage(Cloudinary cloudinary, IFormFile fileform, string articleTitle)
        {
            if (fileform == null)
            {
                return null;
            }

            byte[] articleImg;

            using (var memoryStream = new MemoryStream())
            {
                fileform.CopyTo(memoryStream);
                articleImg = memoryStream.ToArray();
            }

            ImageUploadResult uploadResult;

            using (var ms = new MemoryStream(articleImg))
            {
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(articleTitle, ms),
                    Transformation = new Transformation(),
                };

                uploadResult = cloudinary.Upload(uploadParams);
            }

            return uploadResult.SecureUri.AbsoluteUri;
        }

        // TODO: export to json
        private Cloudinary SetCloudinary()
        {
            CloudinaryDotNet.Account account = new CloudinaryDotNet.Account
            {

                Cloud = Constants.CloudinaryCloudName,
                ApiKey = Constants.CloudinaryApiSecret,
                ApiSecret = Constants.CloudinaryApiSecret,
            };

            Cloudinary cloudinary = new Cloudinary(account);
            return cloudinary;
        }
    }
}
