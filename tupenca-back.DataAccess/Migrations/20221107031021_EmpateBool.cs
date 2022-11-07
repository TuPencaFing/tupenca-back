using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace tupenca_back.DataAccess.Migrations
{
    public partial class EmpateBool : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsEmpateValid",
                table: "Eventos",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsPuntajeEquipoValid",
                table: "Eventos",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Eventos",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "IsEmpateValid", "IsPuntajeEquipoValid" },
                values: new object[] { true, true });

            migrationBuilder.UpdateData(
                table: "Eventos",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "IsEmpateValid", "IsPuntajeEquipoValid" },
                values: new object[] { true, true });

            migrationBuilder.UpdateData(
                table: "Eventos",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "IsEmpateValid", "IsPuntajeEquipoValid" },
                values: new object[] { true, true });

            migrationBuilder.UpdateData(
                table: "Eventos",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "IsEmpateValid", "IsPuntajeEquipoValid" },
                values: new object[] { true, true });

            migrationBuilder.UpdateData(
                table: "Eventos",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "IsEmpateValid", "IsPuntajeEquipoValid" },
                values: new object[] { true, true });

            migrationBuilder.UpdateData(
                table: "Eventos",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "IsEmpateValid", "IsPuntajeEquipoValid" },
                values: new object[] { true, true });

            migrationBuilder.UpdateData(
                table: "Eventos",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "IsEmpateValid", "IsPuntajeEquipoValid" },
                values: new object[] { true, true });

            migrationBuilder.UpdateData(
                table: "Eventos",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "IsEmpateValid", "IsPuntajeEquipoValid" },
                values: new object[] { true, true });

            migrationBuilder.UpdateData(
                table: "Eventos",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "IsEmpateValid", "IsPuntajeEquipoValid" },
                values: new object[] { true, true });

            migrationBuilder.UpdateData(
                table: "Eventos",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "IsEmpateValid", "IsPuntajeEquipoValid" },
                values: new object[] { true, true });

            migrationBuilder.UpdateData(
                table: "Eventos",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "IsEmpateValid", "IsPuntajeEquipoValid" },
                values: new object[] { true, true });

            migrationBuilder.UpdateData(
                table: "Eventos",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "IsEmpateValid", "IsPuntajeEquipoValid" },
                values: new object[] { true, true });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsEmpateValid",
                table: "Eventos");

            migrationBuilder.DropColumn(
                name: "IsPuntajeEquipoValid",
                table: "Eventos");
        }
    }
}
