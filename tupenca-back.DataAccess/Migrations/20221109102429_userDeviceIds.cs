using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace tupenca_back.DataAccess.Migrations
{
    public partial class userDeviceIds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pencas_Planes_PlanId",
                table: "Pencas");

            migrationBuilder.DropForeignKey(
                name: "FK_Predicciones_Pencas_PencaId",
                table: "Predicciones");

            migrationBuilder.DropIndex(
                name: "IX_Predicciones_PencaId",
                table: "Predicciones");

            migrationBuilder.DropIndex(
                name: "IX_Pencas_PlanId",
                table: "Pencas");

            migrationBuilder.DropColumn(
                name: "score",
                table: "UsuariosPencas");

            migrationBuilder.DropColumn(
                name: "PlanId",
                table: "Pencas");

            migrationBuilder.RenameColumn(
                name: "ImagenName",
                table: "Deportes",
                newName: "Image");

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Premios",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PencaId",
                table: "Predicciones",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Score",
                table: "Predicciones",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CantPencas",
                table: "Planes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Personas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Image",
                table: "Pencas",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "PuntajeId",
                table: "Pencas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Eventos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Equipos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Empresas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PlanId",
                table: "Empresas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Campeonatos",
                type: "nvarchar(max)",
                nullable: true);

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

            migrationBuilder.CreateTable(
                name: "Puntajes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Resultado = table.Column<int>(type: "int", nullable: false),
                    ResultadoExacto = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Puntajes", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Campeonatos",
                columns: new[] { "Id", "DeporteId", "FinishDate", "Image", "Name", "StartDate" },
                values: new object[] { 10, 1, new DateTime(2022, 11, 28, 10, 0, 0, 0, DateTimeKind.Unspecified), null, "Torneo Clausura", new DateTime(2022, 11, 20, 13, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Planes",
                columns: new[] { "Id", "CantPencas", "CantUser", "LookAndFeel", "PercentageCost" },
                values: new object[,]
                {
                    { 1, 1, 50, 1, 10m },
                    { 2, 5, 100, 2, 10m },
                    { 3, 10, 500, 2, 10m }
                });

            migrationBuilder.InsertData(
                table: "Premios",
                columns: new[] { "Id", "Image", "PencaId", "Percentage", "Position" },
                values: new object[] { 10, null, null, 100m, 1 });

            migrationBuilder.InsertData(
                table: "Puntajes",
                columns: new[] { "Id", "Resultado", "ResultadoExacto" },
                values: new object[] { 10, 1, 3 });

            migrationBuilder.UpdateData(
                table: "Empresas",
                keyColumn: "Id",
                keyValue: 1,
                column: "PlanId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Empresas",
                keyColumn: "Id",
                keyValue: 2,
                column: "PlanId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Empresas",
                keyColumn: "Id",
                keyValue: 3,
                column: "PlanId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Empresas",
                keyColumn: "Id",
                keyValue: 4,
                column: "PlanId",
                value: 3);

            migrationBuilder.InsertData(
                table: "Pencas",
                columns: new[] { "Id", "CampeonatoId", "Commission", "CostEntry", "Description", "Discriminator", "Image", "Pozo", "PuntajeId", "Title" },
                values: new object[] { 12, 10, 0m, 0m, "Gana muchos premios", "PencaCompartida", null, 0m, 10, "Penca Movistar" });

            migrationBuilder.CreateIndex(
                name: "IX_Pencas_PuntajeId",
                table: "Pencas",
                column: "PuntajeId");

            migrationBuilder.CreateIndex(
                name: "IX_Empresas_PlanId",
                table: "Empresas",
                column: "PlanId");

            migrationBuilder.AddForeignKey(
                name: "FK_Empresas_Planes_PlanId",
                table: "Empresas",
                column: "PlanId",
                principalTable: "Planes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pencas_Puntajes_PuntajeId",
                table: "Pencas",
                column: "PuntajeId",
                principalTable: "Puntajes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Empresas_Planes_PlanId",
                table: "Empresas");

            migrationBuilder.DropForeignKey(
                name: "FK_Pencas_Puntajes_PuntajeId",
                table: "Pencas");

            migrationBuilder.DropTable(
                name: "NotificationUserDeviceIds");

            migrationBuilder.DropTable(
                name: "Puntajes");

            migrationBuilder.DropIndex(
                name: "IX_Pencas_PuntajeId",
                table: "Pencas");

            migrationBuilder.DropIndex(
                name: "IX_Empresas_PlanId",
                table: "Empresas");

            migrationBuilder.DeleteData(
                table: "Pencas",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Planes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Planes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Planes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Premios",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Campeonatos",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Premios");

            migrationBuilder.DropColumn(
                name: "Score",
                table: "Predicciones");

            migrationBuilder.DropColumn(
                name: "CantPencas",
                table: "Planes");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Personas");

            migrationBuilder.DropColumn(
                name: "PuntajeId",
                table: "Pencas");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Eventos");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Equipos");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Empresas");

            migrationBuilder.DropColumn(
                name: "PlanId",
                table: "Empresas");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Campeonatos");

            migrationBuilder.RenameColumn(
                name: "Image",
                table: "Deportes",
                newName: "ImagenName");

            migrationBuilder.AddColumn<int>(
                name: "score",
                table: "UsuariosPencas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "PencaId",
                table: "Predicciones",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Image",
                table: "Pencas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PlanId",
                table: "Pencas",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Predicciones_PencaId",
                table: "Predicciones",
                column: "PencaId");

            migrationBuilder.CreateIndex(
                name: "IX_Pencas_PlanId",
                table: "Pencas",
                column: "PlanId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pencas_Planes_PlanId",
                table: "Pencas",
                column: "PlanId",
                principalTable: "Planes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Predicciones_Pencas_PencaId",
                table: "Predicciones",
                column: "PencaId",
                principalTable: "Pencas",
                principalColumn: "Id");
        }
    }
}
