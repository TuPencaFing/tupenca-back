using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace tupenca_back.DataAccess.Migrations
{
    public partial class addPenca : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PencaId",
                table: "Predicciones",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Planes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CantUser = table.Column<int>(type: "int", nullable: false),
                    PercentageCost = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    LookAndFeel = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Planes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pencas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CampeonatoId = table.Column<int>(type: "int", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CostEntry = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    Pozo = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    Commission = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    EmpresaId = table.Column<int>(type: "int", nullable: true),
                    PlanId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pencas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pencas_Campeonatos_CampeonatoId",
                        column: x => x.CampeonatoId,
                        principalTable: "Campeonatos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pencas_Empresas_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "Empresas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pencas_Planes_PlanId",
                        column: x => x.PlanId,
                        principalTable: "Planes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Premios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Position = table.Column<int>(type: "int", nullable: false),
                    Percentage = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    PencaId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Premios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Premios_Pencas_PencaId",
                        column: x => x.PencaId,
                        principalTable: "Pencas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Predicciones_PencaId",
                table: "Predicciones",
                column: "PencaId");

            migrationBuilder.CreateIndex(
                name: "IX_Pencas_CampeonatoId",
                table: "Pencas",
                column: "CampeonatoId");

            migrationBuilder.CreateIndex(
                name: "IX_Pencas_EmpresaId",
                table: "Pencas",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_Pencas_PlanId",
                table: "Pencas",
                column: "PlanId");

            migrationBuilder.CreateIndex(
                name: "IX_Premios_PencaId",
                table: "Premios",
                column: "PencaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Predicciones_Pencas_PencaId",
                table: "Predicciones",
                column: "PencaId",
                principalTable: "Pencas",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Predicciones_Pencas_PencaId",
                table: "Predicciones");

            migrationBuilder.DropTable(
                name: "Premios");

            migrationBuilder.DropTable(
                name: "Pencas");

            migrationBuilder.DropTable(
                name: "Planes");

            migrationBuilder.DropIndex(
                name: "IX_Predicciones_PencaId",
                table: "Predicciones");

            migrationBuilder.DropColumn(
                name: "PencaId",
                table: "Predicciones");
        }
    }
}
