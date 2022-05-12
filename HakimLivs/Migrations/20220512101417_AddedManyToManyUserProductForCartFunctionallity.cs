using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HakimLivs.Migrations
{
    public partial class AddedManyToManyUserProductForCartFunctionallity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppUserProduct",
                columns: table => new
                {
                    AppUsersId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProductsID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserProduct", x => new { x.AppUsersId, x.ProductsID });
                    table.ForeignKey(
                        name: "FK_AppUserProduct_AspNetUsers_AppUsersId",
                        column: x => x.AppUsersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppUserProduct_Products_ProductsID",
                        column: x => x.ProductsID,
                        principalTable: "Products",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppUserProduct_ProductsID",
                table: "AppUserProduct",
                column: "ProductsID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppUserProduct");
        }
    }
}
