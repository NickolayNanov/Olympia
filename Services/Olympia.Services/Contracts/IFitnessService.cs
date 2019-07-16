namespace Olympia.Services.Contracts
{
    using Olympia.Data.Models.BindingModels.Client;
    using Olympia.Data.Models.ViewModels.Fitness;
    using System.Collections.Generic;

    public interface IFitnessService
    {
        IEnumerable<WorkoutViewModel> GetWorkouts(WorkoutBindingModel model);
    }
}
