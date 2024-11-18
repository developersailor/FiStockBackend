using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FiStockBackend.Migrations
{
    /// <inheritdoc />
    public partial class kasim181626 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_StockMovements_StockMovementId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_StockMovements_Products_ProductCode",
                table: "StockMovements");

            migrationBuilder.DropIndex(
                name: "IX_StockMovements_ProductCode",
                table: "StockMovements");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Products_ProductCode",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_StockMovementId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProductCode",
                table: "StockMovements");

            migrationBuilder.DropColumn(
                name: "StockMovementId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CustomerCode",
                table: "Customers");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "StockMovements",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_StockMovements_ProductId",
                table: "StockMovements",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_StockMovements_Products_ProductId",
                table: "StockMovements",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StockMovements_Products_ProductId",
                table: "StockMovements");

            migrationBuilder.DropIndex(
                name: "IX_StockMovements_ProductId",
                table: "StockMovements");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "StockMovements");

            migrationBuilder.AddColumn<string>(
                name: "ProductCode",
                table: "StockMovements",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "StockMovementId",
                table: "Products",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CustomerCode",
                table: "Customers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Products_ProductCode",
                table: "Products",
                column: "ProductCode");

            migrationBuilder.CreateIndex(
                name: "IX_StockMovements_ProductCode",
                table: "StockMovements",
                column: "ProductCode");

            migrationBuilder.CreateIndex(
                name: "IX_Products_StockMovementId",
                table: "Products",
                column: "StockMovementId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_StockMovements_StockMovementId",
                table: "Products",
                column: "StockMovementId",
                principalTable: "StockMovements",
                principalColumn: "StockMovementId");

            migrationBuilder.AddForeignKey(
                name: "FK_StockMovements_Products_ProductCode",
                table: "StockMovements",
                column: "ProductCode",
                principalTable: "Products",
                principalColumn: "ProductCode",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
