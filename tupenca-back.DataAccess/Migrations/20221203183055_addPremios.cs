using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace tupenca_back.DataAccess.Migrations
{
    public partial class addPremios : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "PremiosEntregados",
                table: "Pencas",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "PremiosEntregados",
                table: "Campeonatos",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "UsuarioPremios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Premio = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Banco = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CuentaBancaria = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PendientePago = table.Column<bool>(type: "bit", nullable: false),
                    Reclamado = table.Column<bool>(type: "bit", nullable: false),
                    IdUsuario = table.Column<int>(type: "int", nullable: false),
                    IdPenca = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioPremios", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsuarioPremios");

            migrationBuilder.DropColumn(
                name: "PremiosEntregados",
                table: "Pencas");

            migrationBuilder.DropColumn(
                name: "PremiosEntregados",
                table: "Campeonatos");
        }
    }
}
