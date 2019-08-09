namespace Olympia.Services
{
    using AutoMapper;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    using Olympia.Common;
    using Olympia.Data;
    using Olympia.Data.Domain;
    using Olympia.Data.Models.BindingModels.Account;
    using Olympia.Services.Contracts;
    using Olympia.Services.Utilities;

    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

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

            if (user == null || !result.Succeeded)
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
                this.CheckForExistingUser(model) ||
                !this.CheckUsername(model.Username) ||
                !this.CheckFullName(model.FullName))
            {
                return null;
            }

            var user = this.mapper.Map<OlympiaUser>(model);
            user.Address.Location = model.Address;
            user.ShoppingCart.UserId = user.Id;

            var result = await this.userManager.CreateAsync(user, model.Password);

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
       
        private bool CheckUsername(string username)
        {
            Regex regex = new Regex(@"^[A-Za-z_]+[0-9]*$");

            if (!regex.IsMatch(username))
            {
                return false;
            }

            return true;
        }

        private bool CheckFullName(string fullName)
        {
            Regex regex = new Regex(@"^[A-Za-zА-Яа-я ]+$");

            if (!regex.IsMatch(fullName))
            {
                return false;
            }

            return true;
        }

        private async Task AddRootAdminIfDoesNotExistAsync()
        {
            if (!this.context.Users.Any(user => user.UserName == "God"))
            {
                var god = new OlympiaUser("God", "God@abv.bg", "God God");
                await this.userManager.CreateAsync(god, password: "imgod123");
                god.ShoppingCart.UserId = god.Id;
                await this.userManager.AddToRoleAsync(god, GlobalConstants.AdministratorRoleName);
            }
        }

        private bool CheckForExistingUser(UserRegisterBingingModel model)
        {
            return this.userManager.Users
                                .Select(users => users.UserName)
                                .Any(name => name == model.Username);
        }
    }
}
