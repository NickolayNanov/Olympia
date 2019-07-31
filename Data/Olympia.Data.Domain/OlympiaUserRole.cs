// ReSharper disable VirtualMemberCallInConstructor
namespace Olympia.Data.Domain
{
    using Microsoft.AspNetCore.Identity;

    using Olympia.Data.Common.Models;

    using System;
    using System.Collections.Generic;

    public class OlympiaUserRole : IdentityRole, IAuditInfo, IDeletableEntity
    {
        public OlympiaUserRole()
            : this(null)
        {
            this.OlympiaRolesUsers = new HashSet<OlympiaRolesUsers>();
        }

        public OlympiaUserRole(string name)
            : base(name)
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public ICollection<OlympiaRolesUsers> OlympiaRolesUsers { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
