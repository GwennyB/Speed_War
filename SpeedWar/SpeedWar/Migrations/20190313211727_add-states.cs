using Microsoft.EntityFrameworkCore.Migrations;

namespace SpeedWar.Migrations
{
    public partial class addstates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CurrentUserID",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "EmptyDecks",
                table: "Users",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "FirstCardID",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PlayerTurn",
                table: "Users",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "SecondCardID",
                table: "Users",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_CurrentUserID",
                table: "Users",
                column: "CurrentUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Users_FirstCardID",
                table: "Users",
                column: "FirstCardID");

            migrationBuilder.CreateIndex(
                name: "IX_Users_SecondCardID",
                table: "Users",
                column: "SecondCardID");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Users_CurrentUserID",
                table: "Users",
                column: "CurrentUserID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Cards_FirstCardID",
                table: "Users",
                column: "FirstCardID",
                principalTable: "Cards",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Cards_SecondCardID",
                table: "Users",
                column: "SecondCardID",
                principalTable: "Cards",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Users_CurrentUserID",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Cards_FirstCardID",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Cards_SecondCardID",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_CurrentUserID",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_FirstCardID",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_SecondCardID",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CurrentUserID",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "EmptyDecks",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "FirstCardID",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PlayerTurn",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "SecondCardID",
                table: "Users");
        }
    }
}
