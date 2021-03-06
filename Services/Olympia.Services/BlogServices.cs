﻿namespace Olympia.Services
{
    using AutoMapper;
    using Microsoft.EntityFrameworkCore;

    using Olympia.Data;
    using Olympia.Data.Domain;
    using Olympia.Data.Models.BindingModels.Blogs;
    using Olympia.Data.Models.ViewModels.BlogPartViewModels;
    using Olympia.Services.Contracts;
    using Olympia.Services.Utilities;

    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class BlogServices : IBlogService
    {
        private readonly OlympiaDbContext context;
        private readonly IMapper mapper;
        private readonly IUsersService usersService;

        public BlogServices(
            OlympiaDbContext context,
            IMapper mapper,
            IUsersService usersService)
        {
            this.context = context;
            this.mapper = mapper;
            this.usersService = usersService;
        }

        public async Task<IEnumerable<ArticleViewModel>> GetAllArticlesAsync()
        {
            IEnumerable<ArticleViewModel> articlesFromDb = new List<ArticleViewModel>();

            await Task.Run(() =>
            {
                var articles = this.context.Articles.Include(article => article.Author).OrderByDescending(x => x.TimesRead);
                articlesFromDb = this.mapper.ProjectTo<ArticleViewModel>(articles).ToList();
            });

            return articlesFromDb;
        }

        public async Task<IEnumerable<ArticleViewModel>> GetAllByUserIdAsync(string authorName)
        {
            IEnumerable<ArticleViewModel> articlesFromDb = new List<ArticleViewModel>();

            await Task.Run(() =>
            {
                articlesFromDb = this.mapper
                .ProjectTo<ArticleViewModel>(this.context.Articles
                                            .Include(article => article.Author)
                                            .Where(article => article.Author.UserName == authorName))
                                            .OrderByDescending(x => x.CreatedOn)
                                            .ToList();
            });

            return articlesFromDb;
        }

        public async Task<IEnumerable<ArticleViewModel>> GetTopThreeArticlesAsync()
        {
            IEnumerable<ArticleViewModel> mostPopularArticles = new List<ArticleViewModel>();

            await Task.Run(() =>
            {
                mostPopularArticles = this.mapper
                .ProjectTo<ArticleViewModel>(this.context.Articles
                .OrderByDescending(article => article.TimesRead)
                .Take(3))
                .ToList();
            });

            return mostPopularArticles;
        }

        public async Task<ArticleViewModel> CreateArticleAsync(CreateArticleBindingModel model, string usersName)
        {
            Article articleForDb = null;

            await Task.Run(async () =>
            {
                var user = await this.usersService.GetUserByUsernameAsync(usersName);
                var url = MyCloudinary.UploadImage(model.ImgUrl, model.Title);

                articleForDb = this.mapper.Map<Article>(model);
                articleForDb.ImgUrl = url ?? Constants.CloudinaryInvalidUrl;
                articleForDb.AuthorId = user.Id;

                this.context.Articles.Add(articleForDb);
                this.context.SaveChanges();
            });

            var articleToReturn = this.mapper.Map<ArticleViewModel>(articleForDb);

            return articleToReturn;
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

            if (!this.context.Articles.Select(x => x.Id).Contains(articleId))
            {
                return null;
            }

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

            if (!this.context.Articles.Select(x => x.Id).Contains(articleId))
            {
                return false;
            }

            await Task.Run(() =>
            {
                var articleFromDb = this.context
                .Articles
                .SingleOrDefault(article => article.Id == articleId);

                this.context.Articles.Remove(articleFromDb);
                this.context.SaveChanges();
                doesContain = this.context.Articles.Contains(articleFromDb);
            });

            return !doesContain;
        }
    }
}
