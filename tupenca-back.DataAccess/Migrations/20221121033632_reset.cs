using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace tupenca_back.DataAccess.Migrations
{
    public partial class reset : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PersonaResetPassword",
                columns: table => new
                {
                    Token = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PersonaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonaResetPassword", x => x.Token);
                    table.ForeignKey(
                        name: "FK_PersonaResetPassword_Personas_PersonaId",
                        column: x => x.PersonaId,
                        principalTable: "Personas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Predicciones_EventoId",
                table: "Predicciones",
                column: "EventoId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonaResetPassword_PersonaId",
                table: "PersonaResetPassword",
                column: "PersonaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Predicciones_Eventos_EventoId",
                table: "Predicciones",
                column: "EventoId",
                principalTable: "Eventos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Predicciones_Eventos_EventoId",
                table: "Predicciones");

            migrationBuilder.DropTable(
                name: "PersonaResetPassword");

            migrationBuilder.DropIndex(
                name: "IX_Predicciones_EventoId",
                table: "Predicciones");
        }
    }
}
