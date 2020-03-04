using Microsoft.EntityFrameworkCore.Migrations;

namespace AdvertismentPlatform.Migrations
{
    public partial class Update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_advertisments_AspNetUsers_ApplicationUserId",
                table: "advertisments");

            migrationBuilder.AddForeignKey(
                name: "FK_advertisments_AspNetUsers_ApplicationUserId",
                table: "advertisments",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_advertisments_AspNetUsers_ApplicationUserId",
                table: "advertisments");

            migrationBuilder.AddForeignKey(
                name: "FK_advertisments_AspNetUsers_ApplicationUserId",
                table: "advertisments",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
