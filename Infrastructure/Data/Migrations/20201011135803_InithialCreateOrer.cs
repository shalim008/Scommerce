using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Data.Migrations
{
    public partial class InithialCreateOrer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "VariationIds",
                table: "OrderItems",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ItemOrdered_Type",
                table: "OrderItems",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ItemOrdered_VariationIds",
                table: "OrderItems",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VariationIds",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "ItemOrdered_Type",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "ItemOrdered_VariationIds",
                table: "OrderItems");
        }
    }
}
