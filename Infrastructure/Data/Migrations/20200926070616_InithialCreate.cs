using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Data.Migrations
{
    public partial class InithialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DeliveryMethods",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DataStatus = table.Column<int>(nullable: false),
                    SetOn = table.Column<long>(nullable: false),
                    ModifiedOn = table.Column<long>(nullable: false),
                    ShortName = table.Column<string>(nullable: true),
                    DeliveryTime = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Price = table.Column<double>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryMethods", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductBrands",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DataStatus = table.Column<int>(nullable: false),
                    SetOn = table.Column<long>(nullable: false),
                    ModifiedOn = table.Column<long>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductBrands", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DataStatus = table.Column<int>(nullable: false),
                    SetOn = table.Column<long>(nullable: false),
                    ModifiedOn = table.Column<long>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DataStatus = table.Column<int>(nullable: false),
                    SetOn = table.Column<long>(nullable: false),
                    ModifiedOn = table.Column<long>(nullable: false),
                    BuyerEmail = table.Column<string>(nullable: true),
                    OrderDate = table.Column<long>(nullable: false),
                    ShipToAddress_FirstName = table.Column<string>(nullable: true),
                    ShipToAddress_LastName = table.Column<string>(nullable: true),
                    ShipToAddress_Street = table.Column<string>(nullable: true),
                    ShipToAddress_City = table.Column<string>(nullable: true),
                    ShipToAddress_State = table.Column<string>(nullable: true),
                    ShipToAddress_Zipcode = table.Column<string>(nullable: true),
                    DeliveryMethodId = table.Column<int>(nullable: true),
                    Subtotal = table.Column<double>(nullable: false),
                    Status = table.Column<string>(nullable: false),
                    PaymentIntentId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_DeliveryMethods_DeliveryMethodId",
                        column: x => x.DeliveryMethodId,
                        principalTable: "DeliveryMethods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DataStatus = table.Column<int>(nullable: false),
                    SetOn = table.Column<long>(nullable: false),
                    ModifiedOn = table.Column<long>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Description = table.Column<string>(maxLength: 180, nullable: false),
                    Price = table.Column<double>(type: "decimal(18,2)", nullable: false),
                    RegularPrice = table.Column<double>(type: "decimal(18,2)", nullable: false, defaultValue: 0.0),
                    IsManageStock = table.Column<bool>(nullable: false, defaultValue: false),
                    IsDownloadable = table.Column<bool>(nullable: false, defaultValue: false),
                    ProductDownloadUrl = table.Column<string>(nullable: true),
                    ProductDownloadUrlExpireOn = table.Column<long>(nullable: false),
                    ProductDownloadLimit = table.Column<int>(nullable: false),
                    IsVirtual = table.Column<bool>(nullable: false, defaultValue: false),
                    StockQuantity = table.Column<double>(type: "decimal(18,2)", nullable: false),
                    ProductWeight = table.Column<double>(type: "decimal(18,2)", nullable: false),
                    ProductLength = table.Column<double>(type: "decimal(18,2)", nullable: false),
                    ProductWidth = table.Column<double>(type: "decimal(18,2)", nullable: false),
                    ProductHeight = table.Column<double>(type: "decimal(18,2)", nullable: false),
                    PictureUrl = table.Column<string>(nullable: false),
                    ProductTypeId = table.Column<int>(nullable: false),
                    ProductBrandId = table.Column<int>(nullable: false),
                    HasVariation = table.Column<bool>(nullable: false, defaultValue: false),
                    ProductSKU = table.Column<string>(maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_ProductBrands_ProductBrandId",
                        column: x => x.ProductBrandId,
                        principalTable: "ProductBrands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_ProductTypes_ProductTypeId",
                        column: x => x.ProductTypeId,
                        principalTable: "ProductTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DataStatus = table.Column<int>(nullable: false),
                    SetOn = table.Column<long>(nullable: false),
                    ModifiedOn = table.Column<long>(nullable: false),
                    ItemOrdered_ProductItemId = table.Column<int>(nullable: true),
                    ItemOrdered_ProductName = table.Column<string>(nullable: true),
                    ItemOrdered_PictureUrl = table.Column<string>(nullable: true),
                    Price = table.Column<double>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    OrderId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductAttribute",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DataStatus = table.Column<int>(nullable: false),
                    SetOn = table.Column<long>(nullable: false),
                    ModifiedOn = table.Column<long>(nullable: false),
                    AttributeName = table.Column<string>(nullable: true),
                    IsVisibleOnProductPage = table.Column<bool>(nullable: false),
                    IsUsedForVariations = table.Column<bool>(nullable: false),
                    ProductId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductAttribute", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductAttribute_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductImages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    DataStatus = table.Column<int>(nullable: false),
                    SetOn = table.Column<long>(nullable: false),
                    ModifiedOn = table.Column<long>(nullable: false),
                    NameAlt = table.Column<string>(maxLength: 100, nullable: false),
                    ImageUrl = table.Column<string>(maxLength: 180, nullable: false),
                    ImageThumbUrl = table.Column<string>(maxLength: 180, nullable: false),
                    ImageBigUrl = table.Column<string>(maxLength: 180, nullable: false),
                    IsRelativeUrl = table.Column<bool>(nullable: false),
                    ProductId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductImages_Products_Id",
                        column: x => x.Id,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductAttributeValues",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DataStatus = table.Column<int>(nullable: false),
                    SetOn = table.Column<long>(nullable: false),
                    ModifiedOn = table.Column<long>(nullable: false),
                    AttributeValueName = table.Column<string>(nullable: true),
                    AttributeValueParentId = table.Column<int>(nullable: false),
                    ProductAttributeId = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Price = table.Column<double>(nullable: false),
                    RegularPrice = table.Column<double>(nullable: false),
                    IsManageStock = table.Column<bool>(nullable: false),
                    IsDownloadable = table.Column<bool>(nullable: false),
                    ProductDownloadUrl = table.Column<string>(nullable: true),
                    ProductDownloadUrlExpireOn = table.Column<long>(nullable: false),
                    ProductDownloadLimit = table.Column<int>(nullable: false),
                    IsVirtual = table.Column<bool>(nullable: false),
                    StockQuantity = table.Column<double>(nullable: false),
                    ProductWeight = table.Column<double>(nullable: false),
                    ProductLength = table.Column<double>(nullable: false),
                    ProductWidth = table.Column<double>(nullable: false),
                    ProductHeight = table.Column<double>(nullable: false),
                    PictureUrl = table.Column<string>(nullable: true),
                    ProductSKU = table.Column<string>(nullable: true),
                    ProductId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductAttributeValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductAttributeValues_ProductAttribute_ProductAttributeId",
                        column: x => x.ProductAttributeId,
                        principalTable: "ProductAttribute",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductAttributeValues_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductVariation",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DataStatus = table.Column<int>(nullable: false),
                    SetOn = table.Column<long>(nullable: false),
                    ModifiedOn = table.Column<long>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: true),
                    Description = table.Column<string>(maxLength: 180, nullable: true),
                    Price = table.Column<double>(type: "decimal(18,2)", nullable: false),
                    RegularPrice = table.Column<double>(type: "decimal(18,2)", nullable: false, defaultValue: 0.0),
                    StockQuantity = table.Column<double>(type: "decimal(18,2)", nullable: false),
                    ProductWeight = table.Column<double>(type: "decimal(18,2)", nullable: false),
                    ProductLength = table.Column<double>(type: "decimal(18,2)", nullable: false),
                    ProductWidth = table.Column<double>(type: "decimal(18,2)", nullable: false),
                    ProductHeight = table.Column<double>(type: "decimal(18,2)", nullable: false),
                    PictureUrl = table.Column<string>(nullable: false),
                    ProductSKU = table.Column<string>(maxLength: 20, nullable: true),
                    ProductId = table.Column<int>(nullable: false),
                    ProductAttributeId = table.Column<int>(nullable: false),
                    ProductAttributeValuesId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductVariation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductVariation_ProductAttribute_ProductAttributeId",
                        column: x => x.ProductAttributeId,
                        principalTable: "ProductAttribute",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductVariation_ProductAttributeValues_ProductAttributeId",
                        column: x => x.ProductAttributeId,
                        principalTable: "ProductAttributeValues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductVariation_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_DeliveryMethodId",
                table: "Orders",
                column: "DeliveryMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttribute_ProductId",
                table: "ProductAttribute",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttributeValues_ProductAttributeId",
                table: "ProductAttributeValues",
                column: "ProductAttributeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttributeValues_ProductId",
                table: "ProductAttributeValues",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductBrandId",
                table: "Products",
                column: "ProductBrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductTypeId",
                table: "Products",
                column: "ProductTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductVariation_ProductAttributeId",
                table: "ProductVariation",
                column: "ProductAttributeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductVariation_ProductId",
                table: "ProductVariation",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "ProductImages");

            migrationBuilder.DropTable(
                name: "ProductVariation");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "ProductAttributeValues");

            migrationBuilder.DropTable(
                name: "DeliveryMethods");

            migrationBuilder.DropTable(
                name: "ProductAttribute");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "ProductBrands");

            migrationBuilder.DropTable(
                name: "ProductTypes");
        }
    }
}
