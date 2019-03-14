using Microsoft.EntityFrameworkCore.Migrations;

namespace SpeedWar.Migrations
{
    public partial class makeaddNAcards : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Cards",
                columns: new[] { "ID", "Rank", "Suit" },
                values: new object[] { 53, 0, 0 });

            migrationBuilder.InsertData(
                table: "Cards",
                columns: new[] { "ID", "Rank", "Suit" },
                values: new object[] { 54, 0, 0 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "ID",
                keyValue: 53);

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "ID",
                keyValue: 54);
        }
    }
}
