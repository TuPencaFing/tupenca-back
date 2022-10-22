using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace tupenca_back.DataAccess.Migrations
{
    public partial class dataInicialEvento : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Deportes",
                columns: new[] { "Id", "ImagenName", "Nombre" },
                values: new object[] { 1, null, "Futbol" });

            migrationBuilder.InsertData(
                table: "Equipos",
                columns: new[] { "Id", "Nombre" },
                values: new object[,]
                {
                    { 1, "Qatar" },
                    { 2, "Ecuador" },
                    { 3, "Senegal" },
                    { 4, "Holanda" },
                    { 5, "Portugal" },
                    { 6, "Ghana" },
                    { 7, "Uruguay" },
                    { 8, "Corea del Sur" }
                });

            migrationBuilder.InsertData(
                table: "Eventos",
                columns: new[] { "Id", "EquipoLocalId", "EquipoVisitanteId", "FechaInicial" },
                values: new object[,]
                {
                    { 1, 1, 2, new DateTime(2022, 11, 20, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 3, 4, new DateTime(2022, 11, 21, 7, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, 7, 8, new DateTime(2022, 11, 24, 10, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, 5, 6, new DateTime(2022, 11, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, 1, 3, new DateTime(2022, 11, 25, 10, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, 4, 2, new DateTime(2022, 11, 25, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 7, 8, 6, new DateTime(2022, 11, 28, 10, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 8, 5, 7, new DateTime(2022, 11, 28, 16, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 9, 2, 3, new DateTime(2022, 11, 29, 12, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 10, 4, 1, new DateTime(2022, 11, 29, 12, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 11, 6, 7, new DateTime(2022, 12, 2, 12, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 12, 8, 5, new DateTime(2022, 12, 2, 12, 0, 0, 0, DateTimeKind.Unspecified) }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Deportes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Eventos",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Eventos",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Eventos",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Eventos",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Eventos",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Eventos",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Eventos",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Eventos",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Eventos",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Eventos",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Eventos",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Eventos",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Equipos",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Equipos",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Equipos",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Equipos",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Equipos",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Equipos",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Equipos",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Equipos",
                keyColumn: "Id",
                keyValue: 8);
        }
    }
}
