using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class UpdateOrderNumbertoOrderInSliderTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderNumber",
                table: "Sliders");

            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "Sliders",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Order",
                table: "Sliders");

            migrationBuilder.AddColumn<int>(
                name: "OrderNumber",
                table: "Sliders",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
