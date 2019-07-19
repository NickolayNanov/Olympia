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
        IEnumerable<WorkoutViewModel> GetWorkouts(WorkoutBindingModel model);
        WorkoutViewModel GetWorkoutById(int workoutId);
        
        Task<bool> AddSupplierAsync(SupplierBindingModel model);

        IEnumerable<Supplier> GetAllSuppliers();
        IEnumerable<ItemViewModel> GetAllItems();
    }
}
