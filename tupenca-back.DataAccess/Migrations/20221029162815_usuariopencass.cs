using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace tupenca_back.DataAccess.Migrations
{
    public partial class usuariopencass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsuarioPenca_Pencas_PencaId",
                table: "UsuarioPenca");

            migrationBuilder.DropForeignKey(
                name: "FK_UsuarioPenca_Personas_UsuarioId",
                table: "UsuarioPenca");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UsuarioPenca",
                table: "UsuarioPenca");

            migrationBuilder.RenameTable(
                name: "UsuarioPenca",
                newName: "UsuariosPencas");

            migrationBuilder.RenameIndex(
                name: "IX_UsuarioPenca_UsuarioId",
                table: "UsuariosPencas",
                newName: "IX_UsuariosPencas_UsuarioId");

            migrationBuilder.RenameIndex(
                name: "IX_UsuarioPenca_PencaId",
                table: "UsuariosPencas",
                newName: "IX_UsuariosPencas_PencaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsuariosPencas",
                table: "UsuariosPencas",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UsuariosPencas_Pencas_PencaId",
                table: "UsuariosPencas",
                column: "PencaId",
                principalTable: "Pencas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsuariosPencas_Personas_UsuarioId",
                table: "UsuariosPencas",
                column: "UsuarioId",
                principalTable: "Personas",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsuariosPencas_Pencas_PencaId",
                table: "UsuariosPencas");

            migrationBuilder.DropForeignKey(
                name: "FK_UsuariosPencas_Personas_UsuarioId",
                table: "UsuariosPencas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UsuariosPencas",
                table: "UsuariosPencas");

            migrationBuilder.RenameTable(
                name: "UsuariosPencas",
                newName: "UsuarioPenca");

            migrationBuilder.RenameIndex(
                name: "IX_UsuariosPencas_UsuarioId",
                table: "UsuarioPenca",
                newName: "IX_UsuarioPenca_UsuarioId");

            migrationBuilder.RenameIndex(
                name: "IX_UsuariosPencas_PencaId",
                table: "UsuarioPenca",
                newName: "IX_UsuarioPenca_PencaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsuarioPenca",
                table: "UsuarioPenca",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UsuarioPenca_Pencas_PencaId",
                table: "UsuarioPenca",
                column: "PencaId",
                principalTable: "Pencas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsuarioPenca_Personas_UsuarioId",
                table: "UsuarioPenca",
                column: "UsuarioId",
                principalTable: "Personas",
                principalColumn: "Id");
        }
    }
}
