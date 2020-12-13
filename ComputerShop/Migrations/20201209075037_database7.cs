using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Dashboard.Migrations
{
    public partial class database7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ID",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2020, 12, 9, 14, 50, 35, 820, DateTimeKind.Local).AddTicks(8557));

            migrationBuilder.CreateIndex(
                name: "IX_ProductInImport_ProductID",
                table: "ProductInImport",
                column: "ProductID",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ProductInImport_ProductID",
                table: "ProductInImport");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ID",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2020, 12, 9, 10, 49, 50, 402, DateTimeKind.Local).AddTicks(1225));
        }
    }
}
