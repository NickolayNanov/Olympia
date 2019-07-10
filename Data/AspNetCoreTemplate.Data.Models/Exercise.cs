namespace Olympia.Data.Domain
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Olympia.Data.Common.Models;
    using Olympia.Data.Domain.Enums;

    public class Exercise : BaseModel<int>
    {
        public Exercise()
        {
            this.CreatedOn = DateTime.UtcNow;
        }

        [Required]
        public string Name { get; set; }

        [Required]
        public BodyPart TargetBodyPart { get; set; }

        [Required]
        public int Sets { get; set; }
    }
}
