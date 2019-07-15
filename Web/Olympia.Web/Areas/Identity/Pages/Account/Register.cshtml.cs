namespace Olympia.Web.Areas.Identity.Pages.Account
{
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text.Encodings.Web;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.UI.Services;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.Extensions.Logging;
    using Olympia.Common;
    using Olympia.Data.Domain;
    using Olympia.Data.Domain.Enums;

    [AllowAnonymous]
#pragma warning disable SA1649 // File name should match first type name
    public class RegisterModel : PageModel
#pragma warning restore SA1649 // File name should match first type name
    {
        private readonly SignInManager<OlympiaUser> signInManager;
        private readonly RoleManager<OlympiaUserRole> roleManager;
        private readonly UserManager<OlympiaUser> userManager;
        private readonly ILogger<RegisterModel> logger;
        private readonly IEmailSender emailSender;

        public RegisterModel(
            RoleManager<OlympiaUserRole> roleManager,
            UserManager<OlympiaUser> userManager,
            SignInManager<OlympiaUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.logger = logger;
            this.emailSender = emailSender;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public void OnGet(string returnUrl = null)
        {
            this.ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            if (!this.userManager.Users.Any())
            {
                var god = new OlympiaUser { UserName = "God", Email = "God@abv.bg" };
                await this.userManager.CreateAsync(god, "godgodgod");
                await this.userManager.AddToRoleAsync(god, "Administrator");
            }

            returnUrl = returnUrl ?? this.Url.Content("~/");
            if (this.ModelState.IsValid)
            {
                var user = new OlympiaUser { UserName = this.Input.Username, Email = this.Input.Email };
                var result = await this.userManager.CreateAsync(user, this.Input.Password);
                if (result.Succeeded)
                {
                    await this.userManager.AddToRoleAsync(user, "Client");

                    this.logger.LogInformation("User created a new account with password.");

                    var code = await this.userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = this.Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { userId = user.Id, code = code },
                        protocol: this.Request.Scheme);

                    await this.emailSender.SendEmailAsync(
                        this.Input.Email,
                        "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    await this.signInManager.SignInAsync(user, isPersistent: false);
                    return this.LocalRedirect(returnUrl);
                }

                foreach (var error in result.Errors)
                {
                    this.ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return this.Page();
        }

        public class InputModel
        {
            [Required]
            [Display(Name = GlobalConstants.DisplayUsername)]
            public string Username { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = GlobalConstants.ErrorInputMessage, MinimumLength = 3)]
            public string FullName { get; set; }

            [Required]
            [StringLength(50, ErrorMessage = GlobalConstants.ErrorInputMessage, MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = GlobalConstants.DisplayPassword)]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = GlobalConstants.DisplayConfirmPassword)]
            [Compare(GlobalConstants.DisplayPassword, ErrorMessage = GlobalConstants.ConfirmPasswordErrorMessage)]
            public string ConfirmPassword { get; set; }

            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [Range(12, 96, ErrorMessage = GlobalConstants.AgeErrorMessage)]
            public int Age { get; set; }

            [Required]
            public Gender Gender { get; set; }
        }
    }
}
