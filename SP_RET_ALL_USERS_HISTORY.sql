 ALTER procedure SP_RET_ALL_USERS_HISTORY
  AS


  BEGIN
  SELECT cedula,Nombre,Apellido,Correo,Telefono,Estado,Rol, FechaCambio FROM UsuarioHistorial;

  END;