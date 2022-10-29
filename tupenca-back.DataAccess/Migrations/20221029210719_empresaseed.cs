using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace tupenca_back.DataAccess.Migrations
{
    public partial class empresaseed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Empresas",
                columns: new[] { "Id", "FechaCreacion", "RUT", "Razonsocial" },
                values: new object[,]
                {
                    { 1, new DateTime(2022, 11, 20, 13, 0, 0, 0, DateTimeKind.Unspecified), "214873040018", "McDonald's S.A." },
                    { 2, new DateTime(2022, 11, 21, 7, 0, 0, 0, DateTimeKind.Unspecified), "304001821487", "BMW Ibérica S.A." },
                    { 3, new DateTime(2022, 11, 21, 10, 0, 0, 0, DateTimeKind.Unspecified), "821473040018", "Air Europa Líneas Aéreas S.A." },
                    { 4, new DateTime(2022, 11, 21, 16, 0, 0, 0, DateTimeKind.Unspecified), "040001821487", "Punto FA S.L." }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Empresas",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Empresas",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Empresas",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Empresas",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
