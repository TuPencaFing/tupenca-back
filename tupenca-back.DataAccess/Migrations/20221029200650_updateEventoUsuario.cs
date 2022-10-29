using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace tupenca_back.DataAccess.Migrations
{
    public partial class updateEventoUsuario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Predicciones_Personas_UsuarioId",
                table: "Predicciones");

            migrationBuilder.DropIndex(
                name: "IX_Predicciones_UsuarioId",
                table: "Predicciones");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Predicciones_UsuarioId",
                table: "Predicciones",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Predicciones_Personas_UsuarioId",
                table: "Predicciones",
                column: "UsuarioId",
                principalTable: "Personas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
