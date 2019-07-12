namespace Olympia.Data.Domain
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Olympia.Data.Common.Models;

    public class DietPlan : BaseModel<int>
    {
        public DietPlan()
        {
            this.CreatedOn = DateTime.UtcNow;
        }

        [Required]
        public int CaloriesGoal { get; set; }
    }
}
