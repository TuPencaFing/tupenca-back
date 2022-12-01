using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace tupenca_back.DataAccess.Migrations
{
    public partial class AddUserToPuntajeList : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_PuntajeUsuarioPencas_UsuarioId",
                table: "PuntajeUsuarioPencas",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_PuntajeUsuarioPencas_Personas_UsuarioId",
                table: "PuntajeUsuarioPencas",
                column: "UsuarioId",
                principalTable: "Personas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PuntajeUsuarioPencas_Personas_UsuarioId",
                table: "PuntajeUsuarioPencas");

            migrationBuilder.DropIndex(
                name: "IX_PuntajeUsuarioPencas_UsuarioId",
                table: "PuntajeUsuarioPencas");
        }
    }
}
