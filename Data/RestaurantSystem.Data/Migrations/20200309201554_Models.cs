using Microsoft.EntityFrameworkCore.Migrations;

namespace RestaurantSystem.Data.Migrations
{
    public partial class Models : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Item_Menus_MenuId",
                table: "Item");

            migrationBuilder.DropForeignKey(
                name: "FK_Item_Order_OrderId",
                table: "Item");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_AspNetUsers_UserId",
                table: "Order");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Order",
                table: "Order");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Item",
                table: "Item");

            migrationBuilder.RenameTable(
                name: "Order",
                newName: "Orders");

            migrationBuilder.RenameTable(
                name: "Item",
                newName: "Items");

            migrationBuilder.RenameIndex(
                name: "IX_Order_UserId",
                table: "Orders",
                newName: "IX_Orders_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Order_IsDeleted",
                table: "Orders",
                newName: "IX_Orders_IsDeleted");

            migrationBuilder.RenameIndex(
                name: "IX_Item_OrderId",
                table: "Items",
                newName: "IX_Items_OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_Item_MenuId",
                table: "Items",
                newName: "IX_Items_MenuId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Orders",
                table: "Orders",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Items",
                table: "Items",
                column: "ItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Menus_MenuId",
                table: "Items",
                column: "MenuId",
                principalTable: "Menus",
                principalColumn: "MenuId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Orders_OrderId",
                table: "Items",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AspNetUsers_UserId",
                table: "Orders",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Menus_MenuId",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_Orders_OrderId",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AspNetUsers_UserId",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Orders",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Items",
                table: "Items");

            migrationBuilder.RenameTable(
                name: "Orders",
                newName: "Order");

            migrationBuilder.RenameTable(
                name: "Items",
                newName: "Item");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_UserId",
                table: "Order",
                newName: "IX_Order_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_IsDeleted",
                table: "Order",
                newName: "IX_Order_IsDeleted");

            migrationBuilder.RenameIndex(
                name: "IX_Items_OrderId",
                table: "Item",
                newName: "IX_Item_OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_Items_MenuId",
                table: "Item",
                newName: "IX_Item_MenuId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Order",
                table: "Order",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Item",
                table: "Item",
                column: "ItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Item_Menus_MenuId",
                table: "Item",
                column: "MenuId",
                principalTable: "Menus",
                principalColumn: "MenuId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Item_Order_OrderId",
                table: "Item",
                column: "OrderId",
                principalTable: "Order",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_AspNetUsers_UserId",
                table: "Order",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
