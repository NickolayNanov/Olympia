namespace Olympia.Services
{
    using System.IO;
    using System.Threading.Tasks;

    using AutoMapper;
    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using Microsoft.AspNetCore.Http;
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
            var user = await this.userManager.FindByNameAsync(model.UserName);

            if (user == null)
            {
                return null;
            }

            await this.signInManager.SignInAsync(user, true);
            return user;
        }

        public async Task<OlympiaUser> RegisterUserAsync(UserRegisterBingingModel model)
        {
            await this.AddRootAdminIfDoesNotExistAsync();

            var user = this.mapper.Map<OlympiaUser>(model);

            var result = await this.userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await this.userManager.AddToRoleAsync(user, GlobalConstants.ClientRoleName);
            }

            var cloudinaryAccount = this.SetCloudinary();
            var url = this.UploadImage(cloudinaryAccount, model.ProfilePicturImgUrl, model.Username);
            user.ProfilePicturImgUrl = url ?? Constants.CloudinaryInvalidUrl;

            this.context.Update(user);
            await this.context.SaveChangesAsync();

            return user;
        }

        private async Task AddRootAdminIfDoesNotExistAsync()
        {
            if (!await this.userManager.Users.AnyAsync())
            {
                var god = new OlympiaUser { UserName = "God", Email = "God@abv.bg", FullName = "God God" };
                await this.userManager.CreateAsync(god, password: "imgod123");
                await this.userManager.AddToRoleAsync(god, GlobalConstants.AdministratorRoleName);
            }
        }

        private string UploadImage(Cloudinary cloudinary, IFormFile fileform, string articleTitle)
        {
            if (fileform == null)
            {
                return null;
            }

            byte[] articleImg;

            using (var memoryStream = new MemoryStream())
            {
                fileform.CopyTo(memoryStream);
                articleImg = memoryStream.ToArray();
            }

            ImageUploadResult uploadResult;

            using (var ms = new MemoryStream(articleImg))
            {
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(articleTitle, ms),
                    Transformation = new Transformation(),
                };

                uploadResult = cloudinary.Upload(uploadParams);
            }

            return uploadResult.SecureUri.AbsoluteUri;
        }

        // TODO: export to json
        private Cloudinary SetCloudinary()
        {
            CloudinaryDotNet.Account account = new CloudinaryDotNet.Account
            {

                Cloud = Constants.CloudinaryCloudName,
                ApiKey = Constants.CloudinaryApiKey,
                ApiSecret = Constants.CloudinaryApiSecret,
            };

            Cloudinary cloudinary = new Cloudinary(account);
            return cloudinary;
        }
    }
}
