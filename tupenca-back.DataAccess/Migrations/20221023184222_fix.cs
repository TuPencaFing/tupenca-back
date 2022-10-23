using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace tupenca_back.DataAccess.Migrations
{
    public partial class fix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UsuarioId",
                table: "Predicciones",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Predicciones_Personas_UsuarioId",
                table: "Predicciones");

            migrationBuilder.DropIndex(
                name: "IX_Predicciones_UsuarioId",
                table: "Predicciones");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "Predicciones");
        }
    }
}
