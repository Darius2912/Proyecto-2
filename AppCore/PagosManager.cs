using DataAccess.CRUD;
using Entities_DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore
{
    public class PagosManager : BaseManager
    {



        public List<TipoBosque> RetrieveBosques(){

            var list = new List<TipoBosque>();

            try {
                var pCrud = new PagoCrudFactory();
                list = pCrud.RetrieveAllBosques<TipoBosque>();
            
            } catch (Exception e) {
                ManegerException(e);
            }


            return list;
        }


        public List<Pendiente> RetrievePendientes()
        {

            var list = new List<Pendiente>();

            try
            {
                var pCrud = new PagoCrudFactory();
                list = pCrud.RetrieveAllPendientes<Pendiente>();

            }
            catch (Exception e)
            {
                ManegerException(e);
            }


            return list;
        }













    }
}
