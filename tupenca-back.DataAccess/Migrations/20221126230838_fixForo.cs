using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace tupenca_back.DataAccess.Migrations
{
    public partial class fixForo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Foros_Personas_UsuarioId",
                table: "Foros");

            migrationBuilder.DropIndex(
                name: "IX_Foros_UsuarioId",
                table: "Foros");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Foros_UsuarioId",
                table: "Foros",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Foros_Personas_UsuarioId",
                table: "Foros",
                column: "UsuarioId",
                principalTable: "Personas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
