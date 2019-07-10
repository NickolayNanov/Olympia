namespace Olympia.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Olympia.Common;
    using Olympia.Data.Domain;
    using Olympia.Data.Models.BindingModels.Account;
    using Olympia.Services.Contracts;

    public class AccountsController : Controller
    {
        private readonly SignInManager<OlympiaUser> signInManager;
        private readonly UserManager<OlympiaUser> userManager;
        private readonly IAccountsServices accountsServices;

        public AccountsController(
            SignInManager<OlympiaUser> signInManager,
            UserManager<OlympiaUser> userManager,
            IAccountsServices accountsServices)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.accountsServices = accountsServices;
        }

        public IActionResult Register()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserInputBingingModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            if (this.userManager.Users.Select(users => users.UserName).Any(name => name == model.Username))
            {
                return this.Redirect(RedirectRoutes.AccountRegister);
            }

            var user = await this.accountsServices.RegisterUser(model);
            await this.signInManager.SignInAsync(user, isPersistent: true);

            return this.Redirect(RedirectRoutes.Index);
        }

        public IActionResult Login()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginBindingModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.Redirect(RedirectRoutes.AccountLogin);
            }

            await this.accountsServices.LoginUserAsync(model);

            return this.Redirect(RedirectRoutes.Index);
        }

        public async Task<IActionResult> Logout()
        {
            await this.signInManager.SignOutAsync();

            return this.Redirect(RedirectRoutes.Index);
        }

        [Authorize]
        public IActionResult Profile()
        {
            return this.View();
        }
    }
}
