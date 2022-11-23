using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace tupenca_back.DataAccess.Migrations
{
    public partial class addfixEmpresaPuntaje : Migration
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
                name: "PlanId",
                table: "Pencas");

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

            migrationBuilder.AddColumn<int>(
                name: "PuntajeId",
                table: "Pencas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PlanId",
                table: "Empresas",
                type: "int",
                nullable: false,
                defaultValue: 0);

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
                name: "Puntajes");

            migrationBuilder.DropIndex(
                name: "IX_Pencas_PuntajeId",
                table: "Pencas");

            migrationBuilder.DropIndex(
                name: "IX_Empresas_PlanId",
                table: "Empresas");


            migrationBuilder.DropColumn(
                name: "Score",
                table: "Predicciones");

            migrationBuilder.DropColumn(
                name: "CantPencas",
                table: "Planes");

            migrationBuilder.DropColumn(
                name: "PuntajeId",
                table: "Pencas");

            migrationBuilder.DropColumn(
                name: "PlanId",
                table: "Empresas");

            migrationBuilder.AlterColumn<int>(
                name: "PencaId",
                table: "Predicciones",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

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
