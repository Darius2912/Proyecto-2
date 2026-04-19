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
