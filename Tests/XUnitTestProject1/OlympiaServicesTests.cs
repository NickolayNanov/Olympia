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

        #region Accounts Service Tests
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

        #endregion

        #region Fitness Service Tests
        [Fact]
        public void GetWorkoutsShouldReturnAllWorkouts()
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

        [Fact]
        public void GetWorkoutByIdShouldReturnTheCorrectOne()
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
            var article = fitnessService.GetWorkoutById(1).Id;

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
        public void GetAllSuppliersShouldReturnAllSuppliers()
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
            var actual = fitnessService.GetAllSuppliers().Select(x => x.Name).ToList();

            int index = 0;

            foreach (var supplierName in suppliers)
            {
                Assert.Equal(supplierName, actual[index++]);
            }
        }

        [Fact]
        public void GetAllItemsShouldReturnAllItems()
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
            var actual = fitnessService.GetAllItems().Items.Select(x => x.Name).ToList();

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

            var actual = await fitnessService.GetFitnessPlanByUsername("Niki");

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
        #endregion
    }
}
