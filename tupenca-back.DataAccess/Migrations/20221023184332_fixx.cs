using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace tupenca_back.DataAccess.Migrations
{
    public partial class fixx : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EmpresaId",
                table: "Personas",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Personas_EmpresaId",
                table: "Personas",
                column: "EmpresaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Personas_Empresas_EmpresaId",
                table: "Personas",
                column: "EmpresaId",
                principalTable: "Empresas",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Personas_Empresas_EmpresaId",
                table: "Personas");

            migrationBuilder.DropIndex(
                name: "IX_Personas_EmpresaId",
                table: "Personas");

            migrationBuilder.DropColumn(
                name: "EmpresaId",
                table: "Personas");
        }
    }
}
