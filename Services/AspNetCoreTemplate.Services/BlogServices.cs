namespace Olympia.Services
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    using AutoMapper;
    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Olympia.Data;
    using Olympia.Data.Domain;
    using Olympia.Data.Models.BindingModels.Blogs;
    using Olympia.Data.Models.ViewModels.BlogPartViewModels;
    using Olympia.Services.Contracts;
    using Olympia.Services.Utilities;

    public class BlogServices : IBlogService
    {
        private readonly OlympiaDbContext context;
        private readonly UserManager<OlympiaUser> userManager;
        private readonly IMapper mapper;
        private readonly IUsersService usersService;

        public BlogServices(
            OlympiaDbContext context,
            UserManager<OlympiaUser>
            userManager,
            IMapper mapper,
            IUsersService usersService)
        {
            this.context = context;
            this.userManager = userManager;
            this.mapper = mapper;
            this.usersService = usersService;
        }

        public IEnumerable<ArticleViewModel> GetAllArticles()
        {
            var articlesFromDb = this.context.Articles
                .Select(ar => this.mapper.Map<ArticleViewModel>(ar))
                .ToList();

            return articlesFromDb;
        }

        public IEnumerable<ArticleViewModel> GetAllByUserId(string authorName)
        {
            var articlesFromDb = this.context.Articles
                .Where(article => article.Author.UserName == authorName)
                .Select(ar => this.mapper.Map<ArticleViewModel>(ar))
                .ToList();

            return articlesFromDb;
        }

        public IEnumerable<ArticleViewModel> GetTopFiveArticles()
        {
            var mostPopularArticles = this.context.Articles
                .OrderByDescending(article => article.TimesRead)
                .Take(5)
                .Select(article => this.mapper.Map<ArticleViewModel>(article))
                .ToList();

            return mostPopularArticles;
        }

        public int CreateArticle(CreateArticleBindingModel model, string usersName)
        {
            var cloudinaryAccount = this.SetCloudinary();
            var user = this.usersService.GetUserByUsername(usersName);
            var url = this.UploadImage(cloudinaryAccount, model.ImgUrl, model.Title);

            var articleForDb = this.mapper.Map<Article>(model);
            articleForDb.ImgUrl = url ?? Constants.CloudinaryInvalidUrl;
            articleForDb.AuthorId = user.Id;

            this.context.Articles.Add(articleForDb);
            this.context.SaveChanges();

            var articleToReturn = this.mapper.Map<ArticleViewModel>(articleForDb);

            return articleToReturn.Id;
        }

        public ArticleViewModel GetArticleById(int articleId)
        {
            var articleFromDb = this.context
                .Articles
                .Include(article => article.Author)
                .ThenInclude(author => author.Articles)
                .SingleOrDefault(article => article.Id == articleId);

            var articleViewModel = this.mapper.Map<ArticleViewModel>(articleFromDb);
            return articleViewModel;
        }

        public ArticleViewModel GetArticleAndIncrementTimesRead(int articleId)
        {
            var articleFromDb = this.context
                .Articles
                .Include(article => article.Author)
                .ThenInclude(author => author.Articles)
                .SingleOrDefault(article => article.Id == articleId);

            articleFromDb.TimesRead++;
            this.context.Update(articleFromDb);
            this.context.SaveChanges();
            var articleViewModel = this.mapper.Map<ArticleViewModel>(articleFromDb);
            return articleViewModel;
        }

        public bool DeleteArticleById(int articleId)
        {
            var articleFromDb = this.context
                .Articles
                .SingleOrDefault(article => article.Id == articleId);

            this.context.Articles.Remove(articleFromDb);
            this.context.SaveChanges();

            return this.context.Articles.Contains(articleFromDb);
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

        private Cloudinary SetCloudinary()
        {
            CloudinaryDotNet.Account account = new CloudinaryDotNet.Account
            {
                Cloud = Constants.CloudinaryCloudName,
                ApiKey = Constants.CloudinaryApiKey,
                ApiSecret = Constants.CloudinaryApiSecret,
            };

            Cloudinary cloudinary = new Cloudinary(account);
            return cloudinary;
        }
    }
}
