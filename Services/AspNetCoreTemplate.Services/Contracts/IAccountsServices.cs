namespace Olympia.Services.Contracts
{
    using System.Threading.Tasks;

    using Olympia.Data.Domain;
    using Olympia.Data.Models.BindingModels.Account;

    public interface IAccountsServices
    {
        Task<OlympiaUser> RegisterUser(UserInputBingingModel model);
    }
}
