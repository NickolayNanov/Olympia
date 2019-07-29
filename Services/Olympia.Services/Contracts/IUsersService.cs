namespace Olympia.Services.Contracts
{
    using Olympia.Data.Domain;
    using Olympia.Data.Models.BindingModels.Account;
    using Olympia.Data.Models.BindingModels.Client;
    using Olympia.Data.Models.ViewModels.AdminViewModels;
    using Olympia.Data.Models.ViewModels.BlogPartViewModels;
    using Olympia.Data.Models.ViewModels.Home;

    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;

    public interface IUsersService
    {
        Task<ClientViewModel> GetUserWithFitnessPlanModelAsync(string username);

        Task<UserProfile> GetUserProfileModelAsync(string username);

        Task<OlympiaUser> GetUserByUsernameAsync(string username);

        Task<IEnumerable<OlympiaUser>> GetAllTrainersAsync();

        Task<IEnumerable<UserViewModel>> GetAllClientsByUserAsync(string trainerUsername);

        Task<bool> SetTrainerAsync(string trainerUsername, string clientUsername);

        Task<bool> BecomeTrainerAsync(ClientToTrainerBindingModel model, string username);

        Task<OlympiaUser> GetUsersTrainerAsync(string username);

        Task<bool> UpdateUserHeightAndWeightAsync(ClientViewModel user, string username);

        Task<IEnumerable<ListedUserViewModel>> GetAllUsersAsync();

        Task<bool> DeleteUserAsync(string username);

        Task<bool> UnsetTrainerAsync(string username, string trainerUsername);

        Task<int> CalculateCaloriesAsync(string username);

        Task<bool> SetFitnessPlanToUserAsync(ClientViewModel model);

        Task UpdateProfileAsync(UserProfile model, string username);

        Task<IndexModel> GetIndexModelAsync(ClaimsPrincipal user);
    }
}
