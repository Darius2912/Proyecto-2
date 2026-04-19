
CREATE PROCEDURE sp_InsertarPropiedad
(
    @NombreFinca NVARCHAR(150),
    @Ubicacion NVARCHAR(250),
    @Latitud DECIMAL(10,6),
    @Longitud DECIMAL(10,6),
    @TamanoHectareas DECIMAL(18,2),
    @TipoSuperficie NVARCHAR(100),
    @TieneRio NVARCHAR(50),
    @Nacientes NVARCHAR(50),
    @CantidadNacientes INT,
    @TipoVegetacion NVARCHAR(100),
    @UsoSuelo NVARCHAR(100),
    @Estado NVARCHAR(50),
    @FechaRegistro DATETIME
)
AS
BEGIN
    INSERT INTO Propiedad
    (
        NombreFinca,
        Ubicacion,
        Latitud,
        Longitud,
        TamanoHectareas,
        TipoSuperficie,
        TieneRio,
        Nacientes,
        CantidadNacientes,
        TipoVegetacion,
        UsoSuelo,
        Estado,
        FechaRegistro
    )
    VALUES
    (
        @NombreFinca,
        @Ubicacion,
        @Latitud,
        @Longitud,
        @TamanoHectareas,
        @TipoSuperficie,
        @TieneRio,
        @Nacientes,
        @CantidadNacientes,
        @TipoVegetacion,
        @UsoSuelo,
        @Estado,
        @FechaRegistro
    );

    SELECT SCOPE_IDENTITY() AS Id;
END
