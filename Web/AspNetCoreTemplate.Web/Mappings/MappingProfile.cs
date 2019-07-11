namespace Olympia.Web.Mappings
{
    using AutoMapper;
    using Olympia.Data.Domain;
    using Olympia.Data.Models.BindingModels.Account;
    using Olympia.Data.Models.BindingModels.Blogs;
    using Olympia.Data.Models.ViewModels;
    using Olympia.Data.Models.ViewModels.BlogPartViewModels;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.CreateMap<Article, ArticleViewModel>();
            this.CreateMap<CreateArticleBindingModel, Article>();

            this.CreateMap<UserRegisterBingingModel, OlympiaUser>();
            this.CreateMap<OlympiaUser, UserViewModel>();
        }
    }
}
