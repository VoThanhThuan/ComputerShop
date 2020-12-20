using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Dashboard.Migrations
{
    public partial class database1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ID",
                keyValue: new Guid("c48b7b0e-f1e6-41db-b544-a6d90242505d"));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "ID",
                keyValue: 1,
                column: "Name",
                value: "ALL");

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ID", "DateCreated", "ImagePath", "Name", "OriginalPrice", "Price", "SeriNumber", "Stock" },
                values: new object[] { new Guid("e434deb8-e54e-4ab2-9d47-cb5c7b29cbab"), new DateTime(2020, 12, 20, 18, 35, 27, 585, DateTimeKind.Local).AddTicks(9564), null, "RAM-SAMSUNG-256GB", 100000m, 200000m, "000-000-000-000", 20 });

            migrationBuilder.UpdateData(
                table: "ProductInCategories",
                keyColumn: "ID",
                keyValue: 1,
                column: "ProductID",
                value: new Guid("e434deb8-e54e-4ab2-9d47-cb5c7b29cbab"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ID",
                keyValue: new Guid("e434deb8-e54e-4ab2-9d47-cb5c7b29cbab"));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "ID",
                keyValue: 1,
                column: "Name",
                value: "RAM");

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ID", "DateCreated", "ImagePath", "Name", "OriginalPrice", "Price", "SeriNumber", "Stock" },
                values: new object[] { new Guid("c48b7b0e-f1e6-41db-b544-a6d90242505d"), new DateTime(2020, 12, 20, 18, 11, 19, 578, DateTimeKind.Local).AddTicks(1135), null, "RAM-SAMSUNG-256GB", 100000m, 200000m, "000-000-000-000", 20 });

            migrationBuilder.UpdateData(
                table: "ProductInCategories",
                keyColumn: "ID",
                keyValue: 1,
                column: "ProductID",
                value: new Guid("c48b7b0e-f1e6-41db-b544-a6d90242505d"));
        }
    }
}
