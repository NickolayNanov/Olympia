﻿namespace Olympia.Web.Mappings
{
    using AutoMapper;
    using Olympia.Data.Domain;
    using Olympia.Data.Models.BindingModels.Account;
    using Olympia.Data.Models.BindingModels.Blogs;
    using Olympia.Data.Models.ViewModels.BlogPartViewModels;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.CreateMap<Article, ArticleViewModel>();
            this.CreateMap<CreateArticleBindingModel, Article>();

            this.CreateMap<UserRegisterBingingModel, OlympiaUser>();

            this.CreateMap<OlympiaUser, UserViewModel>()
                .ForMember(x => x.Weight, y => y.MapFrom(z => z.Weight.ToString()))
                .ForMember(x => x.Height, y => y.MapFrom(z => z.Height.ToString()));

            this.CreateMap<OlympiaUser, ClientToTrainerBindingModel>()
                .ForMember(x => x.Username, y => y.MapFrom(z => z.UserName))
                .ForMember(x => x.FullName, y => y.MapFrom(z => z.FullName))
            .ReverseMap();
        }
    }
}
