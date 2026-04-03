
CREATE PROCEDURE SP_UPDATE_USER

@P_cedula nvarchar(100),
@P_Nombre nvarchar(100),
@P_Apellido nvarchar(100),
@P_Correo nvarchar(255),
@P_Telefono nvarchar(150),
@P_Estado nvarchar,
@P_Rol int

as
BEGIN

UPDATE Usuario
set
cedula = @P_cedula,
Nombre = @P_Nombre,
Apellido = @P_Apellido,
Correo = @P_Correo,
Telefono = @P_Telefono,
Estado = @P_Estado,
Rol = @P_Rol
WHERE cedula = @P_cedula


END
