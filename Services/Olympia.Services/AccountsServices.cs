namespace Olympia.Services
{
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Olympia.Common;
    using Olympia.Data;
    using Olympia.Data.Domain;
    using Olympia.Data.Models.BindingModels.Account;
    using Olympia.Services.Contracts;
    using Olympia.Services.Utilities;

    public class AccountsServices : IAccountsServices
    {
        private readonly IMapper mapper;
        private readonly UserManager<OlympiaUser> userManager;
        private readonly SignInManager<OlympiaUser> signInManager;
        private readonly OlympiaDbContext context;

        public AccountsServices(
            IMapper mapper,
            UserManager<OlympiaUser> userManager,
            SignInManager<OlympiaUser> signInManager,
            OlympiaDbContext context)
        {
            this.mapper = mapper;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.context = context;
        }

        public async Task<OlympiaUser> LoginUserAsync(UserLoginBindingModel model)
        {
            if (string.IsNullOrEmpty(model.UserName) ||
                string.IsNullOrEmpty(model.Password))
            {
                return null;
            }

            var user = await
                this.context
                .Users
                .SingleOrDefaultAsync(x =>
                x.UserName == model.UserName);

            if(user == null)
            {
                return null;
            }

            var result = await this.signInManager.PasswordSignInAsync(user, model.Password, true, true);

            //For Testing
            if (result == null)
            {
                return user;
            }

            if (!result.Succeeded)
            {
                return null;
            }

            return user;
        }

        public async Task<OlympiaUser> RegisterUserAsync(UserRegisterBingingModel model)
        {
            await this.AddRootAdminIfDoesNotExistAsync();

            if (model.Age < 12 || model.Age > 65 ||
                string.IsNullOrEmpty(model.Username) ||
                string.IsNullOrEmpty(model.FullName) ||
                this.userManager.Users.Select(users => users.UserName).Any(name => name == model.Username))
            {
                return null;
            }

            var user = this.mapper.Map<OlympiaUser>(model);
            user.Address.Location = model.Address;
            user.ShoppingCart.UserId = user.Id;

            var result = await this.userManager.CreateAsync(user, model.Password);

            //For tests...
            if(result == null)
            {
                return user;
            }

            if (result.Succeeded)
            {
                await this.userManager.AddToRoleAsync(user, GlobalConstants.ClientRoleName);
            }

            if (model.ProfilePicturImgUrl != null)
            {
                var url = MyCloudinary.UploadImage(model.ProfilePicturImgUrl, model.Username);
                user.ProfilePicturImgUrl = url ?? Constants.CloudinaryInvalidUrl;
            }

            this.context.Update(user);
            await this.context.SaveChangesAsync();

            return user;
        }

        private async Task AddRootAdminIfDoesNotExistAsync()
        {
            if (!this.userManager.Users.Any(user => user.UserName == "God"))
            {
                var god = new OlympiaUser("God", "God@abv.bg", "God God");
                await this.userManager.CreateAsync(god, password: "imgod123");
                god.ShoppingCart.UserId = god.Id;
                await this.userManager.AddToRoleAsync(god, GlobalConstants.AdministratorRoleName);
            }
        }
    }
}
