create procedure sp_RegistrarUsuario(
@Correo varchar(100),
@Contrasena varchar(500),

@Cedula varchar(11),
@Nombre varchar(50),
@Apellido varchar(50),
@Telefono varchar(11),
@FechaRegistro dateTime,

@Registrado bit output,
@Mensaje varchar(100) output
)
as
begin

if(not exists(select * from USUARIO where Correo = @Correo))
begin
insert into USUARIO(Correo,Contrasena,Cedula,Nombre,Apellido,Telefono,FechaRegistro) VALUES (@Correo, @Contrasena,@Cedula,@Nombre,@Apellido,@Telefono,@FechaRegistro)
SET @Registrado = 1
SET @Mensaje = 'Usuario registrado'
end
else
begin
set @Registrado = 0
set @Mensaje = 'correo ya existe'

end

end
