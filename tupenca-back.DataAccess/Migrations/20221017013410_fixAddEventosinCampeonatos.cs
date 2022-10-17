using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace tupenca_back.DataAccess.Migrations
{
    public partial class fixAddEventosinCampeonatos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Campeonatos_Deportes_DeporteId",
                table: "Campeonatos");

            migrationBuilder.AlterColumn<int>(
                name: "DeporteId",
                table: "Campeonatos",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Campeonatos_Deportes_DeporteId",
                table: "Campeonatos",
                column: "DeporteId",
                principalTable: "Deportes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Campeonatos_Deportes_DeporteId",
                table: "Campeonatos");

            migrationBuilder.AlterColumn<int>(
                name: "DeporteId",
                table: "Campeonatos",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Campeonatos_Deportes_DeporteId",
                table: "Campeonatos",
                column: "DeporteId",
                principalTable: "Deportes",
                principalColumn: "Id");
        }
    }
}
