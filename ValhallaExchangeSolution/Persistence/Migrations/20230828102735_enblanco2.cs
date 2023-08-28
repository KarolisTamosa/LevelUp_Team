using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class enblanco2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"--Insertar procedimientos almacenados
CREATE PROCEDURE GetHistorialFromUsuarios

    @UsuarioId UNIQUEIDENTIFIER,

    @NumResultados INT

AS

BEGIN

    SELECT TOP (@NumResultados) *

    FROM Historial

    WHERE IdUsuario = @UsuarioId AND Eliminado = 0

    ORDER BY FechaConversion DESC

END;");

            

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
