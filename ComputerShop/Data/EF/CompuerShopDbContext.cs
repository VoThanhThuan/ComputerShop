using System;
using Microsoft.EntityFrameworkCore;
using Dashboard.Data.Configurations;
using Dashboard.Data.Entities;
using Dashboard.Data.Entities.Enums;
using Dashboard.Data.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Dashboard.Data.EF
{
    public class CompuerShopDbContext : DbContext
    {
        public CompuerShopDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //Configure using Fluent API
            modelBuilder.ApplyConfiguration(new CartConfiguration());

            modelBuilder.ApplyConfiguration(new AppConfigConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new ProductInCategoryConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());

            modelBuilder.ApplyConfiguration(new OrderDetailConfiguration());
            modelBuilder.ApplyConfiguration(new ProductTranslationConfiguration());
            modelBuilder.ApplyConfiguration(new PromotionConfiguration());
            modelBuilder.ApplyConfiguration(new TransactionConfiguration());

            modelBuilder.ApplyConfiguration(new AppUserConfiguration());
            modelBuilder.ApplyConfiguration(new AppRoleConfiguration());
            modelBuilder.ApplyConfiguration(new AppUserRoleConfiguration());

            modelBuilder.ApplyConfiguration(new ProductGuaranteeConfiguration());

            modelBuilder.ApplyConfiguration(new ImportConfiguration());
            modelBuilder.ApplyConfiguration(new ProductInImportConfiguration());

            //modelBuilder.Entity<IdentityUserRole<Guid>>().ToTable("AppUserRole").HasKey(x => new { x.UserId, x.RoleId });
            modelBuilder.Seed();
            //base.OnModelCreating(modelBuilder);
        }

        public DbSet<AppUser> AppUsers { get; set; }

        public DbSet<AppUserRole> AppUserRoles { get; set; }
        public DbSet<Import> Imports { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductInImport> ProductInImports { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<AppConfig> AppConfigs { get; set; }

        public DbSet<ProductGuarantee> ProductGuarantees { get; set; }
        public DbSet<Cart> Carts { get; set; }

        public DbSet<ProductInCategory> ProductInCategories { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<ProductTranslation> ProductTranslations { get; set; }

        public DbSet<Promotion> Promotions { get; set; }


        public DbSet<Transaction> Transactions { get; set; }

    }
}
