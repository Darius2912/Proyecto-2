
CREATE TRIGGER TR_USUARIO_UPDATE_HISTORIAL
ON Usuario
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO UsuarioHistorial (
        Cedula,
        Nombre,
        Apellido,
        Correo,
        Estado,
        Rol,
        Telefono,
        FechaCambio,
        TipoCambio
    )
    SELECT 
        d.cedula,
        d.nombre,
        d.apellido,
        d.correo,
        d.estado,
        d.rol,
        d.telefono,
        GETDATE(),
        'UPDATE'
    FROM deleted d;
END;
