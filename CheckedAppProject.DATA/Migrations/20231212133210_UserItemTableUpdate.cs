using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CheckedAppProject.DATA.Migrations
{
    /// <inheritdoc />
    public partial class UserItemTableUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserItems",
                table: "UserItems");

            migrationBuilder.DropColumn(
                name: "UserItemTableId",
                table: "UserItems");

            migrationBuilder.RenameColumn(
                name: "ItemsTableId",
                table: "UserItems",
                newName: "ItemTableId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserItems",
                table: "UserItems",
                columns: new[] { "ItemListTableId", "ItemTableId" });

            migrationBuilder.CreateIndex(
                name: "IX_UserItems_ItemTableId",
                table: "UserItems",
                column: "ItemTableId");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserItems_ItemLists_ItemListTableId",
                table: "UserItems");

            migrationBuilder.DropForeignKey(
                name: "FK_UserItems_Items_ItemTableId",
                table: "UserItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserItems",
                table: "UserItems");

            migrationBuilder.DropIndex(
                name: "IX_UserItems_ItemTableId",
                table: "UserItems");

            migrationBuilder.RenameColumn(
                name: "ItemTableId",
                table: "UserItems",
                newName: "ItemsTableId");

            migrationBuilder.AddColumn<int>(
                name: "UserItemTableId",
                table: "UserItems",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserItems",
                table: "UserItems",
                column: "UserItemTableId");
        }
    }
}
