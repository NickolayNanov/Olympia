﻿namespace Olympia.Data.Domain
{
    using Microsoft.AspNetCore.Identity;

    using Olympia.Data.Common.Models;
    using Olympia.Data.Domain.Enums;

    using System;
    using System.Collections.Generic;

    public class OlympiaUser : IdentityUser<string>, IAuditInfo, IDeletableEntity
    {
        public OlympiaUser()
        {
            this.Id = Guid.NewGuid().ToString();

            this.FitnessPlan = new FitnessPlan() { OwnerId = this.Id };
            this.ShoppingCart = new ShoppingCart(this.Id);
            this.Address = new Address() { UserId = this.Id };

            this.Clients = new HashSet<OlympiaUser>();
            this.Articles = new HashSet<Article>();
            this.Messages = new HashSet<Message>();
        }

        public OlympiaUser(string username, string email, string fullname)
        {
            this.Id = Guid.NewGuid().ToString();

            this.UserName = username;
            this.Email = email;
            this.FullName = fullname;

            this.FitnessPlan = new FitnessPlan() { OwnerId = this.Id };
            this.Address = new Address() { UserId = this.Id };
            this.ShoppingCart = new ShoppingCart(this.Id);

            this.Clients = new HashSet<OlympiaUser>();
            this.Articles = new HashSet<Article>();
            this.Messages = new HashSet<Message>();
        }

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

        public Gender Gender { get; set; }

        public int AddressId { get; set; }

        public virtual Address Address { get; set; }

        public int ShoppingCartId { get; set; }

        public ShoppingCart ShoppingCart { get; set; }

        public virtual ICollection<OlympiaUser> Clients { get; set; }

        public virtual ICollection<Article> Articles { get; set; }

        public virtual ICollection<Message> Messages { get; set; }
    }
}
