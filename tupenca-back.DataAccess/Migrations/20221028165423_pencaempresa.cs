using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace tupenca_back.DataAccess.Migrations
{
    public partial class pencaempresa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pencas_Empresas_EmpresaId",
                table: "Pencas");

            migrationBuilder.AddForeignKey(
                name: "FK_Pencas_Empresas_EmpresaId",
                table: "Pencas",
                column: "EmpresaId",
                principalTable: "Empresas",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pencas_Empresas_EmpresaId",
                table: "Pencas");

            migrationBuilder.AddForeignKey(
                name: "FK_Pencas_Empresas_EmpresaId",
                table: "Pencas",
                column: "EmpresaId",
                principalTable: "Empresas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
