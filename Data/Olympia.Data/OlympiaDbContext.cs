namespace Olympia.Data
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Olympia.Data.Common.Models;
    using Olympia.Data.Domain;

    public class OlympiaDbContext : IdentityDbContext<OlympiaUser, OlympiaUserRole, string>
    {
        public OlympiaDbContext(DbContextOptions<OlympiaDbContext> options)
            : base(options)
        {
        }

        public OlympiaDbContext()
        {
        }

        public DbSet<OlympiaRolesUsers> OlympiaRolesUsers { get; set; }

        public DbSet<OlympiaUser> Users { get; set; }

        public DbSet<Article> Articles { get; set; }

        public DbSet<Address> Addresses { get; set; }

        public DbSet<ChildCategory> ChildCategories { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Exercise> Exercises { get; set; }

        public DbSet<FitnessPlan> FitnessPlans { get; set; }

        public DbSet<Interest> Interests { get; set; }

        public DbSet<Item> Items { get; set; }

        public DbSet<ItemCategory> ItemCategories { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderItem> OrderItems { get; set; }

        public DbSet<Review> Reviews { get; set; }

        public DbSet<ShoppingCart> ShoppingCarts { get; set; }

        public DbSet<Supplier> Suppliers { get; set; }

        public DbSet<OlympiaUser> Trainers { get; set; }

        public DbSet<Workout> Workouts { get; set; }

        public override int SaveChanges() => this.SaveChanges(true);

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            this.ApplyAuditInfoRules();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
            this.SaveChangesAsync(true, cancellationToken);

        public override Task<int> SaveChangesAsync(
            bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = default)
        {
            this.ApplyAuditInfoRules();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            ConfigureUserIdentityRelations(modelBuilder);

            var entityTypes = modelBuilder.Model.GetEntityTypes().ToList();

            modelBuilder.Entity<ItemCategory>()
               .HasKey(fk => new { fk.ItemId, fk.ChildCategoryId });

            modelBuilder.Entity<Item>()
                 .HasMany(item => item.ItemCategories)
                 .WithOne(itemCategory => itemCategory.Item)
                 .HasForeignKey(fk => fk.ItemId)
                 .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ChildCategory>()
                 .HasMany(childCategory => childCategory.ItemCategories)
                 .WithOne(itemCategory => itemCategory.ChildCategory)
                 .HasForeignKey(fk => fk.ChildCategoryId)
                 .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<OrderItem>()
               .HasKey(pk => new { pk.OrderId, pk.ItemId });

            modelBuilder.Entity<Item>()
                .HasMany(item => item.OrderItems)
                .WithOne(orderItem => orderItem.Item)
                .HasForeignKey(fk => fk.ItemId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Order>()
                .HasMany(order => order.OrderItems)
                .WithOne(orderItem => orderItem.Order)
                .HasForeignKey(fk => fk.OrderId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<OlympiaUser>()
                .HasMany(user => user.Addresses)
                .WithOne(adress => adress.OlympiaUser)
                .HasForeignKey(fk => fk.UserId);

            modelBuilder.Entity<OlympiaUser>()
                .HasIndex(x => x.ShoppingCartId)
                .IsUnique();

            modelBuilder.Entity<OlympiaUser>()
                .HasOne(user => user.ShoppingCart)
                .WithOne(shoppingCart => shoppingCart.User)
                .HasForeignKey<OlympiaUser>(user => user.ShoppingCartId);
        }

        private static void ConfigureUserIdentityRelations(ModelBuilder builder)
        {
            builder.Entity<OlympiaUser>()
                .HasMany(e => e.Claims)
                .WithOne()
                .HasForeignKey(e => e.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<OlympiaUser>()
                .HasMany(e => e.Logins)
                .WithOne()
                .HasForeignKey(e => e.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<OlympiaUser>()
                .HasMany(u => u.OlympiaUserRole)
                .WithOne()
                .HasForeignKey(fk => fk.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<OlympiaUserRole>()
                .HasMany(r => r.OlympiaRolesUsers)
                .WithOne(oru => oru.Role)
                .HasForeignKey(fk => fk.RoleId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);


            builder.Entity<IdentityUserRole<string>>()
                .HasKey(fk => new { fk.RoleId, fk.UserId });
        }

        private void ApplyAuditInfoRules()
        {
            var changedEntries = this.ChangeTracker
                .Entries()
                .Where(e =>
                    e.Entity is IAuditInfo &&
                    (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entry in changedEntries)
            {
                var entity = (IAuditInfo)entry.Entity;

                if (entry.State == EntityState.Added && entity.CreatedOn == default)
                {
                    entity.CreatedOn = DateTime.UtcNow;
                }
                else
                {
                    entity.ModifiedOn = DateTime.UtcNow;
                }
            }
        }
    }
}
