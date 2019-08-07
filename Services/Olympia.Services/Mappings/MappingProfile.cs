namespace Olympia.Services
{
    using AutoMapper;

    using Olympia.Data.Domain;
    using Olympia.Data.Models.BindingModels.Account;
    using Olympia.Data.Models.BindingModels.Blogs;
    using Olympia.Data.Models.BindingModels.Client;
    using Olympia.Data.Models.BindingModels.Shop;
    using Olympia.Data.Models.ViewModels.AdminViewModels;
    using Olympia.Data.Models.ViewModels.BlogPartViewModels;
    using Olympia.Data.Models.ViewModels.Fitness;
    using Olympia.Data.Models.ViewModels.Home;
    using Olympia.Data.Models.ViewModels.Shop;

    using System.Linq;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.CreateMap<Article, ArticleViewModel>()
                .ReverseMap();

            this.CreateMap<CreateArticleBindingModel, Article>();

            this.CreateMap<UserRegisterBingingModel, OlympiaUser>()
                .ForMember(x => x.Address, y => y.Ignore());

            this.CreateMap<OlympiaUser, UserViewModel>()
                .ForMember(x => x.Weight, y => y.MapFrom(z => z.Weight.ToString()))
                .ForMember(x => x.Height, y => y.MapFrom(z => z.Height.ToString()));

            this.CreateMap<OlympiaUser, ClientToTrainerBindingModel>()
                .ForMember(x => x.Username, y => y.MapFrom(z => z.UserName))
                .ForMember(x => x.FullName, y => y.MapFrom(z => z.FullName))
            .ReverseMap();

            this.CreateMap<OlympiaUser, ClientViewModel>()
                .ReverseMap();

            this.CreateMap<OlympiaUser, ListedUserViewModel>();

            this.CreateMap<Workout, WorkoutViewModel>()
                .ReverseMap();

            this.CreateMap<ClientViewModel, FitnessPlan>()
                .ForMember(x => x.CaloriesGoal, y => y.MapFrom(z => z.Calories))
                .ForMember(x => x.WeekWorkoutDuration, y => y.MapFrom(z => z.WorkoutInputModel.Duration))
                .ForMember(x => x.Workout, y => y.MapFrom(z => z.WorkoutViewModel))
                .ForMember(x => x.WeekWorkoutDuration, y => y.MapFrom(z => z.WorkoutInputModel.Duration))
                .ReverseMap();

            this.CreateMap<Item, ItemViewModel>().ReverseMap();

            this.CreateMap<ItemBindingModel, Item>();

            this.CreateMap<SupplierBindingModel, Supplier>();

            this.CreateMap<OlympiaUser, UserProfile>()
                .ForMember(x => x.Adress, y => y.MapFrom(z => z.Address.Location))
                .ReverseMap();

            this.CreateMap<FitnessPlan, FitnessPlanViewModel>()
                .ForMember(x => x.Workout, y => y.MapFrom(z => z.Workout))
                .ForMember(x => x.Calories, y => y.MapFrom(z => z.CaloriesGoal))
                .ForMember(x => x.WeekWorkoutDuration, y => y.MapFrom(z => z.WeekWorkoutDuration))
                .ReverseMap();

            this.CreateMap<ShoppingCart, ShoppingCartViewModel>().ReverseMap();
        }
    }
}
