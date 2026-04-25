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
        


            public List<PlanPago> RetrieveAllPlanesPendientes()
        {

            var list = new List<PlanPago>();

            try
            {
                var pCrud = new PagoCrudFactory();
                list = pCrud.RetrieveAllPlanesPendientes<PlanPago>();

            }
            catch (Exception e)
            {
                ManegerException(e);
            }


            return list;
        }


        public void AprobarPlan(PlanPago p)
        {
            try
            {


                var pCrud = new PagoCrudFactory();
                pCrud.ApprobarPlan(p);
            }
            catch (Exception ex)
            {
                ManegerException(ex);
            }
        }




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

        public PlanPago RetrievePlanPagoById(int Id)
        {

            var list = new PlanPago();

            try
            {
                var pCrud = new PagoCrudFactory();
             return   pCrud.RetrievePlanPagoById( Id);

            }
            catch (Exception e)
            {
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
