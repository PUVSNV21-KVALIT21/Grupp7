using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HakimLivs.Migrations
{
    public partial class ExpirationDateNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ExpirationDate",
                table: "Products",
                type: "smalldatetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "smalldatetime");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ExpirationDate",
                table: "Products",
                type: "smalldatetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "smalldatetime",
                oldNullable: true);
        }
    }
}
