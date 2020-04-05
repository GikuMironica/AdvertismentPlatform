using Microsoft.EntityFrameworkCore.Migrations;

namespace AdvertismentPlatform.Migrations
{
    public partial class ItemCategoryPriceUpdatge : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Car_Type",
                table: "items",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(2) CHARACTER SET utf8mb4",
                oldMaxLength: 2,
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CurrencyId",
                table: "items",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrencyId",
                table: "items");

            migrationBuilder.AlterColumn<string>(
                name: "Car_Type",
                table: "items",
                type: "varchar(2) CHARACTER SET utf8mb4",
                maxLength: 2,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
