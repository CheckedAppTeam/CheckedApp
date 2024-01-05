using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CheckedAppProject.DATA.Migrations
{
    /// <inheritdoc />
    public partial class Corrected : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserItems",
                table: "UserItems");

            migrationBuilder.DropIndex(
                name: "IX_UserItems_ItemId",
                table: "UserItems");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "ItemLists",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true,
                oldDefaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserItems",
                table: "UserItems",
                columns: new[] { "ItemId", "ItemListId" });

            migrationBuilder.CreateIndex(
                name: "IX_UserItems_ItemListId",
                table: "UserItems",
                column: "ItemListId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserItems",
                table: "UserItems");

            migrationBuilder.DropIndex(
                name: "IX_UserItems_ItemListId",
                table: "UserItems");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "ItemLists",
                type: "timestamp with time zone",
                nullable: true,
                defaultValueSql: "CURRENT_TIMESTAMP",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserItems",
                table: "UserItems",
                columns: new[] { "ItemListId", "ItemId" });

            migrationBuilder.CreateIndex(
                name: "IX_UserItems_ItemId",
                table: "UserItems",
                column: "ItemId");
        }
    }
}
