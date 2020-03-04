using Microsoft.EntityFrameworkCore.Migrations;

namespace AdvertismentPlatform.Migrations
{
    public partial class MapAdvertiseAppUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "AspNetUsers",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                nullable: false);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "advertisments",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_advertisments_ApplicationUserId",
                table: "advertisments",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_advertisments_AspNetUsers_ApplicationUserId",
                table: "advertisments",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_advertisments_AspNetUsers_ApplicationUserId",
                table: "advertisments");

            migrationBuilder.DropIndex(
                name: "IX_advertisments_ApplicationUserId",
                table: "advertisments");

            migrationBuilder.DropColumn(
                name: "City",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "advertisments");
        }
    }
}
