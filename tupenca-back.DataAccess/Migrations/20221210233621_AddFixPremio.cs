using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace tupenca_back.DataAccess.Migrations
{
    public partial class AddFixPremio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Premios_Pencas_PencaId",
                table: "Premios");

            migrationBuilder.DropIndex(
                name: "IX_Premios_PencaId",
                table: "Premios");

            migrationBuilder.DropColumn(
                name: "PencaId",
                table: "Premios");

            migrationBuilder.CreateTable(
                name: "PencaPremio",
                columns: table => new
                {
                    PencasId = table.Column<int>(type: "int", nullable: false),
                    PremiosId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PencaPremio", x => new { x.PencasId, x.PremiosId });
                    table.ForeignKey(
                        name: "FK_PencaPremio_Pencas_PencasId",
                        column: x => x.PencasId,
                        principalTable: "Pencas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PencaPremio_Premios_PremiosId",
                        column: x => x.PremiosId,
                        principalTable: "Premios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PencaPremio_PremiosId",
                table: "PencaPremio",
                column: "PremiosId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PencaPremio");

            migrationBuilder.AddColumn<int>(
                name: "PencaId",
                table: "Premios",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Premios_PencaId",
                table: "Premios",
                column: "PencaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Premios_Pencas_PencaId",
                table: "Premios",
                column: "PencaId",
                principalTable: "Pencas",
                principalColumn: "Id");
        }
    }
}
