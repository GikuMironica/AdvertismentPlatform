using Microsoft.EntityFrameworkCore.Migrations;

namespace AdvertismentPlatform.Migrations
{
    public partial class UpdateModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Car_Type",
                table: "items",
                maxLength: 2,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Doors",
                table: "items",
                maxLength: 2,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Seats",
                table: "items",
                maxLength: 2,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TopSpeed",
                table: "items",
                maxLength: 3,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Car_Type",
                table: "items");

            migrationBuilder.DropColumn(
                name: "Doors",
                table: "items");

            migrationBuilder.DropColumn(
                name: "Seats",
                table: "items");

            migrationBuilder.DropColumn(
                name: "TopSpeed",
                table: "items");
        }
    }
}
