using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EF_FluentAPI.Migrations
{
    public partial class RenameTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cart_Customer_CustomerId",
                table: "Cart");

            migrationBuilder.DropForeignKey(
                name: "FK_Credentials_Customer_CustomerId",
                table: "Credentials");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_Customer_CustomerId",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductCart_Cart_CartId",
                table: "ProductCart");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductCart_Products_ProductId",
                table: "ProductCart");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductOrder_Order_OrderId",
                table: "ProductOrder");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductOrder_Products_ProductId",
                table: "ProductOrder");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductOrder",
                table: "ProductOrder");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductCart",
                table: "ProductCart");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Order",
                table: "Order");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Customer",
                table: "Customer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cart",
                table: "Cart");

            migrationBuilder.RenameTable(
                name: "ProductOrder",
                newName: "ProductOrders");

            migrationBuilder.RenameTable(
                name: "ProductCart",
                newName: "ProductCarts");

            migrationBuilder.RenameTable(
                name: "Order",
                newName: "Orders");

            migrationBuilder.RenameTable(
                name: "Customer",
                newName: "Customers");

            migrationBuilder.RenameTable(
                name: "Cart",
                newName: "Carts");

            migrationBuilder.RenameIndex(
                name: "IX_ProductOrder_ProductId",
                table: "ProductOrders",
                newName: "IX_ProductOrders_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductOrder_OrderId",
                table: "ProductOrders",
                newName: "IX_ProductOrders_OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductCart_ProductId",
                table: "ProductCarts",
                newName: "IX_ProductCarts_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductCart_CartId",
                table: "ProductCarts",
                newName: "IX_ProductCarts_CartId");

            migrationBuilder.RenameIndex(
                name: "IX_Order_CustomerId",
                table: "Orders",
                newName: "IX_Orders_CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_Cart_CustomerId",
                table: "Carts",
                newName: "IX_Carts_CustomerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductOrders",
                table: "ProductOrders",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductCarts",
                table: "ProductCarts",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Orders",
                table: "Orders",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Customers",
                table: "Customers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Carts",
                table: "Carts",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_Customers_CustomerId",
                table: "Carts",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Credentials_Customers_CustomerId",
                table: "Credentials",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Customers_CustomerId",
                table: "Orders",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCarts_Carts_CartId",
                table: "ProductCarts",
                column: "CartId",
                principalTable: "Carts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCarts_Products_ProductId",
                table: "ProductCarts",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductOrders_Orders_OrderId",
                table: "ProductOrders",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductOrders_Products_ProductId",
                table: "ProductOrders",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_Customers_CustomerId",
                table: "Carts");

            migrationBuilder.DropForeignKey(
                name: "FK_Credentials_Customers_CustomerId",
                table: "Credentials");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Customers_CustomerId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductCarts_Carts_CartId",
                table: "ProductCarts");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductCarts_Products_ProductId",
                table: "ProductCarts");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductOrders_Orders_OrderId",
                table: "ProductOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductOrders_Products_ProductId",
                table: "ProductOrders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductOrders",
                table: "ProductOrders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductCarts",
                table: "ProductCarts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Orders",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Customers",
                table: "Customers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Carts",
                table: "Carts");

            migrationBuilder.RenameTable(
                name: "ProductOrders",
                newName: "ProductOrder");

            migrationBuilder.RenameTable(
                name: "ProductCarts",
                newName: "ProductCart");

            migrationBuilder.RenameTable(
                name: "Orders",
                newName: "Order");

            migrationBuilder.RenameTable(
                name: "Customers",
                newName: "Customer");

            migrationBuilder.RenameTable(
                name: "Carts",
                newName: "Cart");

            migrationBuilder.RenameIndex(
                name: "IX_ProductOrders_ProductId",
                table: "ProductOrder",
                newName: "IX_ProductOrder_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductOrders_OrderId",
                table: "ProductOrder",
                newName: "IX_ProductOrder_OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductCarts_ProductId",
                table: "ProductCart",
                newName: "IX_ProductCart_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductCarts_CartId",
                table: "ProductCart",
                newName: "IX_ProductCart_CartId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_CustomerId",
                table: "Order",
                newName: "IX_Order_CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_Carts_CustomerId",
                table: "Cart",
                newName: "IX_Cart_CustomerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductOrder",
                table: "ProductOrder",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductCart",
                table: "ProductCart",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Order",
                table: "Order",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Customer",
                table: "Customer",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cart",
                table: "Cart",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cart_Customer_CustomerId",
                table: "Cart",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Credentials_Customer_CustomerId",
                table: "Credentials",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Customer_CustomerId",
                table: "Order",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
                name: "FK_ProductOrder_Order_OrderId",
                table: "ProductOrder",
                column: "OrderId",
                principalTable: "Order",
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
    }
}
