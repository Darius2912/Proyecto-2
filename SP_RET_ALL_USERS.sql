
CREATE PROCEDURE SP_RET_ALL_USERS

AS
BEGIN
SELECT cedula,Nombre,Apellido,Correo,Telefono,Estado,Rol  from Usuario

END;