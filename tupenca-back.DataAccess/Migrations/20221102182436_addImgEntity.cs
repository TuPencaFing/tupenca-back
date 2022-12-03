using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace tupenca_back.DataAccess.Migrations
{
    public partial class addImgEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImagenName",
                table: "Deportes",
                newName: "Image");

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Premios",
                type: "nvarchar(max)",
                nullable: true);

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

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Campeonatos",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Premios");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Personas");

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
                name: "Image",
                table: "Campeonatos");

            migrationBuilder.RenameColumn(
                name: "Image",
                table: "Deportes",
                newName: "ImagenName");

            migrationBuilder.AlterColumn<string>(
                name: "Image",
                table: "Pencas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
