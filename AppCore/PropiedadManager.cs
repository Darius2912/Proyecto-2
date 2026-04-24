using DataAccess.CRUD;
using Entities_DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore
{
    public class PropiedadManager : BaseManager
    {

        public int Create(PropiedadDTO p)
        {
            try
            {

              
                var pCrud = new PropiedadCrudFactory();
               
                return pCrud.CreateAndReturnId(p); 

            }
            catch (Exception ex)
            {
                ManegerException(ex);
                return 0;
            }
        }
        public PropiedadDTO ObtenerPorId(int id)
        {
            try
            {
                var pCrud = new PropiedadCrudFactory();
                return pCrud.RetrieveById(id);
            }
            catch (Exception ex)
            {
                ManegerException(ex);
                return null;
            }
        }

        public List<string> ObtenerProvincias()
        {
            return new PropiedadCrudFactory().ObtenerProvincias();
        }

        public List<string> ObtenerCantones(string provincia)
        {
            return new PropiedadCrudFactory().ObtenerCantones(provincia);
        }

        public List<string> ObtenerDistritos(string canton)
        {
            return new PropiedadCrudFactory().ObtenerDistritos(canton);
        }

        public void UpdateDesdeEvaluacion(EvaluacionDTO dto)
        {
            var propiedad = new PropiedadDTO
            {
                Id = dto.PropiedadId,
                TamanoHectareas = dto.TamanoHectareas,
                TipoSuperficie = dto.TipoSuperficie,
                TieneRio = dto.TieneRio,
                Nacientes = dto.Nacientes,
                CantidadNacientes = dto.CantidadNacientes,
                TipoVegetacion = dto.TipoVegetacion,
                UsoSuelo = dto.UsoSuelo
            };

            var crud = new PropiedadCrudFactory();
            crud.Update(propiedad);
        }

        public List<PropiedadDTO> ObtenerPorEstado(string Estado)
        {
            try
            {
                var pCrud = new PropiedadCrudFactory();
                return pCrud.RetrieveByEstado(Estado);
            }
            catch (Exception ex)
            {
                ManegerException(ex);
                return new List<PropiedadDTO>();
            }
        }

        public List<PropiedadDTO> ObtenerPorUsuario(int idUsuario)
        {
            var crud = new PropiedadCrudFactory();
            return crud.RetrieveByUsuario(idUsuario);
        }

        public List<PropiedadDTO> RetrieveApprovedPropertiesByUsuarioId(int idUsuario)
        {
            var crud = new PropiedadCrudFactory();
            return crud.RetrieveApprovedPropertiesByUsuarioId(idUsuario);
        }

        public void CreateFoto(int propiedadId, string rutaFoto)
        {
            try
            {


                var pCrud = new PropiedadCrudFactory();
                pCrud.CreateFoto(propiedadId, rutaFoto);
                return ;

            }
            catch (Exception ex)
            {
                ManegerException(ex);
              
            }
        }
    }
}
