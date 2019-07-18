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
    using Olympia.Data.Models.ViewModels.Shop;
    using System.Linq;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.CreateMap<Article, ArticleViewModel>()
                .ReverseMap();
            this.CreateMap<CreateArticleBindingModel, Article>();

            this.CreateMap<UserRegisterBingingModel, OlympiaUser>();

            this.CreateMap<OlympiaUser, UserViewModel>()
                .ForMember(x => x.Weight, y => y.MapFrom(z => z.Weight.ToString()))
                .ForMember(x => x.Height, y => y.MapFrom(z => z.Height.ToString()));

            this.CreateMap<OlympiaUser, ClientToTrainerBindingModel>()
                .ForMember(x => x.Username, y => y.MapFrom(z => z.UserName))
                .ForMember(x => x.FullName, y => y.MapFrom(z => z.FullName))
            .ReverseMap();

            this.CreateMap<OlympiaUser, ClientViewModel>()
                .ReverseMap();

            this.CreateMap<OlympiaUser, ListedUserViewModel>()
                .ForMember(x => x.Role,
                y => y.MapFrom(z => z.OlympiaUserRole
                        .Select(x => x.RoleId)
                        .Contains("e9a584d9-3bcd-439b-ac73-aa996070897e") ? "Trainer" : "Client"));

            this.CreateMap<Workout, WorkoutViewModel>()
                .ReverseMap();

            this.CreateMap<ClientViewModel, FitnessPlan>()
                .ForMember(x => x.CaloriesGoal, y => y.MapFrom(z => z.Calories))
                .ForMember(x => x.WeekWorkoutDuration, y => y.MapFrom(z => z.WorkoutInputModel.Duration))
                .ForMember(x => x.Workout, y => y.MapFrom(z => z.WorkoutViewModel))
                .ForMember(x => x.WeekWorkoutDuration, y => y.MapFrom(z => z.WorkoutInputModel.Duration))
                .ReverseMap();

            this.CreateMap<Item, ItemViewModel>();
            this.CreateMap<ItemBindingModel, Item>();

            this.CreateMap<SupplierBindingModel, Supplier>();
        }
    }
}
