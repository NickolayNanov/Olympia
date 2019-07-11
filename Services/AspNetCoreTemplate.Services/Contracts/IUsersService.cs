namespace Olympia.Services.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Olympia.Data.Domain;

    public interface IUsersService
    {
        Task<OlympiaUser> GetUserByUsernameAsync(string username);

        Task<IEnumerable<OlympiaUser>> GetAllTrainersAsync();

        Task<IEnumerable<OlympiaUser>> GetAllClientsByUserAsync(string trainerUsername);

        Task<bool> SetTrainerAsync(string trainerUsername, string clientUsername);
    }
}
