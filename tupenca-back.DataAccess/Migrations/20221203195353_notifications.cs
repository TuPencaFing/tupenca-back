using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace tupenca_back.DataAccess.Migrations
{
    public partial class notifications : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsuarioPremios");

            migrationBuilder.DropColumn(
                name: "PremiosEntregados",
                table: "Pencas");

            migrationBuilder.DropColumn(
                name: "PremiosEntregados",
                table: "Campeonatos");

            migrationBuilder.CreateTable(
                name: "NotificationUserDeviceIds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    deviceId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationUserDeviceIds", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PersonaResetPassword",
                columns: table => new
                {
                    Token = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PersonaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonaResetPassword", x => x.Token);
                    table.ForeignKey(
                        name: "FK_PersonaResetPassword_Personas_PersonaId",
                        column: x => x.PersonaId,
                        principalTable: "Personas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Predicciones_EventoId",
                table: "Predicciones",
                column: "EventoId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonaResetPassword_PersonaId",
                table: "PersonaResetPassword",
                column: "PersonaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Predicciones_Eventos_EventoId",
                table: "Predicciones",
                column: "EventoId",
                principalTable: "Eventos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Predicciones_Eventos_EventoId",
                table: "Predicciones");

            migrationBuilder.DropTable(
                name: "NotificationUserDeviceIds");

            migrationBuilder.DropTable(
                name: "PersonaResetPassword");

            migrationBuilder.DropIndex(
                name: "IX_Predicciones_EventoId",
                table: "Predicciones");

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
                    Banco = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CuentaBancaria = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdPenca = table.Column<int>(type: "int", nullable: false),
                    IdUsuario = table.Column<int>(type: "int", nullable: false),
                    PendientePago = table.Column<bool>(type: "bit", nullable: false),
                    Premio = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Reclamado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioPremios", x => x.Id);
                });
        }
    }
}
