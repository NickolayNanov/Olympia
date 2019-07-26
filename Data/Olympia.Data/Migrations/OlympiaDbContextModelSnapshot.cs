﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Olympia.Data;

namespace Olympia.Data.Migrations
{
    [DbContext(typeof(OlympiaDbContext))]
    partial class OlympiaDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("RoleId");

                    b.Property<string>("UserId");

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.HasKey("RoleId", "UserId");

                    b.HasAlternateKey("UserId", "RoleId");

                    b.ToTable("AspNetUserRoles");

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityUserRole<string>");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Olympia.Data.Domain.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Location")
                        .IsRequired();

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("Olympia.Data.Domain.Article", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AuthorId");

                    b.Property<string>("Content")
                        .IsRequired();

                    b.Property<DateTime>("CreatedOn");

                    b.Property<string>("ImgUrl");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<int>("TimesRead");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.ToTable("Articles");
                });

            modelBuilder.Entity("Olympia.Data.Domain.ChildCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedOn");

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("ChildCategories");
                });

            modelBuilder.Entity("Olympia.Data.Domain.Exercise", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int>("Sets");

                    b.Property<int>("TargetBodyPart");

                    b.Property<int?>("WorkoutId");

                    b.HasKey("Id");

                    b.HasIndex("WorkoutId");

                    b.ToTable("Exercises");
                });

            modelBuilder.Entity("Olympia.Data.Domain.FitnessPlan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CaloriesGoal");

                    b.Property<string>("OwnerId");

                    b.Property<int>("WeekWorkoutDuration");

                    b.Property<int>("WorkoutId");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId")
                        .IsUnique()
                        .HasFilter("[OwnerId] IS NOT NULL");

                    b.HasIndex("WorkoutId");

                    b.ToTable("FitnessPlans");
                });

            modelBuilder.Entity("Olympia.Data.Domain.Item", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedOn");

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<string>("ImgUrl");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<decimal>("Price");

                    b.Property<int>("SupplierId");

                    b.Property<int>("TimesBought");

                    b.HasKey("Id");

                    b.HasIndex("SupplierId");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("Olympia.Data.Domain.ItemCategory", b =>
                {
                    b.Property<int>("ItemId");

                    b.Property<int>("ChildCategoryId");

                    b.HasKey("ItemId", "ChildCategoryId");

                    b.HasIndex("ChildCategoryId");

                    b.ToTable("ItemCategories");
                });

            modelBuilder.Entity("Olympia.Data.Domain.OlympiaUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<int>("Activity");

                    b.Property<int>("Age");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<string>("Description");

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FullName");

                    b.Property<int>("Gender");

                    b.Property<double?>("Height");

                    b.Property<bool>("IsDeleted");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("ProfilePicturImgUrl");

                    b.Property<double>("Rating");

                    b.Property<string>("SecurityStamp");

                    b.Property<int>("ShoppingCartId");

                    b.Property<string>("TrainerId");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.Property<double?>("Weight");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.HasIndex("ShoppingCartId")
                        .IsUnique();

                    b.HasIndex("TrainerId");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Olympia.Data.Domain.OlympiaUserRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Olympia.Data.Domain.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedOn");

                    b.Property<string>("DeliveryAddress")
                        .IsRequired();

                    b.Property<DateTime>("ExpectedDeliveryDate");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<DateTime>("OrderDate");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Olympia.Data.Domain.OrderItem", b =>
                {
                    b.Property<int>("OrderId");

                    b.Property<int>("ItemId");

                    b.Property<decimal>("Price");

                    b.Property<int>("Quantity");

                    b.HasKey("OrderId", "ItemId");

                    b.HasIndex("ItemId");

                    b.ToTable("OrderItems");
                });

            modelBuilder.Entity("Olympia.Data.Domain.Review", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Content")
                        .IsRequired();

                    b.Property<DateTime>("CreatedOn");

                    b.Property<int>("ItemId");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<double>("Rating");

                    b.HasKey("Id");

                    b.HasIndex("ItemId");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("Olympia.Data.Domain.ShoppingCart", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("ShoppingCarts");
                });

            modelBuilder.Entity("Olympia.Data.Domain.ShoppingCartItem", b =>
                {
                    b.Property<int>("ItemId");

                    b.Property<int>("ShoppingCartId");

                    b.HasKey("ItemId", "ShoppingCartId");

                    b.HasIndex("ShoppingCartId");

                    b.ToTable("ShoppingCartItems");
                });

            modelBuilder.Entity("Olympia.Data.Domain.Supplier", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Suppliers");
                });

            modelBuilder.Entity("Olympia.Data.Domain.Workout", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedOn");

                    b.Property<string>("ImgUrl");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<string>("Name");

                    b.Property<int>("WorkoutDifficulty");

                    b.Property<int>("WorkoutType");

                    b.HasKey("Id");

                    b.ToTable("Workouts");
                });

            modelBuilder.Entity("Olympia.Data.Domain.OlympiaRolesUsers", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityUserRole<string>");

                    b.Property<string>("UserId1");

                    b.HasIndex("UserId1");

                    b.HasDiscriminator().HasValue("OlympiaRolesUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Olympia.Data.Domain.OlympiaUserRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Olympia.Data.Domain.OlympiaUser")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Olympia.Data.Domain.OlympiaUser")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Olympia.Data.Domain.OlympiaUser")
                        .WithMany("OlympiaUserRole")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Olympia.Data.Domain.OlympiaUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Olympia.Data.Domain.Address", b =>
                {
                    b.HasOne("Olympia.Data.Domain.OlympiaUser", "OlympiaUser")
                        .WithMany("Addresses")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Olympia.Data.Domain.Article", b =>
                {
                    b.HasOne("Olympia.Data.Domain.OlympiaUser", "Author")
                        .WithMany("Articles")
                        .HasForeignKey("AuthorId");
                });

            modelBuilder.Entity("Olympia.Data.Domain.Exercise", b =>
                {
                    b.HasOne("Olympia.Data.Domain.Workout")
                        .WithMany("Exercises")
                        .HasForeignKey("WorkoutId");
                });

            modelBuilder.Entity("Olympia.Data.Domain.FitnessPlan", b =>
                {
                    b.HasOne("Olympia.Data.Domain.OlympiaUser", "Owner")
                        .WithOne("FitnessPlan")
                        .HasForeignKey("Olympia.Data.Domain.FitnessPlan", "OwnerId");

                    b.HasOne("Olympia.Data.Domain.Workout", "Workout")
                        .WithMany()
                        .HasForeignKey("WorkoutId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Olympia.Data.Domain.Item", b =>
                {
                    b.HasOne("Olympia.Data.Domain.Supplier", "Supplier")
                        .WithMany("Items")
                        .HasForeignKey("SupplierId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Olympia.Data.Domain.ItemCategory", b =>
                {
                    b.HasOne("Olympia.Data.Domain.ChildCategory", "ChildCategory")
                        .WithMany("ItemCategories")
                        .HasForeignKey("ChildCategoryId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Olympia.Data.Domain.Item", "Item")
                        .WithMany("ItemCategories")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Olympia.Data.Domain.OlympiaUser", b =>
                {
                    b.HasOne("Olympia.Data.Domain.ShoppingCart", "ShoppingCart")
                        .WithOne("User")
                        .HasForeignKey("Olympia.Data.Domain.OlympiaUser", "ShoppingCartId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Olympia.Data.Domain.OlympiaUser", "Trainer")
                        .WithMany("Clients")
                        .HasForeignKey("TrainerId");
                });

            modelBuilder.Entity("Olympia.Data.Domain.Order", b =>
                {
                    b.HasOne("Olympia.Data.Domain.OlympiaUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Olympia.Data.Domain.OrderItem", b =>
                {
                    b.HasOne("Olympia.Data.Domain.Item", "Item")
                        .WithMany("OrderItems")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Olympia.Data.Domain.Order", "Order")
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Olympia.Data.Domain.Review", b =>
                {
                    b.HasOne("Olympia.Data.Domain.Item", "Item")
                        .WithMany("Reviews")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Olympia.Data.Domain.ShoppingCartItem", b =>
                {
                    b.HasOne("Olympia.Data.Domain.Item", "Item")
                        .WithMany("ShoppingCartItems")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Olympia.Data.Domain.ShoppingCart", "ShoppingCart")
                        .WithMany("ShoppingCartItems")
                        .HasForeignKey("ShoppingCartId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Olympia.Data.Domain.OlympiaRolesUsers", b =>
                {
                    b.HasOne("Olympia.Data.Domain.OlympiaUserRole", "Role")
                        .WithMany("OlympiaRolesUsers")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Olympia.Data.Domain.OlympiaUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId1");
                });
#pragma warning restore 612, 618
        }
    }
}
