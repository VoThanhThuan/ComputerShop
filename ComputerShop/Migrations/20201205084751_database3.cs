using Microsoft.EntityFrameworkCore.Migrations;

namespace Dashboard.Migrations
{
    public partial class database3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AppUserRole",
                table: "AppUserRole");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppUserRole",
                table: "AppUserRole",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AppUserRole",
                table: "AppUserRole");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppUserRole",
                table: "AppUserRole",
                columns: new[] { "UserId", "RoleId" });
        }
    }
}
