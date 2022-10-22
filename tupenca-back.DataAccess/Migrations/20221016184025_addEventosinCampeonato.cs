using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace tupenca_back.DataAccess.Migrations
{
    public partial class addEventosinCampeonato : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CampeonatoId",
                table: "Eventos",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeporteId",
                table: "Campeonatos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Eventos_CampeonatoId",
                table: "Eventos",
                column: "CampeonatoId");

            migrationBuilder.CreateIndex(
                name: "IX_Campeonatos_DeporteId",
                table: "Campeonatos",
                column: "DeporteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Campeonatos_Deportes_DeporteId",
                table: "Campeonatos",
                column: "DeporteId",
                principalTable: "Deportes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Eventos_Campeonatos_CampeonatoId",
                table: "Eventos",
                column: "CampeonatoId",
                principalTable: "Campeonatos",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Campeonatos_Deportes_DeporteId",
                table: "Campeonatos");

            migrationBuilder.DropForeignKey(
                name: "FK_Eventos_Campeonatos_CampeonatoId",
                table: "Eventos");

            migrationBuilder.DropIndex(
                name: "IX_Eventos_CampeonatoId",
                table: "Eventos");

            migrationBuilder.DropIndex(
                name: "IX_Campeonatos_DeporteId",
                table: "Campeonatos");

            migrationBuilder.DropColumn(
                name: "CampeonatoId",
                table: "Eventos");

            migrationBuilder.DropColumn(
                name: "DeporteId",
                table: "Campeonatos");
        }
    }
}
