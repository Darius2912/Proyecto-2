create procedure SP_TIPOBOSQUE_ACTIVO

AS
BEGIN
 select IdTipoBosque,NombreBosque,PorcentajePago from tipobosque where Estado = 1;
END;