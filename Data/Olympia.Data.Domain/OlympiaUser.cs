// ReSharper disable VirtualMemberCallInConstructor
namespace Olympia.Data.Domain
{
    using System;
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Identity;
    using Olympia.Data.Common.Models;
    using Olympia.Data.Domain.Enums;

    public class OlympiaUser : IdentityUser, IAuditInfo, IDeletableEntity
    {
        public OlympiaUser()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Claims = new HashSet<IdentityUserClaim<string>>();
            this.Logins = new HashSet<IdentityUserLogin<string>>();
            this.Addresses = new HashSet<Address>();
            this.Clients = new HashSet<OlympiaUser>();
            this.OlympiaUserRole = new HashSet<IdentityUserRole<string>>();
            this.Interests = new HashSet<Interest>();            

            this.FitnessPlan = new FitnessPlan();
            this.ShoppingCart = new ShoppingCart(this.Id);
        }

        public OlympiaUser(string username, string email, string fullname)
        {
            this.Id = Guid.NewGuid().ToString();

            this.UserName = username;
            this.Email = email;
            this.FullName = fullname;

            this.ShoppingCart = new ShoppingCart(this.Id);
        }

        public Gender Gender { get; set; }

        public string FullName { get; set; }

        // Audit info
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        // Deletable entity
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public string TrainerId { get; set; }

        public virtual OlympiaUser Trainer { get; set; }

        public int Age { get; set; }

        public virtual FitnessPlan FitnessPlan { get; set; }

        public double Rating { get; set; }

        public string Description { get; set; }

        public string ProfilePicturImgUrl { get; set; }

        public double? Weight { get; set; }

        public double? Height { get; set; }

        public ActityLevel Activity { get; set; }

        public virtual ICollection<OlympiaUser> Clients { get; set; }

        public virtual ICollection<Interest> Interests { get; set; }

        public virtual ICollection<IdentityUserRole<string>> OlympiaUserRole { get; set; }

        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }

        public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }

        public virtual ICollection<Address> Addresses { get; set; }

        public virtual ICollection<Article> Articles { get; set; }

        public int ShoppingCartId { get; set; }

        public ShoppingCart ShoppingCart { get; set; }
    }
}
