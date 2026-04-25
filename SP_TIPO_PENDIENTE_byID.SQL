 create procedure SP_TIPO_PENDIENTE_byID
 @Id int
 AS
 BEGIN
 
 select NombrePendiente,PorcentajePago from TipoPendiente where Estado = 1 and IdPendiente = @Id
 END;

 exec  SP_TIPO_PENDIENTE_byID 1 