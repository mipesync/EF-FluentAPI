using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EF_FluentAPI.Migrations
{
    public partial class ProdOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductOrder_Product_ProductId",
                table: "ProductOrder");

            migrationBuilder.DropForeignKey(
                name: "FK_ProsuctCart_Cart_CartId",
                table: "ProsuctCart");

            migrationBuilder.DropForeignKey(
                name: "FK_ProsuctCart_Product_ProductId",
                table: "ProsuctCart");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductOrder",
                table: "ProductOrder");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProsuctCart",
                table: "ProsuctCart");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Product",
                table: "Product");

            migrationBuilder.RenameTable(
                name: "ProsuctCart",
                newName: "ProductCart");

            migrationBuilder.RenameTable(
                name: "Product",
                newName: "Products");

            migrationBuilder.RenameIndex(
                name: "IX_ProsuctCart_ProductId",
                table: "ProductCart",
                newName: "IX_ProductCart_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_ProsuctCart_CartId",
                table: "ProductCart",
                newName: "IX_ProductCart_CartId");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ProductOrder",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0)
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductOrder",
                table: "ProductOrder",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductCart",
                table: "ProductCart",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Products",
                table: "Products",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ProductOrder_ProductId",
                table: "ProductOrder",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCart_Cart_CartId",
                table: "ProductCart",
                column: "CartId",
                principalTable: "Cart",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCart_Products_ProductId",
                table: "ProductCart",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductOrder_Products_ProductId",
                table: "ProductOrder",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductCart_Cart_CartId",
                table: "ProductCart");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductCart_Products_ProductId",
                table: "ProductCart");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductOrder_Products_ProductId",
                table: "ProductOrder");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductOrder",
                table: "ProductOrder");

            migrationBuilder.DropIndex(
                name: "IX_ProductOrder_ProductId",
                table: "ProductOrder");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Products",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductCart",
                table: "ProductCart");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ProductOrder");

            migrationBuilder.RenameTable(
                name: "Products",
                newName: "Product");

            migrationBuilder.RenameTable(
                name: "ProductCart",
                newName: "ProsuctCart");

            migrationBuilder.RenameIndex(
                name: "IX_ProductCart_ProductId",
                table: "ProsuctCart",
                newName: "IX_ProsuctCart_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductCart_CartId",
                table: "ProsuctCart",
                newName: "IX_ProsuctCart_CartId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductOrder",
                table: "ProductOrder",
                columns: new[] { "ProductId", "OrderId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Product",
                table: "Product",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProsuctCart",
                table: "ProsuctCart",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductOrder_Product_ProductId",
                table: "ProductOrder",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProsuctCart_Cart_CartId",
                table: "ProsuctCart",
                column: "CartId",
                principalTable: "Cart",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProsuctCart_Product_ProductId",
                table: "ProsuctCart",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
