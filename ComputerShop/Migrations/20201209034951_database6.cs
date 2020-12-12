using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Dashboard.Migrations
{
    public partial class database6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GuaranteeInProduct");

            migrationBuilder.DropTable(
                name: "Guarantee");

            migrationBuilder.CreateTable(
                name: "ProductGuarantee",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateOfPurchase = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SeriNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductGuarantee", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ProductGuarantee_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ID",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2020, 12, 9, 10, 49, 50, 402, DateTimeKind.Local).AddTicks(1225));

            migrationBuilder.CreateIndex(
                name: "IX_ProductGuarantee_ProductId",
                table: "ProductGuarantee",
                column: "ProductId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductGuarantee");

            migrationBuilder.CreateTable(
                name: "Guarantee",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Guarantee", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "GuaranteeInProduct",
                columns: table => new
                {
                    ProductID = table.Column<int>(type: "int", nullable: false),
                    GuaranteeID = table.Column<int>(type: "int", nullable: false),
                    DateOfPurchase = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SeriNumber = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GuaranteeInProduct", x => new { x.ProductID, x.GuaranteeID });
                    table.ForeignKey(
                        name: "FK_GuaranteeInProduct_Guarantee_GuaranteeID",
                        column: x => x.GuaranteeID,
                        principalTable: "Guarantee",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GuaranteeInProduct_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ID",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2020, 12, 9, 10, 19, 4, 190, DateTimeKind.Local).AddTicks(1318));

            migrationBuilder.CreateIndex(
                name: "IX_GuaranteeInProduct_GuaranteeID",
                table: "GuaranteeInProduct",
                column: "GuaranteeID");
        }
    }
}
