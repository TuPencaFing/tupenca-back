using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace tupenca_back.DataAccess.Migrations
{
    public partial class FixNameUsuarioPremioIdPenca : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IdPenca",
                table: "UsuarioPremios",
                newName: "PencaId");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioPremios_PencaId",
                table: "UsuarioPremios",
                column: "PencaId");

            migrationBuilder.AddForeignKey(
                name: "FK_UsuarioPremios_Pencas_PencaId",
                table: "UsuarioPremios",
                column: "PencaId",
                principalTable: "Pencas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsuarioPremios_Pencas_PencaId",
                table: "UsuarioPremios");

            migrationBuilder.DropIndex(
                name: "IX_UsuarioPremios_PencaId",
                table: "UsuarioPremios");

            migrationBuilder.RenameColumn(
                name: "PencaId",
                table: "UsuarioPremios",
                newName: "IdPenca");
        }
    }
}
