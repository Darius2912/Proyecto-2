create procedure SP_MARCAR_TOKEN_USADO
@Id INT
AS BEGIN
UPDATE RecuperacionContrasena set Usado = 1 where Id = @Id
END