alter procedure SP_CREATE_PLAN
  (
  @IdPropiedad INT,
  @IdTipoBosque INT,
  @PrecioBaseHectarea DECIMAL(18,2),
  @PorcentajeBosque DECIMAL(18,2),
  @TotalPago DECIMAL(18,2),
  @FechaCalculo DATETIME,
  @Estado nvarchar(100),
  @IdTipoPendiente INT,
  @PorcentajeTerreno DECIMAL(18,2),
  @PorcentajeHidrico DECIMAL(18,2),
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