using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace tupenca_back.DataAccess.Migrations
{
    public partial class notificationuser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NotificationUserDeviceIds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    deviceId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationUserDeviceIds", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Campeonatos",
                columns: new[] { "Id", "DeporteId", "FinishDate", "Image", "Name", "StartDate" },
                values: new object[] { 10, 1, new DateTime(2022, 11, 28, 10, 0, 0, 0, DateTimeKind.Unspecified), null, "Torneo Clausura", new DateTime(2022, 11, 20, 13, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Deportes",
                keyColumn: "Id",
                keyValue: 2,
                column: "Nombre",
                value: "Futbol");

            migrationBuilder.InsertData(
                table: "Premios",
                columns: new[] { "Id", "Image", "PencaId", "Percentage", "Position" },
                values: new object[] { 10, null, null, 100m, 1 });

            migrationBuilder.InsertData(
                table: "Puntajes",
                columns: new[] { "Id", "Resultado", "ResultadoExacto" },
                values: new object[] { 10, 1, 3 });

            migrationBuilder.InsertData(
                table: "Pencas",
                columns: new[] { "Id", "CampeonatoId", "Commission", "CostEntry", "Description", "Discriminator", "Image", "Pozo", "PuntajeId", "Title" },
                values: new object[] { 12, 10, 0m, 0m, "Gana muchos premios", "PencaCompartida", null, 0m, 10, "Penca Movistar" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NotificationUserDeviceIds");

            migrationBuilder.DeleteData(
                table: "Pencas",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Premios",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Campeonatos",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Puntajes",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.UpdateData(
                table: "Deportes",
                keyColumn: "Id",
                keyValue: 2,
                column: "Nombre",
                value: "Tennis");
        }
    }
}
