using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Dashboard.Migrations
{
    public partial class database2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SeriNumber",
                table: "GuaranteeInProduct",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ID",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2020, 12, 8, 21, 19, 35, 867, DateTimeKind.Local).AddTicks(7595));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SeriNumber",
                table: "GuaranteeInProduct");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ID",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2020, 12, 7, 23, 32, 32, 857, DateTimeKind.Local).AddTicks(148));
        }
    }
}
