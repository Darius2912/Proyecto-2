CREATE PROCEDURE SP_RECOVER_PW_BY_TOKEN_ID
    @Token NVARCHAR(200)
AS
Begin
    SELECT * FROM RecuperacionContrasena
    WHERE Token = @Token
end