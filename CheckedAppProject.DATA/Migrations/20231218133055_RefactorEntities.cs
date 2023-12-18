using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CheckedAppProject.DATA.Migrations
{
    /// <inheritdoc />
    public partial class RefactorEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemLists_Users_UserTableId",
                table: "ItemLists");

            migrationBuilder.DropForeignKey(
                name: "FK_UserItems_ItemLists_ItemListTableId",
                table: "UserItems");

            migrationBuilder.DropForeignKey(
                name: "FK_UserItems_Items_ItemTableId",
                table: "UserItems");

            migrationBuilder.RenameColumn(
                name: "UserTableId",
                table: "Users",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "ItemTableId",
                table: "UserItems",
                newName: "ItemId");

            migrationBuilder.RenameColumn(
                name: "ItemListTableId",
                table: "UserItems",
                newName: "ItemListId");

            migrationBuilder.RenameIndex(
                name: "IX_UserItems_ItemTableId",
                table: "UserItems",
                newName: "IX_UserItems_ItemId");

            migrationBuilder.RenameColumn(
                name: "ItemTableId",
                table: "Items",
                newName: "ItemId");

            migrationBuilder.RenameColumn(
                name: "UserTableId",
                table: "ItemLists",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "ItemListTableId",
                table: "ItemLists",
                newName: "ItemListId");

            migrationBuilder.RenameIndex(
                name: "IX_ItemLists_UserTableId",
                table: "ItemLists",
                newName: "IX_ItemLists_UserId");

            migrationBuilder.AlterColumn<int>(
                name: "UserSex",
                table: "Users",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemLists_Users_UserId",
                table: "ItemLists",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserItems_ItemLists_ItemListId",
                table: "UserItems",
                column: "ItemListId",
                principalTable: "ItemLists",
                principalColumn: "ItemListId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserItems_Items_ItemId",
                table: "UserItems",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "ItemId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemLists_Users_UserId",
                table: "ItemLists");

            migrationBuilder.DropForeignKey(
                name: "FK_UserItems_ItemLists_ItemListId",
                table: "UserItems");

            migrationBuilder.DropForeignKey(
                name: "FK_UserItems_Items_ItemId",
                table: "UserItems");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Users",
                newName: "UserTableId");

            migrationBuilder.RenameColumn(
                name: "ItemId",
                table: "UserItems",
                newName: "ItemTableId");

            migrationBuilder.RenameColumn(
                name: "ItemListId",
                table: "UserItems",
                newName: "ItemListTableId");

            migrationBuilder.RenameIndex(
                name: "IX_UserItems_ItemId",
                table: "UserItems",
                newName: "IX_UserItems_ItemTableId");

            migrationBuilder.RenameColumn(
                name: "ItemId",
                table: "Items",
                newName: "ItemTableId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "ItemLists",
                newName: "UserTableId");

            migrationBuilder.RenameColumn(
                name: "ItemListId",
                table: "ItemLists",
                newName: "ItemListTableId");

            migrationBuilder.RenameIndex(
                name: "IX_ItemLists_UserId",
                table: "ItemLists",
                newName: "IX_ItemLists_UserTableId");

            migrationBuilder.AlterColumn<string>(
                name: "UserSex",
                table: "Users",
                type: "text",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemLists_Users_UserTableId",
                table: "ItemLists",
                column: "UserTableId",
                principalTable: "Users",
                principalColumn: "UserTableId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserItems_ItemLists_ItemListTableId",
                table: "UserItems",
                column: "ItemListTableId",
                principalTable: "ItemLists",
                principalColumn: "ItemListTableId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserItems_Items_ItemTableId",
                table: "UserItems",
                column: "ItemTableId",
                principalTable: "Items",
                principalColumn: "ItemTableId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
