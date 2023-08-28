using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class enblanco : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Monedas",
                columns: table => new
                {
                    IdMoneda = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Codigo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ValorEnDolares = table.Column<double>(type: "float", nullable: false),
                    Eliminado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Monedas", x => x.IdMoneda);
                });

            migrationBuilder.CreateTable(
                name: "Paises",
                columns: table => new
                {
                    IdPais = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Eliminado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Paises", x => x.IdPais);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    IdUsuario = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordEncriptado = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaNacimiento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Eliminado = table.Column<bool>(type: "bit", nullable: false),
                    IdPais = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.IdUsuario);
                    table.ForeignKey(
                        name: "FK_Usuarios_Paises_IdPais",
                        column: x => x.IdPais,
                        principalTable: "Paises",
                        principalColumn: "IdPais",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Historial",
                columns: table => new
                {
                    IdHistorial = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdUsuario = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdMonedaOrigen = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdMonedaDestino = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FactorCambio = table.Column<double>(type: "float", nullable: false),
                    Importe = table.Column<double>(type: "float", nullable: false),
                    FechaConversion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ResultadoConversion = table.Column<double>(type: "float", nullable: false),
                    Eliminado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Historial", x => x.IdHistorial);
                    table.ForeignKey(
                        name: "FK_Historial_Monedas_IdMonedaDestino",
                        column: x => x.IdMonedaDestino,
                        principalTable: "Monedas",
                        principalColumn: "IdMoneda",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Historial_Monedas_IdMonedaOrigen",
                        column: x => x.IdMonedaOrigen,
                        principalTable: "Monedas",
                        principalColumn: "IdMoneda",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Historial_Usuarios_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuarios",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Historial_IdMonedaDestino",
                table: "Historial",
                column: "IdMonedaDestino");

            migrationBuilder.CreateIndex(
                name: "IX_Historial_IdMonedaOrigen",
                table: "Historial",
                column: "IdMonedaOrigen");

            migrationBuilder.CreateIndex(
                name: "IX_Historial_IdUsuario",
                table: "Historial",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_IdPais",
                table: "Usuarios",
                column: "IdPais");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Historial");

            migrationBuilder.DropTable(
                name: "Monedas");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Paises");
        }
    }
}
