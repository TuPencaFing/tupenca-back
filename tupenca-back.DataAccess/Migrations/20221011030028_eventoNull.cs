using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace tupenca_back.DataAccess.Migrations
{
    public partial class eventoNull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Eventos_Resultado_ResultadoId",
                table: "Eventos");

            migrationBuilder.AlterColumn<int>(
                name: "ResultadoId",
                table: "Eventos",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Eventos_Resultado_ResultadoId",
                table: "Eventos",
                column: "ResultadoId",
                principalTable: "Resultado",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Eventos_Resultado_ResultadoId",
                table: "Eventos");

            migrationBuilder.AlterColumn<int>(
                name: "ResultadoId",
                table: "Eventos",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Eventos_Resultado_ResultadoId",
                table: "Eventos",
                column: "ResultadoId",
                principalTable: "Resultado",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
