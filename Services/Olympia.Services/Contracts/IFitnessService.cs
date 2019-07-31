namespace Olympia.Services.Contracts
{
    using Olympia.Data.Domain;
    using Olympia.Data.Models.BindingModels.Client;
    using Olympia.Data.Models.BindingModels.Shop;
    using Olympia.Data.Models.ViewModels.Fitness;
    using Olympia.Data.Models.ViewModels.Shop;

    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IFitnessService
    {
        Task<IEnumerable<WorkoutViewModel>> GetWorkoutsAsync(WorkoutBindingModel model);

        Task<WorkoutViewModel> GetWorkoutByIdAsync(int workoutId);

        Task<bool> AddSupplierAsync(SupplierBindingModel model);

        Task<IEnumerable<Supplier>> GetAllSuppliersAsync();

        Task<ShopViewModel> GetAllItemsAsync();

        Task<FitnessPlanViewModel> GetFitnessPlanByUsernameAsync(string username);
    }
}
