namespace Olympia.Services.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Olympia.Data.Domain;

    public interface IUsersService
    {
        OlympiaUser GetUserByUsername(string username);

        Task<IEnumerable<OlympiaUser>> GetAllTrainers();

        IEnumerable<OlympiaUser> GetAllClientsByUser(string trainerUsername);
    }
}
