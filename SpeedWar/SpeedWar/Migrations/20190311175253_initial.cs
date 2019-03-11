using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SpeedWar.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cards",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Suit = table.Column<int>(nullable: false),
                    Rank = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cards", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Decks",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserID = table.Column<int>(nullable: false),
                    DeckType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Decks", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Decks_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DeckCards",
                columns: table => new
                {
                    CardID = table.Column<int>(nullable: false),
                    DeckID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeckCards", x => new { x.CardID, x.DeckID });
                    table.ForeignKey(
                        name: "FK_DeckCards_Cards_CardID",
                        column: x => x.CardID,
                        principalTable: "Cards",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DeckCards_Decks_DeckID",
                        column: x => x.DeckID,
                        principalTable: "Decks",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Cards",
                columns: new[] { "ID", "Rank", "Suit" },
                values: new object[,]
                {
                    { 1, 1, 0 },
                    { 30, 4, 3 },
                    { 31, 5, 3 },
                    { 32, 6, 3 },
                    { 33, 7, 3 },
                    { 34, 8, 3 },
                    { 35, 9, 3 },
                    { 36, 10, 3 },
                    { 37, 11, 3 },
                    { 38, 12, 3 },
                    { 39, 13, 3 },
                    { 40, 1, 2 },
                    { 41, 2, 2 },
                    { 42, 3, 2 },
                    { 43, 4, 2 },
                    { 44, 5, 2 },
                    { 45, 6, 2 },
                    { 46, 7, 2 },
                    { 47, 8, 2 },
                    { 48, 9, 2 },
                    { 49, 10, 2 },
                    { 50, 11, 2 },
                    { 51, 12, 2 },
                    { 52, 13, 2 },
                    { 28, 2, 3 },
                    { 27, 1, 3 },
                    { 29, 3, 3 },
                    { 25, 12, 1 },
                    { 2, 2, 0 },
                    { 3, 3, 0 },
                    { 4, 4, 0 },
                    { 5, 5, 0 },
                    { 6, 6, 0 },
                    { 7, 7, 0 },
                    { 8, 8, 0 },
                    { 9, 9, 0 },
                    { 10, 10, 0 },
                    { 26, 13, 1 },
                    { 12, 12, 0 },
                    { 13, 13, 0 },
                    { 11, 11, 0 },
                    { 15, 2, 1 },
                    { 24, 11, 1 },
                    { 14, 1, 1 },
                    { 22, 9, 1 },
                    { 21, 8, 1 },
                    { 23, 10, 1 },
                    { 19, 6, 1 },
                    { 18, 5, 1 },
                    { 17, 4, 1 },
                    { 16, 3, 1 },
                    { 20, 7, 1 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "ID", "Name" },
                values: new object[,]
                {
                    { 5, "Xia" },
                    { 1, "Discard" },
                    { 2, "Computer" },
                    { 3, "Clarice" },
                    { 4, "Shalom" },
                    { 6, "Gwen" }
                });

            migrationBuilder.InsertData(
                table: "Decks",
                columns: new[] { "ID", "DeckType", "UserID" },
                values: new object[,]
                {
                    { 1, 0, 1 },
                    { 2, 1, 2 },
                    { 3, 2, 2 },
                    { 4, 1, 3 },
                    { 5, 2, 3 },
                    { 6, 1, 4 },
                    { 7, 2, 4 },
                    { 8, 1, 5 },
                    { 9, 2, 5 },
                    { 10, 1, 6 },
                    { 11, 2, 6 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_DeckCards_DeckID",
                table: "DeckCards",
                column: "DeckID");

            migrationBuilder.CreateIndex(
                name: "IX_Decks_UserID",
                table: "Decks",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeckCards");

            migrationBuilder.DropTable(
                name: "Cards");

            migrationBuilder.DropTable(
                name: "Decks");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
