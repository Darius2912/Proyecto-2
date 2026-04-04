CREATE PROCEDURE SP_CREATE_TOKEN
    @Correo      NVARCHAR(150),
    @Token       NVARCHAR(200),
    @FechaExpira DATETIME,
    @Usado       BIT
AS
Begin
    INSERT INTO RecuperacionContrasena (Correo, Token, FechaExpira, Usado)
    VALUES (@Correo, @Token, @FechaExpira, @Usado)
end