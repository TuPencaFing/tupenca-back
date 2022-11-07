using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace tupenca_back.DataAccess.Migrations
{
    public partial class SeedDeporteTennis : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Deportes",
                columns: new[] { "Id", "Image", "Nombre" },
                values: new object[] { 2, null, "Tennis" });

            migrationBuilder.InsertData(
                table: "Eventos",
                columns: new[] { "Id", "EquipoLocalId", "EquipoVisitanteId", "FechaInicial", "Image", "IsEmpateValid", "IsPuntajeEquipoValid" },
                values: new object[] { 13, 2, 7, new DateTime(2023, 1, 2, 7, 0, 0, 0, DateTimeKind.Unspecified), null, false, false });

            migrationBuilder.InsertData(
                table: "Eventos",
                columns: new[] { "Id", "EquipoLocalId", "EquipoVisitanteId", "FechaInicial", "Image", "IsEmpateValid", "IsPuntajeEquipoValid" },
                values: new object[] { 14, 7, 2, new DateTime(2023, 1, 2, 8, 0, 0, 0, DateTimeKind.Unspecified), null, false, false });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Deportes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Eventos",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Eventos",
                keyColumn: "Id",
                keyValue: 14);
        }
    }
}
