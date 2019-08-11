namespace Olympia.Services.Tests
{
    using AutoMapper;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using Moq;

    using Olympia.Data;
    using Olympia.Data.Domain;
    using Olympia.Data.Domain.Enums;
    using Olympia.Data.Models.BindingModels.Account;
    using Olympia.Data.Models.BindingModels.Client;
    using Olympia.Data.Models.BindingModels.Shop;
    using Olympia.Data.Models.ViewModels.BlogPartViewModels;
    using Olympia.Data.Models.ViewModels.Fitness;
    using Olympia.Data.Models.ViewModels.Home;
    using Olympia.Data.Seeding;

    using Olympia.Services.Contracts;
    using System;
    using System.Collections.Generic;
    using System.IO;
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

        private UserManager<OlympiaUser> userManager;

        private void InitiateInMemmoryDbForBlog()
        {
            DbContextOptionsBuilder<OlympiaDbContext> optionsBuilder = new DbContextOptionsBuilder<OlympiaDbContext>();
            optionsBuilder.UseInMemoryDatabase("testDb");

            this.context = new OlympiaDbContext(optionsBuilder.Options);
            this.mockMapper = new Mock<IMapper>().Object;
            UserManager<OlympiaUser> usermanager = this.TestUserManager<OlympiaUser>();
            this.mockUserService = new Mock<UsersService>(context, mockMapper, usermanager).Object;
            this.mockedBlogService = new Mock<BlogServices>(this.context, mockMapper, mockUserService).Object;

            new DataSeeder(this.context);
        }

        private void InitiateInMemmoryDbForAccount()
        {
            DbContextOptionsBuilder<OlympiaDbContext> optionsBuilder = new DbContextOptionsBuilder<OlympiaDbContext>();
            optionsBuilder.UseInMemoryDatabase("testDb");

            this.context = new OlympiaDbContext(optionsBuilder.Options);
            this.mockMapper = new Mock<IMapper>().Object;

            UserManager<OlympiaUser> usermanager = this.TestUserManager<OlympiaUser>();

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
            UserManager<OlympiaUser> usermanager = this.TestUserManager<OlympiaUser>();

            this.mockUserService = new Mock<UsersService>(context, mockMapper, usermanager).Object;
            this.fitnessService = new Mock<FitnessService>(this.context, mockMapper, mockUserService).Object;

            new DataSeeder(this.context);
        }

        private void InitiateInMemmoryDbForUsers()
        {
            DbContextOptionsBuilder<OlympiaDbContext> optionsBuilder = new DbContextOptionsBuilder<OlympiaDbContext>();
            optionsBuilder.UseInMemoryDatabase("testDb");

            this.context = new OlympiaDbContext(optionsBuilder.Options);
            this.mockMapper = new Mock<IMapper>().Object;

            this.userManager = this.TestUserManager<OlympiaUser>();

            this.mockUserService = new Mock<UsersService>(this.context, this.mockMapper, this.userManager).Object;

            new DataSeeder(this.context);
        }

        private void InitiateInMemmoryDbForShop()
        {
            DbContextOptionsBuilder<OlympiaDbContext> optionsBuilder = new DbContextOptionsBuilder<OlympiaDbContext>();
            optionsBuilder.UseInMemoryDatabase("testDb");
            this.context = new OlympiaDbContext(optionsBuilder.Options);

            new DataSeeder(this.context);
        }

        private UserManager<TUser> TestUserManager<TUser>(IUserStore<TUser> store = null) where TUser : class
        {
            store = store ?? new Mock<IUserStore<TUser>>().Object;
            var options = new Mock<IOptions<IdentityOptions>>();
            var idOptions = new IdentityOptions();
            idOptions.Lockout.AllowedForNewUsers = false;
            options.Setup(o => o.Value).Returns(idOptions);
            var userValidators = new List<IUserValidator<TUser>>();
            var validator = new Mock<IUserValidator<TUser>>();
            userValidators.Add(validator.Object);
            var pwdValidators = new List<PasswordValidator<TUser>>();
            pwdValidators.Add(new PasswordValidator<TUser>());
            var userManager = new UserManager<TUser>(store, options.Object, new PasswordHasher<TUser>(),
                userValidators, pwdValidators, new UpperInvariantLookupNormalizer(),
                new IdentityErrorDescriber(), null,
                new Mock<ILogger<UserManager<TUser>>>().Object);
            validator.Setup(v => v.ValidateAsync(userManager, It.IsAny<TUser>()))
                .Returns(Task.FromResult(IdentityResult.Success)).Verifiable();
            return userManager;
        }

        #region Blog Service Tests
        [Fact]
        public async Task ArticleShouldBeDeleted()
        {
            this.InitiateInMemmoryDbForBlog();

            var aricle = await this.mockedBlogService.DeleteArticleByIdAsync(13);
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

        [Theory]
        [InlineData("Pesho")]
        [InlineData("asd")]
        [InlineData("")]
        [InlineData(null)]
        public async Task GetAllArticleByUserNameShuouldReturnAll(string username)
        {
            this.InitiateInMemmoryDbForBlog();

            var user = this.context.Users.SingleOrDefault(x => x.UserName == username);
            var expected = this.mockMapper.ProjectTo<ArticleViewModel>(new List<Article>()
                {
                    new Article
                    {
                        Author = this.context.Users.SingleOrDefault(x => x.UserName == username),
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
            var actual = await this.mockedBlogService.GetAllByUserIdAsync(username);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(2)]
        [InlineData(-1)]
        [InlineData(int.MaxValue)]
        public async Task IncrementCountShouldIncreaseTimesRead(int id)
        {
            this.InitiateInMemmoryDbForBlog();

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            var mockedMapper = new Mock<Mapper>(mappingConfig).Object;
            this.mockedBlogService = new Mock<BlogServices>(this.context, mockedMapper, mockUserService).Object;

            int expected = 1;
            var article = await this.mockedBlogService.GetArticleAndIncrementTimesReadAsync(id);

            if (article == null)
            {
                Assert.Null(article);
                return;
            }

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
        #endregion      

        #region Fitness Service Tests
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
                ((await this.fitnessService.GetWorkoutsAsync(model))
                    .AsQueryable())
                .AsEnumerable();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task GetWorkoutByIdShouldReturnTheCorrectOne()
        {
            this.InitiateInMemmoryDbForFitness();

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            var mockedMapper = new Mock<Mapper>(mappingConfig).Object;
            UserManager<OlympiaUser> usermanager = this.TestUserManager<OlympiaUser>();

            var mockedUserService = new Mock<UsersService>(context, mockMapper, usermanager).Object;
            var fitnessService = new Mock<FitnessService>(this.context, mockedMapper, mockedUserService).Object;

            var expected = 1;
            var article = (await fitnessService.GetWorkoutByIdAsync(1)).Id;

            Assert.Equal(expected, article);
        }

        [Fact]
        public async Task AddSupplierAsyncShouldWorkCorrect()
        {
            this.InitiateInMemmoryDbForFitness();

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            var mockedMapper = new Mock<Mapper>(mappingConfig).Object;
            var mockedUserService = new Mock<UsersService>(context, mockMapper, this.userManager).Object;
            var fitnessService = new Mock<FitnessService>(this.context, mockedMapper, mockedUserService).Object;


            SupplierBindingModel supplier = new SupplierBindingModel
            {
                Name = "TestSupplier",
                Description = "sad"
            };

            var result = await fitnessService.AddSupplierAsync(supplier);

            Assert.True(result);
        }

        [Fact]
        public async Task GetAllSuppliersShouldReturnAllSuppliers()
        {
            this.InitiateInMemmoryDbForFitness();

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            var mockedMapper = new Mock<Mapper>(mappingConfig).Object;
            UserManager<OlympiaUser> usermanager = this.TestUserManager<OlympiaUser>();

            var mockedUserService = new Mock<UsersService>(context, mockMapper, userManager).Object;
            var fitnessService = new Mock<FitnessService>(this.context, mockedMapper, mockedUserService).Object;

            var suppliers = new List<Supplier>()
                {
                    new Supplier
                    {
                        Name = "GymBeam",
                        Description = "The best supplier ever."
                    },
                    new Supplier
                    {
                        Name = "Bodybuilding",
                        Description = "The best supplier ever."
                    },
                    new Supplier
                    {
                        Name = "MaxPower",
                        Description = "The best supplier ever."
                    },
                    new Supplier
                    {
                        Name = "Olympia",
                        Description = "The best supplier ever."
                    },
                    new Supplier
                    {
                        Name = "IronIide",
                        Description = "The best supplier ever."
                    },
                    new Supplier
                    {
                        Name = "Thunder",
                        Description = "The best supplier ever."
                    }
                }.Select(x => x.Name);
            var actual = (await fitnessService.GetAllSuppliersAsync()).Select(x => x.Name).ToList();

            int index = 0;

            foreach (var supplierName in suppliers)
            {
                Assert.Equal(supplierName, actual[index++]);
            }
        }

        [Fact]
        public async Task GetAllItemsShouldReturnAllItems()
        {
            this.InitiateInMemmoryDbForFitness();

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            var mockedMapper = new Mock<Mapper>(mappingConfig).Object;
            UserManager<OlympiaUser> usermanager = this.TestUserManager<OlympiaUser>();


            var mockedUserService = new Mock<UsersService>(context, mockMapper, userManager).Object;
            var fitnessService = new Mock<FitnessService>(this.context, mockedMapper, mockedUserService).Object;

            var items = new List<Item>()
                {
                    new Item("Protein Powder", 19.99m){ ImgUrl = "https://res.cloudinary.com/olympiacloudinary/image/upload/v1563565242/prhnarsdb3hny5h82wb3.jpg", Description = "The perfect item for you"},
                    new Item("Protein Powder", 19.99m){ ImgUrl = "https://res.cloudinary.com/olympiacloudinary/image/upload/v1563565242/prhnarsdb3hny5h82wb3.jpg", Description = "The perfect item for you"},
                    new Item("Protein Powder", 19.99m){ ImgUrl = "https://res.cloudinary.com/olympiacloudinary/image/upload/v1563565242/prhnarsdb3hny5h82wb3.jpg", Description = "The perfect item for you"},
                    new Item("Protein Powder", 19.99m){ ImgUrl = "https://res.cloudinary.com/olympiacloudinary/image/upload/v1563565242/prhnarsdb3hny5h82wb3.jpg", Description = "The perfect item for you"},
                    new Item("Protein Powder", 19.99m){ ImgUrl = "https://res.cloudinary.com/olympiacloudinary/image/upload/v1563565242/prhnarsdb3hny5h82wb3.jpg", Description = "The perfect item for you"},
                    new Item("Protein Powder", 19.99m){ ImgUrl = "https://res.cloudinary.com/olympiacloudinary/image/upload/v1563565242/prhnarsdb3hny5h82wb3.jpg", Description = "The perfect item for you"},
                    new Item("Protein Powder", 19.99m){ ImgUrl = "https://res.cloudinary.com/olympiacloudinary/image/upload/v1563565242/prhnarsdb3hny5h82wb3.jpg", Description = "The perfect item for you"},
                    new Item("Protein Powder", 19.99m){ ImgUrl = "https://res.cloudinary.com/olympiacloudinary/image/upload/v1563565242/prhnarsdb3hny5h82wb3.jpg", Description = "The perfect item for you"},
                    new Item("Protein Powder", 19.99m){ ImgUrl = "https://res.cloudinary.com/olympiacloudinary/image/upload/v1563565242/prhnarsdb3hny5h82wb3.jpg", Description = "The perfect item for you"},
                    new Item("Protein Powder", 19.99m){ ImgUrl = "https://res.cloudinary.com/olympiacloudinary/image/upload/v1563565242/prhnarsdb3hny5h82wb3.jpg", Description = "The perfect item for you"},
                    new Item("Protein Powder", 19.99m){ ImgUrl = "https://res.cloudinary.com/olympiacloudinary/image/upload/v1563565242/prhnarsdb3hny5h82wb3.jpg", Description = "The perfect item for you"},
                }.Select(x => x.Name);
            var actual = (await fitnessService.GetAllItemsAsync()).Items.Select(x => x.Name).ToList();

            int index = 0;

            foreach (var supplierName in items)
            {
                Assert.Equal(supplierName, actual[index++]);
            }
        }

        [Fact]
        public async Task GetFitnessPlanByUsernameShouldReturntCorrectOne()
        {
            this.InitiateInMemmoryDbForFitness();

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            var mockedMapper = new Mock<Mapper>(mappingConfig).Object;
            UserManager<OlympiaUser> usermanager = this.TestUserManager<OlympiaUser>();

            var mockedUserService = new Mock<UsersService>(context, mockedMapper, usermanager).Object;
            var fitnessService = new Mock<FitnessService>(this.context, mockedMapper, mockedUserService).Object;

            var user = this.context.Users.Add(new OlympiaUser("Niki", "asd@asd.bg", "NikiNiki")).Entity;
            var fitnessPlan = this.context.FitnessPlans.Add(new FitnessPlan() { OwnerId = user.Id, Owner = user }).Entity;
            this.context.SaveChanges();

            var actual = await fitnessService.GetFitnessPlanByUsernameAsync("Niki");

            Assert.Equal(fitnessPlan.Id, actual.Id);
        }
        #endregion

        #region User Service Tests
        [Theory]
        [InlineData("Pesho")]
        [InlineData(null)]
        public async Task GetUserProfileModelShouldReturnCorrectOne(string username)
        {
            this.InitiateInMemmoryDbForUsers();

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            var mockedMapper = new Mock<Mapper>(mappingConfig).Object;
            var mockUserService = new Mock<UsersService>(this.context, mockedMapper, this.userManager).Object;

            var result = await mockUserService.GetUserProfileModelAsync(username);

            Assert.True(result?.UserName == username);
        }

        [Fact]
        public async Task GetUserProfileModelShouldReturnNull()
        {
            this.InitiateInMemmoryDbForUsers();

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            var mockedMapper = new Mock<Mapper>(mappingConfig).Object;
            var mockUserService = new Mock<UsersService>(this.context, mockedMapper, this.userManager).Object;

            var result = await mockUserService.GetUserProfileModelAsync("");

            Assert.Null(result);
        }

        [Fact]
        public async Task GetUserByUsernameAsyncShouldReturnCorrectOne()
        {
            this.InitiateInMemmoryDbForUsers();

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            var mockedMapper = new Mock<Mapper>(mappingConfig).Object;
            UserManager<OlympiaUser> usermanager = this.TestUserManager<OlympiaUser>();

            var mockUserService = new Mock<UsersService>(
                this.context,
                mockedMapper,
                usermanager).Object;

            var user = await mockUserService.GetUserByUsernameAsync("Pesho");

            Assert.NotNull(user);
        }

        [Fact]
        public async Task GetAllClientsByUserAsyncShouldReturnAll()
        {
            this.InitiateInMemmoryDbForUsers();

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            var mockedMapper = new Mock<Mapper>(mappingConfig).Object;

            UserManager<OlympiaUser> usermanager = this.TestUserManager<OlympiaUser>();

            var mockUserService = new Mock<UsersService>(
                this.context,
                mockedMapper,
                userManager).Object;

            var pesho = this.context.Users.SingleOrDefault(x => x.UserName == "Pesho");
            var mesho = this.context.Users.SingleOrDefault(x => x.UserName == "Mesho");
            var gesho = this.context.Users.SingleOrDefault(x => x.UserName == "Gesho");

            mesho.Trainer = pesho;
            gesho.Trainer = pesho;

            context.Update(mesho);
            context.Update(gesho);
            await context.SaveChangesAsync();

            var expected = 2;
            var actual = (await mockUserService.GetAllClientsByUserAsync(pesho.UserName)).Count();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task SetTrainerAsyncShouldSetTrainer()
        {
            this.InitiateInMemmoryDbForUsers();

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            var mockedMapper = new Mock<Mapper>(mappingConfig).Object;

            UserManager<OlympiaUser> usermanager = this.TestUserManager<OlympiaUser>();

            var userService = new Mock<UsersService>(
                this.context,
                mockedMapper,
                userManager).Object;

            var result = await userService.SetTrainerAsync("Pesho", "Mesho");

            Assert.True(result);
        }

        [Fact]
        public async Task SetTrainerAsyncShouldReturnFalse()
        {
            this.InitiateInMemmoryDbForUsers();

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            var mockedMapper = new Mock<Mapper>(mappingConfig).Object;

            UserManager<OlympiaUser> usermanager = this.TestUserManager<OlympiaUser>();

            var userService = new Mock<UsersService>(
                this.context,
                mockedMapper,
                userManager).Object;

            var result = await userService.SetTrainerAsync("", "Mesho");

            Assert.False(result);
        }

        [Fact]
        public async Task SetTrainerAsyncShouldReturnFalseNullInput()
        {
            this.InitiateInMemmoryDbForUsers();

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            var mockedMapper = new Mock<Mapper>(mappingConfig).Object;

            UserManager<OlympiaUser> usermanager = this.TestUserManager<OlympiaUser>();

            var userService = new Mock<UsersService>(
                this.context,
                mockedMapper,
                userManager).Object;

            var result = await userService.SetTrainerAsync("", "");

            Assert.False(result);
        }

        [Fact]
        public async Task SetTrainerAsyncShouldReturnFalseNullInputInvalidOne()
        {
            this.InitiateInMemmoryDbForUsers();

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            var mockedMapper = new Mock<Mapper>(mappingConfig).Object;

            UserManager<OlympiaUser> usermanager = this.TestUserManager<OlympiaUser>();

            var userService = new Mock<UsersService>(
                this.context,
                mockedMapper,
                userManager).Object;

            var result = await userService.SetTrainerAsync("", "Mesho");

            Assert.False(result);
        }

        [Fact]
        public async Task GetUsersTrainerAsyncShouldReturnCorrectly()
        {
            this.InitiateInMemmoryDbForUsers();

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            var mockedMapper = new Mock<Mapper>(mappingConfig).Object;

            UserManager<OlympiaUser> usermanager = this.TestUserManager<OlympiaUser>();

            var userService = new Mock<UsersService>(
                this.context,
                mockedMapper,
                usermanager).Object;

            var pesho = this.context.Users.SingleOrDefault(x => x.UserName == "Pesho");
            var mesho = this.context.Users.SingleOrDefault(x => x.UserName == "Mesho").Trainer = pesho;

            this.context.Update(mesho);
            await this.context.SaveChangesAsync();

            var result = await userService.GetUsersTrainerAsync("Mesho");
            var actual = "Pesho";
            var expected = result.UserName;


            Assert.NotNull(result);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task GetUsersTrainerAsyncShouldReturnFalse()
        {
            this.InitiateInMemmoryDbForUsers();

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            var mockedMapper = new Mock<Mapper>(mappingConfig).Object;

            UserManager<OlympiaUser> usermanager = this.TestUserManager<OlympiaUser>();

            var userService = new Mock<UsersService>(
                this.context,
                mockedMapper,
                usermanager).Object;

            var pesho = this.context.Users.SingleOrDefault(x => x.UserName == "Pesho");
            var mesho = this.context.Users.SingleOrDefault(x => x.UserName == "Mesho").Trainer = pesho;

            this.context.Update(mesho);
            await this.context.SaveChangesAsync();

            var result = await userService.GetUsersTrainerAsync("");

            Assert.Null(result);
        }

        [Fact]
        public async Task UpdateUserHeightAndWeightAsyncShouldIncrease()
        {
            this.InitiateInMemmoryDbForUsers();

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            var mockedMapper = new Mock<Mapper>(mappingConfig).Object;
            UserManager<OlympiaUser> usermanager = this.TestUserManager<OlympiaUser>();

            var userService = new Mock<UsersService>(
                this.context,
                mockedMapper,
                usermanager).Object;

            ClientViewModel model = new ClientViewModel()
            {
                Height = 31,
                Weight = 31,
                Activity = ActityLevel.SixToServen
            };

            await userService.UpdateUserHeightAndWeightAsync(model, "Pesho");

            var user = this.context.Users.SingleOrDefault(x => x.UserName == "Pesho");

            var expectedHeight = 31;
            var expectedWeight = 31;
            var expectedActivity = ActityLevel.SixToServen;

            var actualHeight = user.Height;
            var actualWeight = user.Weight;
            var actualActivity = ActityLevel.SixToServen;

            Assert.Equal(expectedHeight, actualHeight);
            Assert.Equal(expectedWeight, actualWeight);
            Assert.Equal(expectedActivity, actualActivity);
        }

        [Fact]
        public async Task UpdateUserHeightAndWeightAsyncShouldReturnFalse()
        {
            this.InitiateInMemmoryDbForUsers();

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            var mockedMapper = new Mock<Mapper>(mappingConfig).Object;
            UserManager<OlympiaUser> usermanager = this.TestUserManager<OlympiaUser>();

            var userService = new Mock<UsersService>(
                this.context,
                mockedMapper,
                usermanager).Object;

            ClientViewModel model = new ClientViewModel()
            {
                Height = 31,
                Weight = 31,
                Activity = ActityLevel.SixToServen
            };

            var result = await userService.UpdateUserHeightAndWeightAsync(model, "");

            Assert.False(result);
        }

        [Fact]
        public async Task GetAllUsersShouldReturnAll()
        {
            this.InitiateInMemmoryDbForUsers();

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            var mockedMapper = new Mock<Mapper>(mappingConfig).Object;
            UserManager<OlympiaUser> usermanager = this.TestUserManager<OlympiaUser>();

            var userService = new Mock<UsersService>(
                this.context,
                mockedMapper,
                usermanager).Object;

            var expected = this.context.Users.Select(x => x.UserName).AsEnumerable();
            var actual = (await userService.GetAllUsersAsync()).Select(x => x.UserName);

            Assert.Equal(expected.Count(), actual.Count());
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task DeleteUserAsyncShouldDelete()
        {
            this.InitiateInMemmoryDbForUsers();

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            var mockedMapper = new Mock<Mapper>(mappingConfig).Object;
            UserManager<OlympiaUser> usermanager = this.TestUserManager<OlympiaUser>();

            var userService = new Mock<UsersService>(
                this.context,
                mockedMapper,
                usermanager).Object;

            this.context.Users.Add(new OlympiaUser { UserName = "asd", FullName = "asd", Email = "asd@ad.bg" });
            await this.context.SaveChangesAsync();

            var result = await userService.DeleteUserAsync("asd");

            Assert.True(result);
        }

        [Fact]
        public async Task DeleteUserAsyncShouldReturnFalse()
        {
            this.InitiateInMemmoryDbForUsers();

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            var mockedMapper = new Mock<Mapper>(mappingConfig).Object;
            UserManager<OlympiaUser> usermanager = this.TestUserManager<OlympiaUser>();

            var userService = new Mock<UsersService>(
                this.context,
                mockedMapper,
                usermanager).Object;

            await this.context.SaveChangesAsync();

            var result = await userService.DeleteUserAsync("");

            Assert.False(result);
        }

        [Fact]
        public async Task UnSetTrainerAsyncShouldSetTrainer()
        {
            this.InitiateInMemmoryDbForUsers();

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            var mockedMapper = new Mock<Mapper>(mappingConfig).Object;
            UserManager<OlympiaUser> usermanager = this.TestUserManager<OlympiaUser>();

            var userService = new Mock<UsersService>(
                this.context,
                mockedMapper,
                userManager).Object;

            var pesho = this.context.Users.SingleOrDefault(x => x.UserName == "Pesho");
            var mesho = this.context.Users.SingleOrDefault(x => x.UserName == "Mesho");

            mesho.Trainer = pesho;
            this.context.Update(mesho);
            await this.context.SaveChangesAsync();

            var result = await userService.UnsetTrainerAsync(mesho.UserName, pesho.UserName);

            Assert.True(result);
        }

        [Fact]
        public async Task UnSetTrainerAsyncShouldReturnFalse()
        {
            this.InitiateInMemmoryDbForUsers();

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            var mockedMapper = new Mock<Mapper>(mappingConfig).Object;

            UserManager<OlympiaUser> usermanager = this.TestUserManager<OlympiaUser>();

            var userService = new Mock<UsersService>(
                this.context,
                mockedMapper,
                userManager).Object;

            var result = await userService.UnsetTrainerAsync("", "");

            Assert.False(result);
        }

        [Fact]
        public async Task CalculateCaloriesShouldReturnCorrect()
        {
            this.InitiateInMemmoryDbForUsers();

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            var mockedMapper = new Mock<Mapper>(mappingConfig).Object;
            UserManager<OlympiaUser> usermanager = this.TestUserManager<OlympiaUser>();

            var userService = new Mock<UsersService>(
                this.context,
                mockedMapper,
                userManager).Object;

            var user = await this.context.Users.SingleOrDefaultAsync(x => x.UserName == "Pesho");

            user.Gender = Gender.Male;
            user.Weight = 31;
            user.Height = 31;
            user.Age = 31;
            user.Activity = ActityLevel.OneToThree;

            this.context.Update(user);
            await this.context.SaveChangesAsync();

            var expected = 602;
            var actual = await userService.CalculateCaloriesAsync(user.UserName);

            Assert.Equal(expected, actual);
        }


        [Fact]
        public async Task SetFitnessPlanToUserAsyncShouldReturnTrue()
        {
            this.InitiateInMemmoryDbForUsers();

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            var mockedMapper = new Mock<Mapper>(mappingConfig).Object;
            UserManager<OlympiaUser> usermanager = this.TestUserManager<OlympiaUser>();

            var userService = new Mock<UsersService>(
                this.context,
                mockedMapper,
                userManager).Object;

            ClientViewModel model = new ClientViewModel()
            {
                UserName = "Pesho",
                WorkoutViewModel = new WorkoutViewModel
                {
                    Exercises = this.context.Exercises.ToList()
                },
                Calories = 2010
            };

            var result = await userService.SetFitnessPlanToUserAsync(model);

            Assert.True(result);
        }

        [Fact]
        public async Task UpdateProfileAsyncShouldExecuteCorrectly()
        {
            this.InitiateInMemmoryDbForUsers();

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            var mockedMapper = new Mock<Mapper>(mappingConfig).Object;
            UserManager<OlympiaUser> usermanager = this.TestUserManager<OlympiaUser>();

            var userService = new Mock<UsersService>(
                this.context,
                mockedMapper,
                userManager).Object;

            var user = await this.context.Users.SingleOrDefaultAsync(x => x.UserName == "Pesho");

            UserProfile model = new UserProfile()
            {
                Weight = 31,
                Height = 31,
                Age = 31,
                Actity = ActityLevel.ThreeToFive,
            };

            await userService.UpdateProfileAsync(model, user.UserName);

            Assert.Equal(user.Height, model.Height);
            Assert.Equal(user.Weight, model.Weight);
            Assert.Equal(user.Age, model.Age);
            Assert.Equal(user.Activity, model.Actity);
        }
        #endregion

        #region Shop Service Tests
        [Fact]
        public async Task GetShoppingCartDtoByUserNameAsyncShouldReturnTrue()
        {
            this.InitiateInMemmoryDbForShop();

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            var usermanager = this.TestUserManager<OlympiaUser>();
            var mockedMapper = new Mock<Mapper>(mappingConfig).Object;
            var mockUserService = new Mock<UsersService>(this.context, mockedMapper, usermanager).Object;
            var shoppService = new Mock<ShopService>(mockedMapper, mockUserService, this.context).Object;

            var cart = await shoppService.GetShoppingCartDtoByUserNameAsync("Pesho");

            Assert.NotNull(cart);
        }

        [Fact]
        public async Task GetShoppingCartDtoByUserNameAsyncShouldReturnNull()
        {
            this.InitiateInMemmoryDbForShop();

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            var usermanager = this.TestUserManager<OlympiaUser>();
            var mockedMapper = new Mock<Mapper>(mappingConfig).Object;
            var mockUserService = new Mock<UsersService>(this.context, mockedMapper, usermanager).Object;
            var shoppService = new Mock<ShopService>(mockedMapper, mockUserService, this.context).Object;

            var cart = await shoppService.GetShoppingCartDtoByUserNameAsync(null);

            Assert.Null(cart);
        }

        [Fact]
        public async Task GetItemByIdAsyncShouldNotBeNull()
        {
            this.InitiateInMemmoryDbForShop();

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            var usermanager = this.TestUserManager<OlympiaUser>();
            var mockedMapper = new Mock<Mapper>(mappingConfig).Object;
            var mockUserService = new Mock<UsersService>(this.context, mockedMapper, usermanager).Object;
            var shoppService = new Mock<ShopService>(mockedMapper, mockUserService, this.context).Object;

            var item = await shoppService.GetItemByIdAsync(1);

            Assert.NotNull(item);
        }

        [Fact]
        public async Task GetItemByIdAsyncShouldBeNull()
        {
            this.InitiateInMemmoryDbForShop();

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            var usermanager = this.TestUserManager<OlympiaUser>();
            var mockedMapper = new Mock<Mapper>(mappingConfig).Object;
            var mockUserService = new Mock<UsersService>(this.context, mockedMapper, usermanager).Object;
            var shoppService = new Mock<ShopService>(mockedMapper, mockUserService, this.context).Object;

            var itemIntMax = await shoppService.GetItemByIdAsync(int.MaxValue);
            var itemIntMin = await shoppService.GetItemByIdAsync(int.MinValue);

            Assert.Null(itemIntMax);
            Assert.Null(itemIntMin);
        }

        [Fact]
        public async Task GetAllItems()
        {
            this.InitiateInMemmoryDbForShop();

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            var usermanager = this.TestUserManager<OlympiaUser>();
            var mockedMapper = new Mock<Mapper>(mappingConfig).Object;
            var mockUserService = new Mock<UsersService>(this.context, mockedMapper, usermanager).Object;
            var shoppService = new Mock<ShopService>(mockedMapper, mockUserService, this.context).Object;

            var expectedIds = this.context.Items.Select(x => x.Id).AsEnumerable();

            var actualIds = (await shoppService.GetAllItemsAsync()).Select(x => x.Id).ToList();
            var index = 0;


            Assert.Equal(expectedIds.Count(), actualIds.Count());

            foreach (var expected in expectedIds)
            {
                Assert.Equal(expected, actualIds[index++]);
            }
        }        

        [Fact]
        public async Task CreateItemAsyncShouldReturnFalse()
        {
            this.InitiateInMemmoryDbForShop();

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            var usermanager = this.TestUserManager<OlympiaUser>();
            var mockedMapper = new Mock<Mapper>(mappingConfig).Object;
            var mockUserService = new Mock<UsersService>(this.context, mockedMapper, usermanager).Object;
            var shoppService = new Mock<ShopService>(mockedMapper, mockUserService, this.context).Object;

            var fileMock = new Mock<IFormFile>();
            //Setup mock file using a memory stream
            var content = "Hello World from a Fake File";
            var fileName = "test.pdf";
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write(content);
            writer.Flush();
            ms.Position = 0;
            fileMock.Setup(_ => _.OpenReadStream()).Returns(ms);
            fileMock.Setup(_ => _.FileName).Returns(fileName);
            fileMock.Setup(_ => _.Length).Returns(ms.Length);

            var file = fileMock.Object;

            ItemBindingModel model = new ItemBindingModel()
            {
                Name = "",
                Description = "",
                Price = -21,
                ImgUrl = null,
                CategoryName = Category.Fitness,
                SupplierName = ""
            };

            var result = await shoppService.CreateItemAsync(model);
            Assert.False(result);
        }

        [Fact]
        public async Task GetAllItemsByCategoryShouldReturnAll()
        {
            this.InitiateInMemmoryDbForShop();

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            var usermanager = this.TestUserManager<OlympiaUser>();
            var mockedMapper = new Mock<Mapper>(mappingConfig).Object;
            var mockUserService = new Mock<UsersService>(this.context, mockedMapper, usermanager).Object;
            var shoppService = new Mock<ShopService>(mockedMapper, mockUserService, this.context).Object;

            var expectedIds = Enumerable.Range(1, 11).Reverse();
            var actualIds = (await shoppService.GetAllItemsByCategoryAsync("Fitness")).Select(x => x.Id).ToList();

            Assert.Equal(expectedIds.Count(), actualIds.Count());
            int index = 0;

            foreach (var expectedId in expectedIds)
            {
                Assert.Equal(expectedId, actualIds[index++]);
            }
        }

        [Fact]
        public async Task GetItemDtoByIdAsyncShouldNotBeNull()
        {
            this.InitiateInMemmoryDbForShop();

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            var usermanager = this.TestUserManager<OlympiaUser>();
            var mockedMapper = new Mock<Mapper>(mappingConfig).Object;
            var mockUserService = new Mock<UsersService>(this.context, mockedMapper, usermanager).Object;
            var shoppService = new Mock<ShopService>(mockedMapper, mockUserService, this.context).Object;

            var result = await shoppService.GetItemDtoByIdAsync(1);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetItemDtoByIdAsyncShouldBeNull()
        {
            this.InitiateInMemmoryDbForShop();

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            var usermanager = this.TestUserManager<OlympiaUser>();
            var mockedMapper = new Mock<Mapper>(mappingConfig).Object;
            var mockUserService = new Mock<UsersService>(this.context, mockedMapper, usermanager).Object;
            var shoppService = new Mock<ShopService>(mockedMapper, mockUserService, this.context).Object;

            var resultMax = await shoppService.GetItemDtoByIdAsync(int.MaxValue);
            var resultMin = await shoppService.GetItemDtoByIdAsync(int.MinValue);

            Assert.Null(resultMax);
            Assert.Null(resultMin);
        }

        [Fact]
        public async Task GetAllSuppliersShouldReturnAll()
        {
            this.InitiateInMemmoryDbForShop();

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            var usermanager = this.TestUserManager<OlympiaUser>();
            var mockedMapper = new Mock<Mapper>(mappingConfig).Object;
            var mockUserService = new Mock<UsersService>(this.context, mockedMapper, usermanager).Object;
            var shoppService = new Mock<ShopService>(mockedMapper, mockUserService, this.context).Object;

            var expected = this.context.Suppliers.AsEnumerable();
            var actual = (await shoppService.GetAllSuppliersAsync()).ToList();
            var index = 0;

            Assert.Equal(expected.Count(), actual.Count());

            foreach (var expectedSupplier in expected)
            {
                Assert.Equal(expectedSupplier, actual[index++]);
            }
        }

        [Fact]
        public async Task AddItemToUserCartAsyncShouldReturnTrue()
        {
            this.InitiateInMemmoryDbForShop();

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            var usermanager = this.TestUserManager<OlympiaUser>();
            var mockedMapper = new Mock<Mapper>(mappingConfig).Object;
            var mockUserService = new Mock<UsersService>(this.context, mockedMapper, usermanager).Object;
            var shoppService = new Mock<ShopService>(mockedMapper, mockUserService, this.context).Object;

            var item = await this.context.Items.SingleOrDefaultAsync(x => x.Id == 1);
            var user = await this.context.Users.SingleOrDefaultAsync(x => x.UserName == "Pesho");

            var result = await shoppService.AddItemToUserCartAsync(item.Id, user.UserName);

            Assert.True(result);
        }

        [Fact]
        public async Task AddItemToUserCartAsyncShouldReturnFalse()
        {
            this.InitiateInMemmoryDbForShop();

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            var usermanager = this.TestUserManager<OlympiaUser>();
            var mockedMapper = new Mock<Mapper>(mappingConfig).Object;
            var mockUserService = new Mock<UsersService>(this.context, mockedMapper, usermanager).Object;
            var shoppService = new Mock<ShopService>(mockedMapper, mockUserService, this.context).Object;

            var result = await shoppService.AddItemToUserCartAsync(int.MaxValue, "");

            Assert.False(result);
        }

        [Fact]
        public async Task AddItemToUserCartAsyncShouldReturnFalseIntMax()
        {
            this.InitiateInMemmoryDbForShop();

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            var usermanager = this.TestUserManager<OlympiaUser>();
            var mockedMapper = new Mock<Mapper>(mappingConfig).Object;
            var mockUserService = new Mock<UsersService>(this.context, mockedMapper, usermanager).Object;
            var shoppService = new Mock<ShopService>(mockedMapper, mockUserService, this.context).Object;

            var user = await this.context.Users.SingleOrDefaultAsync(x => x.UserName == "Pesho");
            var result = await shoppService.AddItemToUserCartAsync(int.MaxValue, user.UserName);

            Assert.False(result);
        }

        [Fact]
        public async Task GetTopFiveItemsAsyncShouldReturnAll()
        {
            this.InitiateInMemmoryDbForShop();

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            var usermanager = this.TestUserManager<OlympiaUser>();
            var mockedMapper = new Mock<Mapper>(mappingConfig).Object;
            var mockUserService = new Mock<UsersService>(this.context, mockedMapper, usermanager).Object;
            var shoppService = new Mock<ShopService>(mockedMapper, mockUserService, this.context).Object;

            var expected = this.context.Items.OrderByDescending(x => x.TimesBought).Take(5).Select(x => x.Id).AsEnumerable();
            var actual = (await shoppService.GetTopFiveItemsAsync()).Select(x => x.Id).ToList();
            var index = 0;

            Assert.Equal(expected.Count(), actual.Count());

            foreach (var expectedItem in expected)
            {
                Assert.Equal(expectedItem, actual[index++]);
            }
        }

        [Fact]
        public async Task GetShoppingCartByUserNameAsyncShouldNotBeNull()
        {
            this.InitiateInMemmoryDbForShop();

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            var usermanager = this.TestUserManager<OlympiaUser>();
            var mockedMapper = new Mock<Mapper>(mappingConfig).Object;
            var mockUserService = new Mock<UsersService>(this.context, mockedMapper, usermanager).Object;
            var shoppService = new Mock<ShopService>(mockedMapper, mockUserService, this.context).Object;

            var cart = await shoppService.GetShoppingCartByUserNameAsync("Pesho");

            Assert.NotNull(cart);
        }

        [Fact]
        public async Task GetShoppingCartByUserNameAsyncShouldBeNull()
        {
            this.InitiateInMemmoryDbForShop();

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            var usermanager = this.TestUserManager<OlympiaUser>();
            var mockedMapper = new Mock<Mapper>(mappingConfig).Object;
            var mockUserService = new Mock<UsersService>(this.context, mockedMapper, usermanager).Object;
            var shoppService = new Mock<ShopService>(mockedMapper, mockUserService, this.context).Object;

            var cartNull = await shoppService.GetShoppingCartByUserNameAsync(null);
            var cartStringEmpty = await shoppService.GetShoppingCartByUserNameAsync("");

            Assert.Null(cartNull);
            Assert.Null(cartStringEmpty);
        }

        [Theory]
        [InlineData(int.MaxValue)]
        [InlineData(int.MinValue)]
        [InlineData(1)]
        public async Task GetShoppingCartByCartIdAsyncShouldNotBeNull(int id)
        {
            this.InitiateInMemmoryDbForShop();

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            var usermanager = this.TestUserManager<OlympiaUser>();
            var mockedMapper = new Mock<Mapper>(mappingConfig).Object;
            var mockUserService = new Mock<UsersService>(this.context, mockedMapper, usermanager).Object;
            var shoppService = new Mock<ShopService>(mockedMapper, mockUserService, this.context).Object;

            var cart = await shoppService.GetShoppingCartByCartIdAsync(id);

            if (id == int.MaxValue || id == int.MinValue)
            {
                Assert.Null(cart);
            }
            else
            {
                Assert.NotNull(cart);
            }
        }

        [Theory]
        [InlineData(null, 1)]
        [InlineData(null, int.MaxValue)]
        [InlineData("Pesho", int.MaxValue)]
        [InlineData("Pesho", int.MinValue)]
        [InlineData("Pesho", 6)]
        [InlineData("", 1)]
        public async Task RemoveFromCartAsync(string username, int itemId)
        {
            this.InitiateInMemmoryDbForShop();

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            var usermanager = this.TestUserManager<OlympiaUser>();
            var mockedMapper = new Mock<Mapper>(mappingConfig).Object;
            var mockUserService = new Mock<UsersService>(this.context, mockedMapper, usermanager).Object;
            var shoppService = new Mock<ShopService>(mockedMapper, mockUserService, this.context).Object;

            var result = await shoppService.RemoveFromCartAsync(username, itemId);

            if (username == "Pesho" && itemId == 1)
            {
                Assert.True(result);
            }
            else
            {
                Assert.False(result);
            }
        }

        [Theory]
        [InlineData(int.MaxValue)]
        [InlineData(int.MinValue)]
        [InlineData(5)]
        public async Task DeleteItemAsyncShouldDelete(int itemId)
        {
            this.InitiateInMemmoryDbForShop();

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            var usermanager = this.TestUserManager<OlympiaUser>();
            var mockedMapper = new Mock<Mapper>(mappingConfig).Object;
            var mockUserService = new Mock<UsersService>(this.context, mockedMapper, usermanager).Object;
            var shoppService = new Mock<ShopService>(mockedMapper, mockUserService, this.context).Object;


            if (itemId == 5)
            {
                var item = new Item("ASD", 31.31m) { Id = 21 };
                await this.context.Items.AddAsync(item);
                await this.context.SaveChangesAsync();

                var result = await shoppService.DeleteItemAsync(21);

                if (itemId == int.MaxValue || itemId == int.MinValue)
                {
                    Assert.False(result);
                }
                else
                {
                    Assert.True(result);
                }
            }
            else
            {

                var result = await shoppService.DeleteItemAsync(itemId);

                if (itemId == int.MaxValue || itemId == int.MinValue)
                {
                    Assert.False(result);
                }
                else
                {
                    Assert.True(result);
                }
            }
        }

        [Theory]
        [InlineData(int.MaxValue)]
        [InlineData(int.MinValue)]
        [InlineData(5)]
        public async Task IncreaseTimesItemIsBoughtAsyncShouldIncreaseOrReturnNull(int itemId)
        {
            this.InitiateInMemmoryDbForShop();

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            var usermanager = this.TestUserManager<OlympiaUser>();
            var mockedMapper = new Mock<Mapper>(mappingConfig).Object;
            var mockUserService = new Mock<UsersService>(this.context, mockedMapper, usermanager).Object;
            var shoppService = new Mock<ShopService>(mockedMapper, mockUserService, this.context).Object;

            var result = await shoppService.IncreaseTimesItemIsBoughtAsync(itemId);

            if (itemId == int.MaxValue || itemId == int.MinValue)
            {
                Assert.False(result);
            }
            else
            {
                Assert.True(result);
            }
        }

        [Theory]
        [InlineData(int.MaxValue)]
        [InlineData(int.MinValue)]
        [InlineData(5)]
        public async Task DecreaseTimesItemIsBoughtAsyncShouldIncreaseOrReturnNull(int itemId)
        {
            this.InitiateInMemmoryDbForShop();

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            var usermanager = this.TestUserManager<OlympiaUser>();
            var mockedMapper = new Mock<Mapper>(mappingConfig).Object;
            var mockUserService = new Mock<UsersService>(this.context, mockedMapper, usermanager).Object;
            var shoppService = new Mock<ShopService>(mockedMapper, mockUserService, this.context).Object;

            var result = await shoppService.DecreaseTimesItemIsBoughtAsync(itemId);

            if (itemId == int.MaxValue || itemId == int.MinValue)
            {
                Assert.False(result);
            }
            else
            {
                Assert.True(result);
            }
        }

        [Theory]
        [InlineData("Pesho")]
        [InlineData("asd")]
        [InlineData("")]
        [InlineData(null)]
        public async Task FinishOrderAsyncShouldFinishSuccessfully(string username)
        {
            this.InitiateInMemmoryDbForShop();

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            var usermanager = this.TestUserManager<OlympiaUser>();
            var mockedMapper = new Mock<Mapper>(mappingConfig).Object;
            var mockUserService = new Mock<UsersService>(this.context, mockedMapper, usermanager).Object;
            var shoppService = new Mock<ShopService>(mockedMapper, mockUserService, this.context).Object;

            if (username == "Pesho")
            {
                var user = await this.context.Users.SingleOrDefaultAsync(x => x.UserName == username);
                var items = this.context.Items.Take(3).ToList();

                foreach (var item in items)
                {
                    user.ShoppingCart.ShoppingCartItems.Add(new ShoppingCartItem() { Item = item, ShoppingCart = user.ShoppingCart });
                }

                this.context.Update(user);
                await this.context.SaveChangesAsync();

                var result = await shoppService.FinishOrderAsync(username);

                Assert.True(result);
            }
            else
            {
                var result = await shoppService.FinishOrderAsync(username);

                if (string.IsNullOrEmpty(username) || username == "asd")
                {
                    Assert.False(result);
                }
                else
                {
                    Assert.True(result);
                }
            }
        }
        #endregion
    }
}
