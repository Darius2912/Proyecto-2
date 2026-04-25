  alter procedure SP_PLANES_APROBAR
  @Id int
  as
  begin
  update PlanPago
  set estado = 'Aprobado' where IdPropiedad = @Id;

  END;