﻿// <auto-generated />
using System;
using Dashboard.Data.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Dashboard.Migrations
{
    [DbContext(typeof(CompuerShopDbContext))]
    partial class CompuerShopDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("Dashboard.Data.Entities.AppConfig", b =>
                {
                    b.Property<string>("Key")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Key");

                    b.ToTable("AppConfigs");
                });

            modelBuilder.Entity("Dashboard.Data.Entities.AppRole", b =>
                {
                    b.Property<string>("ID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("AppRoles");

                    b.HasData(
                        new
                        {
                            ID = "admin",
                            Description = "Administrator role",
                            Name = "admin"
                        },
                        new
                        {
                            ID = "staff",
                            Description = "staff role",
                            Name = "staff"
                        },
                        new
                        {
                            ID = "dev",
                            Description = "developer role",
                            Name = "dev"
                        });
                });

            modelBuilder.Entity("Dashboard.Data.Entities.AppUser", b =>
                {
                    b.Property<string>("ID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Avatar")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Dob")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Identity")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("AppUsers");

                    b.HasData(
                        new
                        {
                            ID = "admin",
                            Dob = new DateTime(2020, 7, 14, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "voanome@gmail.com",
                            FirstName = "Thuận",
                            Identity = "035123456",
                            LastName = "Võ",
                            PasswordHash = "ISMvKXpXpadDiUoOSoAfww==",
                            Username = "admin"
                        },
                        new
                        {
                            ID = "devSon",
                            Dob = new DateTime(2020, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "sondeptrai@gmail.com",
                            FirstName = "Sơn",
                            Identity = "035123456",
                            LastName = "Nguyễn Ngọc",
                            PasswordHash = "dOc/DGes1e2AFDggnmrAhA==",
                            Username = "sondeptrai"
                        },
                        new
                        {
                            ID = "NV01",
                            Dob = new DateTime(2020, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "toan@gmail.com",
                            FirstName = "Toàn",
                            Identity = "035123456",
                            LastName = "Nguyễn Thanh",
                            PasswordHash = "cwHuoXLomiI3mEZ9SpHnqQ==",
                            Username = "toan"
                        });
                });

            modelBuilder.Entity("Dashboard.Data.Entities.AppUserRole", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("RoleID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserID")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("AppUserRole");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            RoleID = "admin",
                            UserID = "admin"
                        },
                        new
                        {
                            ID = 2,
                            RoleID = "admin",
                            UserID = "devSon"
                        },
                        new
                        {
                            ID = 3,
                            RoleID = "staff",
                            UserID = "NV01"
                        });
                });

            modelBuilder.Entity("Dashboard.Data.Entities.Cart", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<string>("UserID")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("ID");

                    b.HasIndex("ProductId");

                    b.HasIndex("UserID");

                    b.ToTable("Carts");
                });

            modelBuilder.Entity("Dashboard.Data.Entities.Category", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<bool>("IsShowOnHome")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int?>("ParentId")
                        .HasColumnType("int");

                    b.Property<int>("SortOrder")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(1);

                    b.HasKey("ID");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            IsShowOnHome = true,
                            Name = "RAM",
                            SortOrder = 1,
                            Status = 1
                        });
                });

            modelBuilder.Entity("Dashboard.Data.Entities.Import", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("AppUserID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("DayImport")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("SecurityCode")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Stock")
                        .HasColumnType("int");

                    b.Property<string>("Supplier")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Warehouse")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("AppUserID");

                    b.ToTable("Import");
                });

            modelBuilder.Entity("Dashboard.Data.Entities.Order", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ShipAddress")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("ShipEmail")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("ShipName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("ShipPhoneNumber")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("ID");

                    b.HasIndex("UserId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Dashboard.Data.Entities.OrderDetail", b =>
                {
                    b.Property<int>("OrderID")
                        .HasColumnType("int");

                    b.Property<int>("ProductID")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("OrderID", "ProductID");

                    b.HasIndex("ProductID");

                    b.ToTable("OrderDetails");
                });

            modelBuilder.Entity("Dashboard.Data.Entities.Product", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<string>("ImagePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("OriginalPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("SeriNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Stock")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.HasKey("ID");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            DateCreated = new DateTime(2020, 12, 20, 16, 11, 21, 788, DateTimeKind.Local).AddTicks(6438),
                            Name = "RAM-SAMSUNG-256GB",
                            OriginalPrice = 100000m,
                            Price = 200000m,
                            SeriNumber = "000-000-000-000",
                            Stock = 20
                        });
                });

            modelBuilder.Entity("Dashboard.Data.Entities.ProductGuarantee", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("DateOfPurchase")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ExpirationDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<string>("SeriNumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("ProductId")
                        .IsUnique();

                    b.ToTable("ProductGuarantee");
                });

            modelBuilder.Entity("Dashboard.Data.Entities.ProductInCategory", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("CategoryID")
                        .HasColumnType("int");

                    b.Property<int>("ProductID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("CategoryID");

                    b.HasIndex("ProductID");

                    b.ToTable("ProductInCategories");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            CategoryID = 1,
                            ProductID = 1
                        });
                });

            modelBuilder.Entity("Dashboard.Data.Entities.ProductInImport", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("ImportID")
                        .HasColumnType("int");

                    b.Property<int>("ProductID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("ImportID");

                    b.HasIndex("ProductID")
                        .IsUnique();

                    b.ToTable("ProductInImport");
                });

            modelBuilder.Entity("Dashboard.Data.Entities.ProductTranslation", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("TransactionID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("ProductId");

                    b.HasIndex("TransactionID");

                    b.ToTable("ProductTranslations");
                });

            modelBuilder.Entity("Dashboard.Data.Entities.Promotion", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<bool>("ApplyForAll")
                        .HasColumnType("bit");

                    b.Property<decimal?>("DiscountAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("DiscountPercent")
                        .HasColumnType("int");

                    b.Property<DateTime>("FromDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProductCategoryIds")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProductIds")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ToDate")
                        .HasColumnType("datetime2");

                    b.HasKey("ID");

                    b.ToTable("Promotions");
                });

            modelBuilder.Entity("Dashboard.Data.Entities.Transaction", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Fee")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("NameStaff")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("TransactionDate")
                        .HasColumnType("datetime2");

                    b.HasKey("ID");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("Dashboard.Data.Entities.Cart", b =>
                {
                    b.HasOne("Dashboard.Data.Entities.Product", "Product")
                        .WithMany("Carts")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Dashboard.Data.Entities.AppUser", "AppUser")
                        .WithMany("Carts")
                        .HasForeignKey("UserID");

                    b.Navigation("AppUser");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Dashboard.Data.Entities.Import", b =>
                {
                    b.HasOne("Dashboard.Data.Entities.AppUser", null)
                        .WithMany("ImportGoods")
                        .HasForeignKey("AppUserID");
                });

            modelBuilder.Entity("Dashboard.Data.Entities.Order", b =>
                {
                    b.HasOne("Dashboard.Data.Entities.AppUser", "AppUser")
                        .WithMany("Orders")
                        .HasForeignKey("UserId");

                    b.Navigation("AppUser");
                });

            modelBuilder.Entity("Dashboard.Data.Entities.OrderDetail", b =>
                {
                    b.HasOne("Dashboard.Data.Entities.Order", "Order")
                        .WithMany("OrderDetails")
                        .HasForeignKey("OrderID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Dashboard.Data.Entities.Product", "Product")
                        .WithMany("OrderDetails")
                        .HasForeignKey("ProductID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Dashboard.Data.Entities.ProductGuarantee", b =>
                {
                    b.HasOne("Dashboard.Data.Entities.Product", "Product")
                        .WithOne("ProductGuarantee")
                        .HasForeignKey("Dashboard.Data.Entities.ProductGuarantee", "ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Dashboard.Data.Entities.ProductInCategory", b =>
                {
                    b.HasOne("Dashboard.Data.Entities.Category", "Category")
                        .WithMany("ProductInCategories")
                        .HasForeignKey("CategoryID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Dashboard.Data.Entities.Product", "Product")
                        .WithMany("ProductInCategories")
                        .HasForeignKey("ProductID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Dashboard.Data.Entities.ProductInImport", b =>
                {
                    b.HasOne("Dashboard.Data.Entities.Import", "Import")
                        .WithMany("ProductInImports")
                        .HasForeignKey("ImportID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Dashboard.Data.Entities.Product", "Product")
                        .WithOne("ProductInImports")
                        .HasForeignKey("Dashboard.Data.Entities.ProductInImport", "ProductID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Import");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Dashboard.Data.Entities.ProductTranslation", b =>
                {
                    b.HasOne("Dashboard.Data.Entities.Product", "Product")
                        .WithMany("ProductTranslations")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Dashboard.Data.Entities.Transaction", "Transaction")
                        .WithMany("ProductTranslations")
                        .HasForeignKey("TransactionID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("Transaction");
                });

            modelBuilder.Entity("Dashboard.Data.Entities.AppUser", b =>
                {
                    b.Navigation("Carts");

                    b.Navigation("ImportGoods");

                    b.Navigation("Orders");
                });

            modelBuilder.Entity("Dashboard.Data.Entities.Category", b =>
                {
                    b.Navigation("ProductInCategories");
                });

            modelBuilder.Entity("Dashboard.Data.Entities.Import", b =>
                {
                    b.Navigation("ProductInImports");
                });

            modelBuilder.Entity("Dashboard.Data.Entities.Order", b =>
                {
                    b.Navigation("OrderDetails");
                });

            modelBuilder.Entity("Dashboard.Data.Entities.Product", b =>
                {
                    b.Navigation("Carts");

                    b.Navigation("OrderDetails");

                    b.Navigation("ProductGuarantee");

                    b.Navigation("ProductInCategories");

                    b.Navigation("ProductInImports");

                    b.Navigation("ProductTranslations");
                });

            modelBuilder.Entity("Dashboard.Data.Entities.Transaction", b =>
                {
                    b.Navigation("ProductTranslations");
                });
#pragma warning restore 612, 618
        }
    }
}
