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

        public List<String> ObtenerProvincias()
        {
            var factory = new PropiedadCrudFactory();
            return factory.ObtenerProvincias();
        }

        public List<String> ObtenerCantones(string provincia)
        {
            var factory = new PropiedadCrudFactory();
            return factory.ObtenerCantones(provincia);
        }

        public List<String> ObtenerDistritos(String canton)
        {
            var factory = new PropiedadCrudFactory();
            return factory.ObtenerDistritos(canton);
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
