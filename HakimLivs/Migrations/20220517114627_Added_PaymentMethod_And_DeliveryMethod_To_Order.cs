using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HakimLivs.Migrations
{
    public partial class Added_PaymentMethod_And_DeliveryMethod_To_Order : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DeliveryMethod",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PaymentMethod",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeliveryMethod",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "PaymentMethod",
                table: "Orders");
        }
    }
}
