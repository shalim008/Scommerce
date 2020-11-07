using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Data.Migrations
{
    public partial class ProductTypeTableChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BrandsJson",
                table: "ProductTypes",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ParentProductId",
                table: "ProductTypes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "VendorRegistrationId",
                table: "ProductTypes",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LowStockThreshold",
                table: "Products",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BrandsJson",
                table: "ProductTypes");

            migrationBuilder.DropColumn(
                name: "ParentProductId",
                table: "ProductTypes");

            migrationBuilder.DropColumn(
                name: "VendorRegistrationId",
                table: "ProductTypes");

            migrationBuilder.DropColumn(
                name: "LowStockThreshold",
                table: "Products");
        }
    }
}
