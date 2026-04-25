  ALTER procedure SP_PLANES_APROBADO_BY_ID
  @Id int
  as
  begin
  select idPropiedad, FechaCalculo, PrecioBaseHectarea, PorcentajeBosque, PorcentajeTerreno, PorcentajeHidrico,Estado ,TotalPago from PlanPago
  WHERE Estado = 'APROBADO' and IdPropiedad = @Id;

  end;