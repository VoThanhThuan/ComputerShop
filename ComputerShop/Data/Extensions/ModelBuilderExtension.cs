using System;
using System.Collections.Generic;
using System.Text;
using Dashboard.Data.Entities;
using Dashboard.Data.Entities.Enums;
using Dashboard.Login;
using Microsoft.EntityFrameworkCore;

namespace Dashboard.Data.Extensions
{
    public static class ModelBuilderExtension
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category()
                {
                    ID = 1,
                    Name = "RAM",
                    IsShowOnHome = true,
                    ParentId = null,
                    SortOrder = 1,
                    Status = Status.Active,
                });

            modelBuilder.Entity<Product>().HasData(
                new Product()
                {
                    ID = 1,
                    Name = "RAM-SAMSUNG-256GB",
                    DateCreated = DateTime.Now,
                    OriginalPrice = 100000,
                    Price = 200000,
                    Stock = 20,
                    SeriNumber = "000-000-000-000"
                });
            modelBuilder.Entity<ProductTranslation>().HasData(
                new ProductTranslation()
                {
                    ID = 1,
                    ProductId = 1,
                    Name = "Tai Nghe",
                    Details = "Tai nghe bờ lu tút"
                });
            modelBuilder.Entity<ProductInCategory>().HasData(
                new ProductInCategory() { ProductID = 1, CategoryID = 1 });

            modelBuilder.Entity<AppRole>().HasData(new AppRole
            {
                ID = "admin",
                Name = "admin",
                Description = "Administrator role"
            });
            modelBuilder.Entity<AppUser>().HasData(new AppUser
            {
                ID = "admin",
                Username = "admin",
                Email = "voanome@gmail.com",
                PasswordHash = UserService.PasswordHash("admin"),
                FirstName = "Thuận",
                LastName = "Võ",
                Dob = new DateTime(2020, 07, 14),
                Identity = "035123456", // chứng minh nhân dân
            });
            modelBuilder.Entity<AppUserRole>().HasData(new AppUserRole
            {
                RoleID = "admin",
                UserID = "admin"
            });
        }
    }
}
