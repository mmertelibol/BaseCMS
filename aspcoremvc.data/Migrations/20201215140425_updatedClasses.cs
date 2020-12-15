using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class updatedClasses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ViewCount",
                table: "Contents");

            migrationBuilder.AddColumn<string>(
                name: "PageUrl",
                table: "References",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PageContent",
                table: "News",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PageUrl",
                table: "References");

            migrationBuilder.DropColumn(
                name: "PageContent",
                table: "News");

            migrationBuilder.AddColumn<int>(
                name: "ViewCount",
                table: "Contents",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
