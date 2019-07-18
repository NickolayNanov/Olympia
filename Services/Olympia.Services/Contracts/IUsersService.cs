namespace Olympia.Services.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Olympia.Data.Domain;
    using Olympia.Data.Models.BindingModels.Account;
    using Olympia.Data.Models.BindingModels.Client;
    using Olympia.Data.Models.ViewModels.AdminViewModels;
    using Olympia.Data.Models.ViewModels.BlogPartViewModels;

    public interface IUsersService
    {
        Task<ClientViewModel> GetFitnessPlanModelAsync(string username);

        Task<OlympiaUser> GetUserByUsernameAsync(string username);

        Task<IEnumerable<OlympiaUser>> GetAllTrainersAsync();

        Task<IEnumerable<UserViewModel>> GetAllClientsByUserAsync(string trainerUsername);

        Task<bool> SetTrainerAsync(string trainerUsername, string clientUsername);

        Task<bool> BecomeTrainerAsync(ClientToTrainerBindingModel model, string username);

        Task<OlympiaUser> GetUsersTrainerAsync(string username);

        Task<FitnessPlan> CreateFitnessPlanAsync();

        Task<bool> UpdateUserHeightAndWeightAsync(ClientViewModel user, string username);

        IEnumerable<ListedUserViewModel> GetAllUsers();

        Task<bool> DeleteUserAsync(string username);

        Task<bool> UnsetTrainerAsync(string username);

        int CalculateCalories(string username);

        bool SetFitnessPlanToUser(ClientViewModel model);
    }
}
