namespace Olympia.Data.Models.ViewModels
{ 
    using Olympia.Data.Domain;

    using System.Collections.Generic;

    public class UsernamesAndTrainerNameModel
    {
        public IEnumerable<OlympiaUser> Trainers { get; set; }

        public string TrainerName { get; set; }
    }
}
