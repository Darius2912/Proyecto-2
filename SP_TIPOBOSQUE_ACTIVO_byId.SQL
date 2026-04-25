 create procedure SP_TIPOBOSQUE_ACTIVO_byId
 @Id int
AS
BEGIN
 select NombreBosque,PorcentajePago from tipobosque where Estado = 1 and IdTipoBosque = @Id;
END;
EXEC SP_TIPOBOSQUE_ACTIVO_byId 1