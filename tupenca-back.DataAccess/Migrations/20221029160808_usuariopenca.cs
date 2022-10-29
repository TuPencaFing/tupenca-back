using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace tupenca_back.DataAccess.Migrations
{
    public partial class usuariopenca : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UsuarioPenca",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    PencaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioPenca", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsuarioPenca_Pencas_PencaId",
                        column: x => x.PencaId,
                        principalTable: "Pencas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsuarioPenca_Personas_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Personas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioPenca_PencaId",
                table: "UsuarioPenca",
                column: "PencaId");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioPenca_UsuarioId",
                table: "UsuarioPenca",
                column: "UsuarioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsuarioPenca");
        }
    }
}
