using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SpeedWar.Migrations
{
    public partial class initial2 : Migration
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
                    Rank = table.Column<int>(nullable: false),
                    ImageURL = table.Column<string>(nullable: true)
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
                    Name = table.Column<string>(nullable: true),
                    PlayerTurn = table.Column<bool>(nullable: false),
                    FirstCard = table.Column<int>(nullable: false),
                    SecondCard = table.Column<int>(nullable: false),
                    EmptyDecks = table.Column<bool>(nullable: false)
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
                columns: new[] { "ID", "ImageURL", "Rank", "Suit" },
                values: new object[,]
                {
                    { 1, "/Assets/AH.png", 1, 0 },
                    { 31, "/Assets/5C.png", 5, 3 },
                    { 32, "/Assets/6C.png", 6, 3 },
                    { 33, "/Assets/7C.png", 7, 3 },
                    { 34, "/Assets/8C.png", 8, 3 },
                    { 35, "/Assets/9C.png", 9, 3 },
                    { 36, "/Assets/10C.png", 10, 3 },
                    { 37, "/Assets/JC.png", 11, 3 },
                    { 38, "/Assets/QC.png", 12, 3 },
                    { 39, "/Assets/KC.png", 13, 3 },
                    { 40, "/Assets/AD.png", 1, 2 },
                    { 41, "/Assets/2D.png", 2, 2 },
                    { 29, "/Assets/3C.png", 3, 3 },
                    { 42, "/Assets/3D.png", 3, 2 },
                    { 44, "/Assets/5D.png", 5, 2 },
                    { 45, "/Assets/6D.png", 6, 2 },
                    { 46, "/Assets/7D.png", 7, 2 },
                    { 47, "/Assets/8D.png", 8, 2 },
                    { 48, "/Assets/9D.png", 9, 2 },
                    { 49, "/Assets/10D.png", 10, 2 },
                    { 50, "/Assets/JD.png", 11, 2 },
                    { 51, "/Assets/QD.png", 12, 2 },
                    { 52, "/Assets/KD.png", 13, 2 },
                    { 53, null, 13, 3 },
                    { 54, null, 13, 3 },
                    { 43, "/Assets/4D.png", 4, 2 },
                    { 28, "/Assets/2C.png", 2, 3 },
                    { 30, "/Assets/4C.png", 4, 3 },
                    { 26, "/Assets/KS.png", 13, 1 },
                    { 2, "/Assets/2H.png", 2, 0 },
                    { 3, "/Assets/3H.png", 3, 0 },
                    { 4, "/Assets/4H.png", 4, 0 },
                    { 5, "/Assets/5H.png", 5, 0 },
                    { 6, "/Assets/6H.png", 6, 0 },
                    { 7, "/Assets/7H.png", 7, 0 },
                    { 8, "/Assets/8H.png", 8, 0 },
                    { 9, "/Assets/9H.png", 9, 0 },
                    { 10, "/Assets/10H.png", 10, 0 },
                    { 11, "/Assets/JH.png", 11, 0 },
                    { 27, "/Assets/AC.png", 1, 3 },
                    { 13, "/Assets/KH.png", 13, 0 },
                    { 12, "/Assets/QH.png", 12, 0 },
                    { 15, "/Assets/2S.png", 2, 1 },
                    { 25, "/Assets/QS.png", 12, 1 },
                    { 24, "/Assets/JS.png", 11, 1 },
                    { 14, "/Assets/AS.png", 1, 1 },
                    { 22, "/Assets/9S.png", 9, 1 },
                    { 21, "/Assets/8S.png", 8, 1 },
                    { 23, "/Assets/10S.png", 10, 1 },
                    { 19, "/Assets/6S.png", 6, 1 },
                    { 18, "/Assets/5S.png", 5, 1 },
                    { 17, "/Assets/4S.png", 4, 1 },
                    { 16, "/Assets/3S.png", 3, 1 },
                    { 20, "/Assets/7S.png", 7, 1 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "ID", "EmptyDecks", "FirstCard", "Name", "PlayerTurn", "SecondCard" },
                values: new object[,]
                {
                    { 5, false, 0, "Xia", false, 0 },
                    { 1, false, 0, "Discard", false, 0 },
                    { 2, false, 0, "Computer", false, 0 },
                    { 3, false, 0, "Clarice", false, 0 },
                    { 4, false, 0, "Shalom", false, 0 },
                    { 6, false, 0, "Gwen", false, 0 }
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
