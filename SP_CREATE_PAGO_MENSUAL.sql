
CREATE PROCEDURE SP_CREATE_PAGO_MENSUALES
(
    
    @IdPropiedad INT,
    @NumeroMes INT,
    @FechaPago DATE,
    @Monto DECIMAL(18,2),
    @Estado VARCHAR(50)
)
AS
BEGIN
    INSERT INTO PagoMensuales
    (
        
        IdPropiedad,
        NumeroMes,
        FechaPago,
        Monto,
        Estado
    )
    VALUES
    (
        
        @IdPropiedad,
        @NumeroMes,
        @FechaPago,
        @Monto,
        @Estado
    );
END;