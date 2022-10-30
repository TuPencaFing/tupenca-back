using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace tupenca_back.DataAccess.Migrations
{
    public partial class usertoken : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserInviteTokens",
                columns: table => new
                {
                    Token = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PencaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserInviteTokens", x => x.Token);
                });
        }
        /*
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserInviteTokens");
        }
        */
    }
}
