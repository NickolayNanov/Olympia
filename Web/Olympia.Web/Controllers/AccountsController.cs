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

        [AllowAnonymous]
        public IActionResult Register()
        {
            return this.View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(UserRegisterBingingModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var user = await this.accountsServices.RegisterUserAsync(model);

            if (user == null)
            {
                this.ViewData["Errors"] = GlobalConstants.InvalidRegisterMessage;
                return this.View(model);
            }

            await this.signInManager.SignInAsync(user, isPersistent: true);

            return this.Redirect(GlobalConstants.Index);
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            return this.View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserLoginBindingModel model)
        {
            if (!this.ModelState.IsValid ||
                string.IsNullOrEmpty(model.UserName) ||
                string.IsNullOrEmpty(model.Password))
            {
                return this.View(model);
            }

            var user = await this.accountsServices.LoginUserAsync(model);

            if (user == null)
            {
                this.ViewData["Errors"] = GlobalConstants.InvalidLoginMessage;
                return this.View();
            }

            return this.Redirect(GlobalConstants.Index);
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await this.signInManager.SignOutAsync();

            return this.Redirect(GlobalConstants.Index);
        }

        [Authorize]
        public IActionResult Profile()
        {
            return this.View();
        }
    }
}
