 create procedure SP_OPTENER_PARAMETROS
 

AS
BEGIN
 select PrecioPorHectarea,PorcentajeRios,PorcentajeNaciente from ConfiguracionParametros where Id = 3;
END;