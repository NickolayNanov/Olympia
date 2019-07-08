namespace Olympia.Services
{
    using System.Threading.Tasks;

    using AutoMapper;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Olympia.Data;
    using Olympia.Data.Domain;
    using Olympia.Data.Models.BindingModels.Account;
    using Olympia.Services.Contracts;

    public class AccountsServices : IAccountsServices
    {
        private readonly IMapper mapper;
        private readonly UserManager<OlympiaUser> userManager;
        private readonly OlympiaDbContext context;

        public AccountsServices(IMapper mapper, UserManager<OlympiaUser> userManager, OlympiaDbContext context)
        {
            this.mapper = mapper;
            this.userManager = userManager;
            this.context = context;
        }

        public async Task<OlympiaUser> RegisterUser(UserInputBingingModel model)
        {
            await this.AddRootAdminIfDoesNotExist();

            var user = this.mapper.Map<OlympiaUser>(model);

            var result = await this.userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await this.userManager.AddToRoleAsync(user, "Client");
            }

            return user;
        }

        private async Task AddRootAdminIfDoesNotExist()
        {
            if (!this.userManager.Users.AnyAsync().Result)
            {
                var god = new OlympiaUser { UserName = "God", Email = "God@abv.bg", FullName = "God God" };
                await this.userManager.CreateAsync(god, password: "imgod123");
                await this.userManager.AddToRoleAsync(god, "Administrator");
            }
        }
    }
}
