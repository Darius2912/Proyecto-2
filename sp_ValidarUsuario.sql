ALTER proc [dbo].[sp_ValidarUsuario](
@Correo varchar(100),
@Contrasena varchar(500)
)
as
begin

if(exists(select * from USUARIO WHERE Correo = @Correo and Contrasena = @Contrasena))
select IdUsuario, Rol,Correo from USUARIO where Correo = @Correo and Contrasena = @Contrasena
else
select '0'

end