using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Dashboard.Migrations
{
    public partial class database2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ID",
                keyValue: new Guid("d4964779-d5f4-4915-9814-a1436097dcb8"));

            migrationBuilder.AddColumn<int>(
                name: "Amount",
                table: "ProductTranslations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ID", "DateCreated", "ImagePath", "Name", "OriginalPrice", "Price", "SeriNumber", "Stock" },
                values: new object[] { new Guid("da7488be-dd26-43cc-931d-21586b40c28c"), new DateTime(2020, 12, 20, 20, 30, 2, 704, DateTimeKind.Local).AddTicks(9931), null, "RAM-SAMSUNG-256GB", 100000m, 200000m, "000-000-000-000", 20 });

            migrationBuilder.UpdateData(
                table: "ProductInCategories",
                keyColumn: "ID",
                keyValue: 1,
                column: "ProductID",
                value: new Guid("da7488be-dd26-43cc-931d-21586b40c28c"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ID",
                keyValue: new Guid("da7488be-dd26-43cc-931d-21586b40c28c"));

            migrationBuilder.DropColumn(
                name: "Amount",
                table: "ProductTranslations");

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ID", "DateCreated", "ImagePath", "Name", "OriginalPrice", "Price", "SeriNumber", "Stock" },
                values: new object[] { new Guid("d4964779-d5f4-4915-9814-a1436097dcb8"), new DateTime(2020, 12, 20, 18, 56, 34, 960, DateTimeKind.Local).AddTicks(3884), null, "RAM-SAMSUNG-256GB", 100000m, 200000m, "000-000-000-000", 20 });

            migrationBuilder.UpdateData(
                table: "ProductInCategories",
                keyColumn: "ID",
                keyValue: 1,
                column: "ProductID",
                value: new Guid("d4964779-d5f4-4915-9814-a1436097dcb8"));
        }
    }
}
