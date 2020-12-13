using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Dashboard.Migrations
{
    public partial class database5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Stock",
                table: "ProductInImport");

            migrationBuilder.AddColumn<int>(
                name: "Stock",
                table: "Import",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ID",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2020, 12, 9, 10, 19, 4, 190, DateTimeKind.Local).AddTicks(1318));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Stock",
                table: "Import");

            migrationBuilder.AddColumn<int>(
                name: "Stock",
                table: "ProductInImport",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Import",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ID",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2020, 12, 8, 23, 39, 47, 579, DateTimeKind.Local).AddTicks(6535));
        }
    }
}
