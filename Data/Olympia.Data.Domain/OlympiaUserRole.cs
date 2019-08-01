// ReSharper disable VirtualMemberCallInConstructor
namespace Olympia.Data.Domain
{
    using Microsoft.AspNetCore.Identity;

    using Olympia.Data.Common.Models;

    using System;

    public class OlympiaUserRole : IdentityRole<string>, IAuditInfo, IDeletableEntity
    {
        public OlympiaUserRole()
        {
        }

        public OlympiaUserRole(string name)
            : base(name)
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
