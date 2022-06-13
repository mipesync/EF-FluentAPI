using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EF_FluentAPI.Migrations
{
    public partial class ProdCart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductCart_Cart_CartId",
                table: "ProductCart");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductCart_Product_ProductId",
                table: "ProductCart");

            migrationBuilder.DropTable(
                name: "CartProduct");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductCart",
                table: "ProductCart");

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
                name: "PK_ProsuctCart",
                table: "ProsuctCart",
                column: "Id");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProsuctCart_Cart_CartId",
                table: "ProsuctCart");

            migrationBuilder.DropForeignKey(
                name: "FK_ProsuctCart_Product_ProductId",
                table: "ProsuctCart");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProsuctCart",
                table: "ProsuctCart");

            migrationBuilder.RenameTable(
                name: "ProsuctCart",
                newName: "ProductCart");

            migrationBuilder.RenameIndex(
                name: "IX_ProsuctCart_ProductId",
                table: "ProductCart",
                newName: "IX_ProductCart_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_ProsuctCart_CartId",
                table: "ProductCart",
                newName: "IX_ProductCart_CartId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductCart",
                table: "ProductCart",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "CartProduct",
                columns: table => new
                {
                    CartsId = table.Column<int>(type: "INTEGER", nullable: false),
                    ProductsId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartProduct", x => new { x.CartsId, x.ProductsId });
                    table.ForeignKey(
                        name: "FK_CartProduct_Cart_CartsId",
                        column: x => x.CartsId,
                        principalTable: "Cart",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartProduct_Product_ProductsId",
                        column: x => x.ProductsId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CartProduct_ProductsId",
                table: "CartProduct",
                column: "ProductsId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCart_Cart_CartId",
                table: "ProductCart",
                column: "CartId",
                principalTable: "Cart",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCart_Product_ProductId",
                table: "ProductCart",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
