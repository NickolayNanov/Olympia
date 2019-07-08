namespace Olympia.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Olympia.Data.Domain;
    using Olympia.Data.Models.BindingModels.Account;
    using Olympia.Services.Contracts;

    public class AccountsController : Controller
    {
        private readonly SignInManager<OlympiaUser> signInManager;
        private readonly UserManager<OlympiaUser> userManager;
        private readonly IAccountsServices accountsServices;

        public AccountsController(SignInManager<OlympiaUser> signInManager, UserManager<OlympiaUser> userManager ,IAccountsServices accountsServices)
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
                return this.Redirect("/Accounts/Register");
            }

            var user = await this.accountsServices.RegisterUser(model);
            await this.signInManager.SignInAsync(user, isPersistent: true);

            return this.Redirect("/");
        }

        public async Task<IActionResult> Logout()
        {
            await this.signInManager.SignOutAsync();

            return this.Redirect("/");
        }

        [Authorize]
        public IActionResult Profile()
        {
            return this.View();
        }
    }
}