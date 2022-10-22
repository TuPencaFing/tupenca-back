using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace tupenca_back.DataAccess.Migrations
{
    public partial class persona : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Deportes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImagenName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deportes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Empresa",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Razonsocial = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RUT = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empresa", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Equipos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Personas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HashedPassword = table.Column<byte[]>(type: "varbinary(64)", maxLength: 64, nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(128)", maxLength: 128, nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Campeonatos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FinishDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeporteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Campeonatos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Campeonatos_Deportes_DeporteId",
                        column: x => x.DeporteId,
                        principalTable: "Deportes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Eventos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FechaInicial = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EquipoLocalId = table.Column<int>(type: "int", nullable: false),
                    EquipoVisitanteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Eventos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Eventos_Equipos_EquipoLocalId",
                        column: x => x.EquipoLocalId,
                        principalTable: "Equipos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Eventos_Equipos_EquipoVisitanteId",
                        column: x => x.EquipoVisitanteId,
                        principalTable: "Equipos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_Campeonatos_DeporteId",
                table: "Campeonatos",
                column: "DeporteId");

            migrationBuilder.CreateIndex(
                name: "IX_Eventos_EquipoLocalId",
                table: "Eventos",
                column: "EquipoLocalId");

            migrationBuilder.CreateIndex(
                name: "IX_Eventos_EquipoVisitanteId",
                table: "Eventos",
                column: "EquipoVisitanteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CampeonatoEvento");

            migrationBuilder.DropTable(
                name: "Empresa");

            migrationBuilder.DropTable(
                name: "Personas");

            migrationBuilder.DropTable(
                name: "Campeonatos");

            migrationBuilder.DropTable(
                name: "Eventos");

            migrationBuilder.DropTable(
                name: "Deportes");

            migrationBuilder.DropTable(
                name: "Equipos");
        }
    }
}
