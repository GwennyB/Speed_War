using Microsoft.EntityFrameworkCore.Migrations;

namespace SpeedWar.Migrations
{
    public partial class @int : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Cards",
                keyColumn: "ID",
                keyValue: 53,
                column: "ImageURL",
                value: "/Assets/PNG/null.jpg");

            migrationBuilder.UpdateData(
                table: "Cards",
                keyColumn: "ID",
                keyValue: 54,
                column: "ImageURL",
                value: "/Assets/PNG/null.jpg");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Cards",
                keyColumn: "ID",
                keyValue: 53,
                column: "ImageURL",
                value: null);

            migrationBuilder.UpdateData(
                table: "Cards",
                keyColumn: "ID",
                keyValue: 54,
                column: "ImageURL",
                value: null);
        }
    }
}
