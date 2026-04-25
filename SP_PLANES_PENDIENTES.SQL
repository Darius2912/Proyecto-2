  create procedure SP_PLANES_PENDIENTES
  as
  begin
  select idPropiedad, FechaCalculo, PrecioBaseHectarea, PorcentajeBosque, PorcentajeTerreno, PorcentajeHidrico,Estado ,TotalPago from PlanPago
  WHERE Estado = 'Pendiente';

  end;