using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class updatepagecategoryId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ComponentCategoryId",
                table: "PageComponents");

            migrationBuilder.AlterColumn<int>(
                name: "PageComponentCategoryId",
                table: "PageComponents",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "PageComponentCategoryId",
                table: "PageComponents",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "ComponentCategoryId",
                table: "PageComponents",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
