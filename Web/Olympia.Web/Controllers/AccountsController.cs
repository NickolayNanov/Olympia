﻿namespace Olympia.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    using Olympia.Common;
    using Olympia.Data.Domain;
    using Olympia.Data.Models.BindingModels.Account;
    using Olympia.Data.Models.ViewModels.Home;
    using Olympia.Services.Contracts;

    using System.Threading.Tasks;

    public class AccountsController : Controller
    {

        private readonly SignInManager<OlympiaUser> signInManager;
        private readonly IAccountsServices accountsServices;
        private readonly IUsersService usersService;

        public AccountsController(
            SignInManager<OlympiaUser> signInManager,
            IAccountsServices accountsServices,
            IUsersService usersService)
        {
            this.signInManager = signInManager;
            this.accountsServices = accountsServices;
            this.usersService = usersService;
        }

        [AllowAnonymous]
        public IActionResult Register()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                return this.Redirect(GlobalConstants.ErrorPage);
            }

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
            if (this.User.Identity.IsAuthenticated)
            {
                return this.Redirect(GlobalConstants.ErrorPage);
            }

            return this.View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserLoginBindingModel model)
        {
            if (!this.ModelState.IsValid)
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
        public async Task<IActionResult> ProfileIndex()
        {
            var currentUser = await this.usersService.GetUserProfileModelAsync(this.User.Identity.Name);
            return this.View(currentUser);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ProfileIndex(UserProfile model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            await this.usersService.UpdateProfileAsync(model, this.User.Identity.Name);
            return this.Redirect(GlobalConstants.Index);
        }
    }
}
