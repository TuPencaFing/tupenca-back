using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace tupenca_back.DataAccess.Migrations
{
    public partial class usuariopencas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "habilitado",
                table: "UsuarioPenca",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "score",
                table: "UsuarioPenca",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "habilitado",
                table: "UsuarioPenca");

            migrationBuilder.DropColumn(
                name: "score",
                table: "UsuarioPenca");
        }
    }
}
