CREATE PROCEDURE sp_InsertarPropiedadFoto
(
    @PropiedadId INT,
    @RutaFoto NVARCHAR(300)
)
AS
BEGIN
    INSERT INTO PropiedadFoto
    (
        PropiedadId,
        RutaFoto
    )
    VALUES
    (
        @PropiedadId,
        @RutaFoto
    )
END