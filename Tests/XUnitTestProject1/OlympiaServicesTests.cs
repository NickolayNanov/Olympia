namespace Olympia.Services.Tests
{
    using AutoMapper;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using Olympia.Data;
    using Olympia.Data.Domain;
    using Olympia.Data.Domain.Enums;
    using Olympia.Data.Models.BindingModels.Account;
    using Olympia.Data.Models.BindingModels.Client;
    using Olympia.Data.Models.ViewModels.BlogPartViewModels;
    using Olympia.Data.Models.ViewModels.Fitness;
    using Olympia.Data.Seeding;
    using Olympia.Services.Contracts;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Xunit;

    public class OlympiaServicesTests
    {
        private OlympiaDbContext context;
        private IMapper mockMapper;
        private IUsersService mockUserService;
        private IBlogService mockedBlogService;
        private IAccountsServices accountService;
        private IFitnessService fitnessService;

        public OlympiaServicesTests()
        {

        }

        private void InitiateInMemmoryDbForBlog()
        {
            DbContextOptionsBuilder<OlympiaDbContext> optionsBuilder = new DbContextOptionsBuilder<OlympiaDbContext>();
            optionsBuilder.UseInMemoryDatabase("testDb");

            this.context = new OlympiaDbContext(optionsBuilder.Options);
            this.mockMapper = new Mock<IMapper>().Object;
            this.mockUserService = new Mock<UsersService>(context, mockMapper, null, null).Object;
            this.mockedBlogService = new Mock<BlogServices>(this.context, mockMapper, mockUserService).Object;

            new DataSeeder(this.context);
        }

        private void InitiateInMemmoryDbForAccount()
        {
            DbContextOptionsBuilder<OlympiaDbContext> optionsBuilder = new DbContextOptionsBuilder<OlympiaDbContext>();
            optionsBuilder.UseInMemoryDatabase("testDb");

            this.context = new OlympiaDbContext(optionsBuilder.Options);
            this.mockMapper = new Mock<IMapper>().Object;

            UserManager<OlympiaUser> usermanager = this.GetMockUserManager();


            var _contextAccessor = new Mock<IHttpContextAccessor>();
            var _userPrincipalFactory = new Mock<IUserClaimsPrincipalFactory<OlympiaUser>>();
            var signInManager = new Mock<SignInManager<OlympiaUser>>(usermanager,
                           _contextAccessor.Object, _userPrincipalFactory.Object, null, null, null).Object;

            this.accountService = new Mock<AccountsServices>(
                mockMapper,
                usermanager,
                signInManager,
                this.context).Object;

            new DataSeeder(this.context);
        }

        private void InitiateInMemmoryDbForFitness()
        {
            DbContextOptionsBuilder<OlympiaDbContext> optionsBuilder = new DbContextOptionsBuilder<OlympiaDbContext>();
            optionsBuilder.UseInMemoryDatabase("testDb");

            this.context = new OlympiaDbContext(optionsBuilder.Options);
            this.mockMapper = new Mock<IMapper>().Object;
            this.mockUserService = new Mock<UsersService>(context, mockMapper, null, null).Object;
            this.fitnessService = new Mock<FitnessService>(this.context, mockMapper, mockUserService).Object;

            new DataSeeder(this.context);
        }

        private UserManager<OlympiaUser> GetMockUserManager()
        {
            var userStoreMock = new Mock<IUserStore<OlympiaUser>>();
            return new Mock<UserManager<OlympiaUser>>(
                userStoreMock.Object, null, null, null, null, null, null, null, null).Object;
        }

        [Fact]
        public async Task ArticleShouldBeDeleted()
        {
            this.InitiateInMemmoryDbForBlog();

            var aricle = await this.mockedBlogService.DeleteArticleByIdAsync(1);
            Assert.False(aricle);
        }

        [Fact]
        public async Task GetAllArticlesShouldReturnAllArticles()
        {
            this.InitiateInMemmoryDbForBlog();

            var user = this.context.Users.SingleOrDefault(x => x.UserName == "Pesho");
            var expected = this.mockMapper.ProjectTo<ArticleViewModel>(new List<Article>()
                {
                    new Article
                    {
                        Author = this.context.Users.SingleOrDefault(x => x.UserName == "God"),
                        Title = "How to get stronger today",
                        Content = "orem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.",
                        ImgUrl = "https://res.cloudinary.com/olympiacloudinary/image/upload/v1563984082/eojtgytpm1a0i1q0katk.jpg"
                    },
                    new Article
                    {
                        Author = this.context.Users.SingleOrDefault(x => x.UserName == "God"),
                        Title = "How to get stronger today",
                        Content = "orem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.",
                        ImgUrl = "https://res.cloudinary.com/olympiacloudinary/image/upload/v1563984082/eojtgytpm1a0i1q0katk.jpg"
                    },
                    new Article
                    {
                        Author = user,
                        Title = "How to get stronger today",
                        Content = "orem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.",
                        ImgUrl = "https://res.cloudinary.com/olympiacloudinary/image/upload/v1563984082/eojtgytpm1a0i1q0katk.jpg"
                    },
                    new Article
                    {
                        Author = user,
                        Title = "How to get stronger today",
                        Content = "orem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.",
                        ImgUrl = "https://res.cloudinary.com/olympiacloudinary/image/upload/v1563984082/eojtgytpm1a0i1q0katk.jpg"
                    },
                    new Article
                    {
                        Author = user,
                        Title = "How to get stronger today",
                        Content = "orem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.",
                        ImgUrl = "https://res.cloudinary.com/olympiacloudinary/image/upload/v1563984082/eojtgytpm1a0i1q0katk.jpg"
                    },
                    new Article
                    {
                        Author = user,
                        Title = "How to get stronger today",
                        Content = "orem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.",
                        ImgUrl = "https://res.cloudinary.com/olympiacloudinary/image/upload/v1563984082/eojtgytpm1a0i1q0katk.jpg"
                    },
                    new Article
                    {
                        Author = user,
                        Title = "How to get stronger today",
                        Content = "orem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.",
                        ImgUrl = "https://res.cloudinary.com/olympiacloudinary/image/upload/v1563984082/eojtgytpm1a0i1q0katk.jpg"
                    }

                }.AsQueryable()).AsEnumerable();
            var actual = await this.mockedBlogService.GetAllArticlesAsync();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task GetAllArticleByUserNameShuouldReturnAll()
        {
            this.InitiateInMemmoryDbForBlog();

            var user = this.context.Users.SingleOrDefault(x => x.UserName == "Pesho");
            var expected = this.mockMapper.ProjectTo<ArticleViewModel>(new List<Article>()
                {
                    new Article
                    {
                        Author = this.context.Users.SingleOrDefault(x => x.UserName == "God"),
                        Title = "How to get stronger today",
                        Content = "orem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.",
                        ImgUrl = "https://res.cloudinary.com/olympiacloudinary/image/upload/v1563984082/eojtgytpm1a0i1q0katk.jpg"
                    },
                    new Article
                    {
                        Author = this.context.Users.SingleOrDefault(x => x.UserName == "God"),
                        Title = "How to get stronger today",
                        Content = "orem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.",
                        ImgUrl = "https://res.cloudinary.com/olympiacloudinary/image/upload/v1563984082/eojtgytpm1a0i1q0katk.jpg"
                    },
                    new Article
                    {
                        Author = user,
                        Title = "How to get stronger today",
                        Content = "orem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.",
                        ImgUrl = "https://res.cloudinary.com/olympiacloudinary/image/upload/v1563984082/eojtgytpm1a0i1q0katk.jpg"
                    },
                    new Article
                    {
                        Author = user,
                        Title = "How to get stronger today",
                        Content = "orem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.",
                        ImgUrl = "https://res.cloudinary.com/olympiacloudinary/image/upload/v1563984082/eojtgytpm1a0i1q0katk.jpg"
                    },
                    new Article
                    {
                        Author = user,
                        Title = "How to get stronger today",
                        Content = "orem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.",
                        ImgUrl = "https://res.cloudinary.com/olympiacloudinary/image/upload/v1563984082/eojtgytpm1a0i1q0katk.jpg"
                    },
                    new Article
                    {
                        Author = user,
                        Title = "How to get stronger today",
                        Content = "orem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.",
                        ImgUrl = "https://res.cloudinary.com/olympiacloudinary/image/upload/v1563984082/eojtgytpm1a0i1q0katk.jpg"
                    },
                    new Article
                    {
                        Author = user,
                        Title = "How to get stronger today",
                        Content = "orem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.",
                        ImgUrl = "https://res.cloudinary.com/olympiacloudinary/image/upload/v1563984082/eojtgytpm1a0i1q0katk.jpg"
                    }

                }.AsQueryable()).AsEnumerable();
            var actual = await this.mockedBlogService.GetAllByUserIdAsync("Pesho");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task IncrementCountShouldIncreaseTimesRead()
        {
            this.InitiateInMemmoryDbForBlog();

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            var mockedMapper = new Mock<Mapper>(mappingConfig).Object;
            this.mockedBlogService = new Mock<BlogServices>(this.context, mockedMapper, mockUserService).Object;

            int expected = 1;
            var article = await this.mockedBlogService.GetArticleAndIncrementTimesReadAsync(2);

            Assert.True(expected == article.TimesRead);
        }

        [Fact]
        public async Task GetArticleByIdAsyncShouldReturnTheCorrectOne()
        {
            this.InitiateInMemmoryDbForBlog();

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            this.mockMapper = new Mock<Mapper>(mappingConfig).Object;
            this.mockedBlogService = new Mock<BlogServices>(this.context, mockMapper, mockUserService).Object;

            var expected = new ArticleViewModel()
            {
                Id = 1,
                Author = this.context.Users.SingleOrDefault(x => x.UserName == "God"),
                Title = "How to get stronger today",
                Content = "orem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.",
                ImgUrl = "https://res.cloudinary.com/olympiacloudinary/image/upload/v1563984082/eojtgytpm1a0i1q0katk.jpg"
            };
            var actual = await this.mockedBlogService.GetArticleByIdAsync(1);

            Assert.Equal(expected.Id, actual.Id);
        }

        [Fact]
        public async Task GetTopThreeArticlesAsyncShouldReturnCorrectOnes()
        {
            this.InitiateInMemmoryDbForBlog();

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            var user = this.context.Users.SingleOrDefault(x => x.UserName == "Pesho");

            var expected = this.mockMapper
                .ProjectTo<ArticleViewModel>(new List<Article>()
            {
                    new Article
                    {
                        Id = 5,
                        Author = user,
                        Title = "How to get stronger today",
                        Content = "orem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.",
                        ImgUrl = "https://res.cloudinary.com/olympiacloudinary/image/upload/v1563984082/eojtgytpm1a0i1q0katk.jpg",
                        TimesRead = 4
                    },
                    new Article
                    {
                        Id = 6,
                        Author = user,
                        Title = "How to get stronger today",
                        Content = "orem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.",
                        ImgUrl = "https://res.cloudinary.com/olympiacloudinary/image/upload/v1563984082/eojtgytpm1a0i1q0katk.jpg",
                        TimesRead = 4
                    },
                    new Article
                    {
                        Id = 7,
                        Author = user,
                        Title = "How to get stronger today",
                        Content = "orem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.",
                        ImgUrl = "https://res.cloudinary.com/olympiacloudinary/image/upload/v1563984082/eojtgytpm1a0i1q0katk.jpg",
                        TimesRead = 4
                    }
            }.AsQueryable())
                .AsEnumerable()
                .Select(x => x.Id);

            var actual = (await this.mockedBlogService.GetTopThreeArticlesAsync()).Select(x => x.Id);


            Assert.Equal(expected, actual);
        }

        //TODO: Fix the tests...
        [Fact]
        public async Task LoginUserAsyncShouldReturnRealUser()
        {
            this.InitiateInMemmoryDbForAccount();

            UserLoginBindingModel model = new UserLoginBindingModel
            {
                UserName = "Pesho",
                Password = "123123"
            };

            var user = await this.accountService.LoginUserAsync(model);

            Assert.Equal(model.UserName, user.UserName);
        }

        [Fact]
        public async Task RegisterUserAsyncShouldReturnRealUser()
        {
            this.InitiateInMemmoryDbForAccount();

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            var mockedMapper = new Mock<Mapper>(mappingConfig).Object;
            UserManager<OlympiaUser> usermanager = this.GetMockUserManager();

            var _contextAccessor = new Mock<IHttpContextAccessor>();
            var _userPrincipalFactory = new Mock<IUserClaimsPrincipalFactory<OlympiaUser>>();
            var signInManager = new Mock<SignInManager<OlympiaUser>>(usermanager,
                           _contextAccessor.Object, _userPrincipalFactory.Object, null, null, null).Object;

            var accountsServices = new Mock<AccountsServices>(
                mockedMapper,
                usermanager,
                signInManager,
                this.context).Object;

            UserRegisterBingingModel model = new UserRegisterBingingModel
            {
                Username = "grisho",
                Age = 16,
                FullName = "Nikola",
                Email = "ala@asdas.bg",
                Gender = Gender.Male,
                Password = "123123",
                ConfirmPassword = "123123",
            };

            var expected = model.Username;
            var actual = await accountsServices.RegisterUserAsync(model);

            Assert.Equal(expected, actual.UserName);
        }

        [Fact]
        public async Task GetWorkoutsShouldReturnAllWorkouts()
        {
            this.InitiateInMemmoryDbForFitness();

            WorkoutBindingModel model = new WorkoutBindingModel()
            {
                WorkoutDifficulty = WorkoutDifficulty.Beginners,
                Duration = WeekWorkoutDuration.Four,
                WorkoutType = WorkoutType.Strength
            };

            var expected = this.mockMapper.ProjectTo<WorkoutViewModel>(this.context.Workouts
                .Where(x => x.WorkoutDifficulty == model.WorkoutDifficulty &&
                            x.WorkoutType == model.WorkoutType)).AsEnumerable();

            var actual = this.mockMapper
                .ProjectTo<WorkoutViewModel>
                (this.fitnessService.GetWorkouts(model)
                    .AsQueryable())
                .AsEnumerable();

            Assert.Equal(expected, actual);
        }
    }
}
