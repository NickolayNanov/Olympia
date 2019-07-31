namespace Olympia.Services.Contracts
{
    using Olympia.Data.Domain;
    using Olympia.Data.Models.BindingModels.Account;

    using System.Threading.Tasks;

    public interface IAccountsServices
    {
        Task<OlympiaUser> RegisterUserAsync(UserRegisterBingingModel model);

        Task<OlympiaUser> LoginUserAsync(UserLoginBindingModel model);
    }
}
