using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Dashboard.Migrations
{
    public partial class database4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Supplier",
                table: "Import",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Warehouse",
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Supplier",
                table: "Import");

            migrationBuilder.DropColumn(
                name: "Warehouse",
                table: "Import");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ID",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2020, 12, 8, 21, 26, 6, 428, DateTimeKind.Local).AddTicks(576));
        }
    }
}
