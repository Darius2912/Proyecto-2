
  create procedure SP_CREATE_PLAN
  (
  @IdPropiedad INT,
  @IdTipoBosque INT,
  @PrecioBaseHectarea DECIMAL(10,2),
  @PorcentajeBosque DECIMAL(5,2),
  @TotalPago DECIMAL(10,2),
  @FechaCalculo DATETIME,
  @Estado BIT,
  @IdTipoPendiente INT,
  @PorcentajeTerreno DECIMAL(5,2),
  @PorcentajeHidrico DECIMAL(5,2),
  @IdConfiguracionParametros INT
 
  )
  AS
  BEGIN
  INSERT INTO PlanPago
  (IdPropiedad,IdTipoBosque,PrecioBaseHectarea,PorcentajeBosque,TotalPago,FechaCalculo,Estado,IdTipoPendiente,PorcentajeTerreno,PorcentajeHidrico,IdConfiguracionParametros)

  VALUES ( @IdPropiedad ,
  @IdTipoBosque ,
  @PrecioBaseHectarea ,
  @PorcentajeBosque ,
  @TotalPago ,
  @FechaCalculo ,
  @Estado ,
  @IdTipoPendiente ,
  @PorcentajeTerreno ,
  @PorcentajeHidrico,
  @IdConfiguracionParametros 
  )
  END;
