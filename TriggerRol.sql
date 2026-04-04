CREATE TRIGGER TRG_ActualizarEstado
ON Usuario
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    IF UPDATE(Rol)
    BEGIN
        UPDATE u
        SET Estado = 
            CASE 
                WHEN i.Rol = 1 THEN 'Activo'
                WHEN i.Rol = 2 THEN 'Adm'
                ELSE 'Inactivo'
            END
        FROM Usuario u
        INNER JOIN inserted i ON u.IdUsuario = i.IdUsuario;
    END
END;