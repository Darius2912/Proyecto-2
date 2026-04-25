 create procedure SP_TIPO_PENDIENTE
 AS
 BEGIN
  select IdPendiente,NombrePendiente,PorcentajePago from TipoPendiente where Estado = 1;
 END;