using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class updatePageComponentCategories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ComponentCategories_Users_AddedById",
                table: "ComponentCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_ComponentCategories_Users_UpdatedById",
                table: "ComponentCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_PageComponents_ComponentCategories_PageComponentCategoryId",
                table: "PageComponents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ComponentCategories",
                table: "ComponentCategories");

            migrationBuilder.RenameTable(
                name: "ComponentCategories",
                newName: "PageComponentCategories");

            migrationBuilder.RenameIndex(
                name: "IX_ComponentCategories_UpdatedById",
                table: "PageComponentCategories",
                newName: "IX_PageComponentCategories_UpdatedById");

            migrationBuilder.RenameIndex(
                name: "IX_ComponentCategories_AddedById",
                table: "PageComponentCategories",
                newName: "IX_PageComponentCategories_AddedById");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PageComponentCategories",
                table: "PageComponentCategories",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PageComponentCategories_Users_AddedById",
                table: "PageComponentCategories",
                column: "AddedById",
                principalSchema: "User",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PageComponentCategories_Users_UpdatedById",
                table: "PageComponentCategories",
                column: "UpdatedById",
                principalSchema: "User",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PageComponents_PageComponentCategories_PageComponentCategoryId",
                table: "PageComponents",
                column: "PageComponentCategoryId",
                principalTable: "PageComponentCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PageComponentCategories_Users_AddedById",
                table: "PageComponentCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_PageComponentCategories_Users_UpdatedById",
                table: "PageComponentCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_PageComponents_PageComponentCategories_PageComponentCategoryId",
                table: "PageComponents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PageComponentCategories",
                table: "PageComponentCategories");

            migrationBuilder.RenameTable(
                name: "PageComponentCategories",
                newName: "ComponentCategories");

            migrationBuilder.RenameIndex(
                name: "IX_PageComponentCategories_UpdatedById",
                table: "ComponentCategories",
                newName: "IX_ComponentCategories_UpdatedById");

            migrationBuilder.RenameIndex(
                name: "IX_PageComponentCategories_AddedById",
                table: "ComponentCategories",
                newName: "IX_ComponentCategories_AddedById");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ComponentCategories",
                table: "ComponentCategories",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ComponentCategories_Users_AddedById",
                table: "ComponentCategories",
                column: "AddedById",
                principalSchema: "User",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ComponentCategories_Users_UpdatedById",
                table: "ComponentCategories",
                column: "UpdatedById",
                principalSchema: "User",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PageComponents_ComponentCategories_PageComponentCategoryId",
                table: "PageComponents",
                column: "PageComponentCategoryId",
                principalTable: "ComponentCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
