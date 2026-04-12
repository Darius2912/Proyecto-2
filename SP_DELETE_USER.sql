alter PROCEDURE DELL_USER_PR

@P_Cedula varchar(100)

as
BEGIN
DELETE  FROM Usuario
WHERE Usuario.Cedula = @P_Cedula;

END