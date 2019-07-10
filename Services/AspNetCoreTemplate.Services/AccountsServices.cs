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
        private readonly SignInManager<OlympiaUser> signInManager;

        public AccountsServices(
            IMapper mapper,
            UserManager<OlympiaUser> userManager,
            OlympiaDbContext context,
            SignInManager<OlympiaUser> signInManager)
        {
            this.mapper = mapper;
            this.userManager = userManager;
            this.context = context;
            this.signInManager = signInManager;
        }

        public async Task<OlympiaUser> LoginUserAsync(UserLoginBindingModel model)
        {
            var user = await this.userManager.FindByNameAsync(model.UserName);

            if (user == null)
            {
                return null;
            }

            await this.signInManager.SignInAsync(user, true);
            return user;
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
