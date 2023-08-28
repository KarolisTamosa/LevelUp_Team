use PruebaBD3


 --Insertar datos en la tabla de Paises
INSERT INTO [dbo].[Paises] ([IdPais], [Nombre], [Eliminado])
VALUES
    (NEWID(), 'País 1', 0),
    (NEWID(), 'País 2', 0),
    (NEWID(), 'País 3', 0),
    (NEWID(), 'País 4', 0),
    (NEWID(), 'País 5', 0),
    (NEWID(), 'País 6', 0),
    (NEWID(), 'País 7', 0),
    (NEWID(), 'País 8', 0),
    (NEWID(), 'País 9', 0),
    (NEWID(), 'País 10', 0),
    (NEWID(), 'País 11', 0),
    (NEWID(), 'País 12', 0),
    (NEWID(), 'País 20', 0);

-- Insertar datos en la tabla de Usuarios relacionando con los países
INSERT INTO [dbo].[Usuarios] ([IdUsuario], [Email], [PasswordEncriptado], [FechaNacimiento], [Eliminado], [IdPais])
VALUES
    (NEWID(), 'usuario1@example.com', '1234', '1990-01-01', 0, (SELECT TOP 1 [IdPais] FROM [dbo].[Paises] ORDER BY NEWID())),
    (NEWID(), 'usuario2@example.com', '1234', '1985-05-10', 0, (SELECT TOP 1 [IdPais] FROM [dbo].[Paises] ORDER BY NEWID())),
    (NEWID(), 'usuario3@example.com', '1234', '1985-05-10', 0, (SELECT TOP 1 [IdPais] FROM [dbo].[Paises] ORDER BY NEWID())),
    (NEWID(), 'usuario4@example.com', '1234', '1985-05-10', 0, (SELECT TOP 1 [IdPais] FROM [dbo].[Paises] ORDER BY NEWID())),
    (NEWID(), 'usuario5@example.com', '1234', '1985-05-10', 0, (SELECT TOP 1 [IdPais] FROM [dbo].[Paises] ORDER BY NEWID())),
    (NEWID(), 'usuario60@example.com', '1234', '1998-09-15', 0, (SELECT TOP 1 [IdPais] FROM [dbo].[Paises] ORDER BY NEWID()));

-- Insertar datos en la tabla de Historial
INSERT INTO [dbo].[Historial] ([IdHistorial], [IdUsuario], [IdMonedaOrigen], [IdMonedaDestino], [FactorCambio], [Importe], [FechaConversion], [ResultadoConversion], [Eliminado])
VALUES
    (NEWID(), (SELECT TOP 1 [IdUsuario] FROM [dbo].[Usuarios] ORDER BY NEWID()), (SELECT TOP 1 [IdMoneda] FROM [dbo].[Monedas] ORDER BY NEWID()), (SELECT TOP 1 [IdMoneda] FROM [dbo].[Monedas] ORDER BY NEWID()), 100, 500.00, GETDATE(), 600.00, 0),
    (NEWID(), (SELECT TOP 1 [IdUsuario] FROM [dbo].[Usuarios] ORDER BY NEWID()), (SELECT TOP 1 [IdMoneda] FROM [dbo].[Monedas] ORDER BY NEWID()), (SELECT TOP 1 [IdMoneda] FROM [dbo].[Monedas] ORDER BY NEWID()), 80, 300.00, GETDATE(), 240.00, 0),
    (NEWID(), (SELECT TOP 1 [IdUsuario] FROM [dbo].[Usuarios] ORDER BY NEWID()), (SELECT TOP 1 [IdMoneda] FROM [dbo].[Monedas] ORDER BY NEWID()), (SELECT TOP 1 [IdMoneda] FROM [dbo].[Monedas] ORDER BY NEWID()), 120, 750.00, GETDATE(), 900.00, 0);

--Insertar procedimientos almacenados
CREATE PROCEDURE GetHistorialFromUsuarios
    @UsuarioId INT,
    @NumResultados INT
AS
BEGIN
    SELECT TOP (@NumResultados) *
    FROM Historial
    WHERE IdUsuario = @UsuarioId AND Eliminado = False
    ORDER BY FechaConversion DESC
END;