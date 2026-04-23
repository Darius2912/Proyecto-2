  create procedure SP_PROPIEDADES_BYID_APROBADAS
  
  @IdUsuario int
  
  AS 
  BEGIN
  
  select * from Propiedad 
  where Propiedad.IdUsuario = @IdUsuario and Estado = 'Aprobada'
  ;

  END;