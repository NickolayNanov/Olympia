namespace Olympia.Services.Contracts
{
    using System.Collections.Generic;

    using Olympia.Data.Domain;

    public interface IUsersService
    {
        OlympiaUser GetUserByUsername(string username);

        IEnumerable<OlympiaUser> GetAllTrainers();

        IEnumerable<OlympiaUser> GetAllClientsByUser(string trainerUsername);

        bool SetTrainer(string trainerUsername, string clientUsername);
    }
}
