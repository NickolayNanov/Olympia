namespace Olympia.Services.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Olympia.Data.Domain;
    using Olympia.Data.Models.BindingModels.Account;

    public interface IUsersService
    {
        Task<OlympiaUser> GetUserByUsernameAsync(string username);

        Task<IEnumerable<OlympiaUser>> GetAllTrainersAsync();

        Task<IEnumerable<OlympiaUser>> GetAllClientsByUserAsync(string trainerUsername);

        Task<bool> SetTrainerAsync(string trainerUsername, string clientUsername);

        Task<bool> BecomeTrainerAsync(ClientToTrainerBindingModel model, string username);
    }
}
