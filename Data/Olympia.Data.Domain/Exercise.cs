namespace Olympia.Data.Domain
{
    using Olympia.Data.Common.Models;
    using Olympia.Data.Domain.Enums;

    using System;
    using System.ComponentModel.DataAnnotations;

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
