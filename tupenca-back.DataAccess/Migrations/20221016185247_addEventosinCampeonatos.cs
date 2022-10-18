using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace tupenca_back.DataAccess.Migrations
{
    public partial class addEventosinCampeonatos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "CampeonatoId",
                table: "Eventos");

            migrationBuilder.AlterColumn<int>(
                name: "DeporteId",
                table: "Campeonatos",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "CampeonatoEvento",
                columns: table => new
                {
                    CampeonatosId = table.Column<int>(type: "int", nullable: false),
                    EventosId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CampeonatoEvento", x => new { x.CampeonatosId, x.EventosId });
                    table.ForeignKey(
                        name: "FK_CampeonatoEvento_Campeonatos_CampeonatosId",
                        column: x => x.CampeonatosId,
                        principalTable: "Campeonatos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CampeonatoEvento_Eventos_EventosId",
                        column: x => x.EventosId,
                        principalTable: "Eventos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CampeonatoEvento_EventosId",
                table: "CampeonatoEvento",
                column: "EventosId");

            migrationBuilder.AddForeignKey(
                name: "FK_Campeonatos_Deportes_DeporteId",
                table: "Campeonatos",
                column: "DeporteId",
                principalTable: "Deportes",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Campeonatos_Deportes_DeporteId",
                table: "Campeonatos");

            migrationBuilder.DropTable(
                name: "CampeonatoEvento");

            migrationBuilder.AddColumn<int>(
                name: "CampeonatoId",
                table: "Eventos",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DeporteId",
                table: "Campeonatos",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Eventos_CampeonatoId",
                table: "Eventos",
                column: "CampeonatoId");

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
    }
}
