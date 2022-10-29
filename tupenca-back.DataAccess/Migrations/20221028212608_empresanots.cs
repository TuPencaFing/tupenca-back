using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace tupenca_back.DataAccess.Migrations
{
    public partial class empresanots : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pencas_Empresas_EmpresaId",
                table: "Pencas");

            migrationBuilder.DropForeignKey(
                name: "FK_Pencas_Empresas_EmpresaId1",
                table: "Pencas");

            migrationBuilder.DropForeignKey(
                name: "FK_Pencas_Pencas_PencaEmpresaId",
                table: "Pencas");

            migrationBuilder.DropForeignKey(
                name: "FK_Personas_Empresas_EmpresaId",
                table: "Personas");

            migrationBuilder.DropIndex(
                name: "IX_Pencas_PencaEmpresaId",
                table: "Pencas");

            migrationBuilder.DropColumn(
                name: "PencaEmpresaId",
                table: "Pencas");

            migrationBuilder.AddForeignKey(
                name: "FK_Pencas_Empresas_EmpresaId",
                table: "Pencas",
                column: "EmpresaId",
                principalTable: "Empresas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Personas_Empresas_EmpresaId",
                table: "Personas",
                column: "EmpresaId",
                principalTable: "Empresas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pencas_Empresas_EmpresaId",
                table: "Pencas");

            migrationBuilder.DropForeignKey(
                name: "FK_Personas_Empresas_EmpresaId",
                table: "Personas");

            migrationBuilder.AddColumn<int>(
                name: "PencaEmpresaId",
                table: "Pencas",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pencas_PencaEmpresaId",
                table: "Pencas",
                column: "PencaEmpresaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pencas_Empresas_EmpresaId",
                table: "Pencas",
                column: "EmpresaId",
                principalTable: "Empresas",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Pencas_Empresas_EmpresaId1",
                table: "Pencas",
                column: "EmpresaId",
                principalTable: "Empresas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pencas_Pencas_PencaEmpresaId",
                table: "Pencas",
                column: "PencaEmpresaId",
                principalTable: "Pencas",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Personas_Empresas_EmpresaId",
                table: "Personas",
                column: "EmpresaId",
                principalTable: "Empresas",
                principalColumn: "Id");
        }
    }
}
