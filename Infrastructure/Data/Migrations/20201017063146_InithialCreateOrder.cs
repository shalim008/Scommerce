using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Data.Migrations
{
    public partial class InithialCreateOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DeliveryFrom",
                table: "Products",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeliveryTime",
                table: "Products",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductRating",
                table: "Products",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeliveryFrom",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "DeliveryTime",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProductRating",
                table: "Products");
        }
    }
}
