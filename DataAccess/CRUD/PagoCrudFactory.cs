using DataAccess.DAO;
using Entities_DTOs;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.CRUD
{
    public class PagoCrudFactory : CrudFactory
    {

        public PagoCrudFactory() {
            SqlDAO = SqlDAO.GetInstance();
        }

        public override void Create(BaseDTO baseDTO)
        {
            var planPago = baseDTO as PlanPago;
            var sqlOperation = new SqlOperation();
            sqlOperation.ProcedureName = "SP_CREATE_PLAN";

            sqlOperation.AddIntParam("IdPropiedad ", planPago.IdPropiedad);

            sqlOperation.AddIntParam("IdTipoBosque  ", planPago.IdTipoBosque);
            sqlOperation.AddDecimalParam("PrecioBaseHectarea  ", planPago.PrecioBaseHectarea);
            sqlOperation.AddDecimalParam("PorcentajeBosque  ", planPago.PorcentajeBosque);
            sqlOperation.AddDecimalParam("TotalPago  ", planPago.TotalPago);
            sqlOperation.AddDateTimeParam("FechaCalculo  ", planPago.FechaCalculo);

            sqlOperation.AddBoolParam("Estado  ", planPago.Estado);
            sqlOperation.AddIntParam("IdTipoPendiente  ", planPago.IdTipoPendiente);
            sqlOperation.AddDecimalParam("PorcentajeTerreno  ", planPago.PorcentajeTerreno);
            sqlOperation.AddDecimalParam("PorcentajeHidrico  ", planPago.PorcentajeHidrico);
            sqlOperation.AddIntParam("IdConfiguracionParametros  ", planPago.IdConfiguracionParametros);

            SqlDAO.ExecuteProcedure(sqlOperation);
        }

        public override void Delete(BaseDTO baseDTO)
        {
            throw new NotImplementedException();
        }

        public override List<T> RetrieveAll<T>()
        {
            throw new NotImplementedException();
        }


        public  List<T> RetrieveAllBosques<T>()
        {
            var listaBosques = new List<T>();
            var sqlOperation = new SqlOperation();
            sqlOperation.ProcedureName = "SP_TIPOBOSQUE_ACTIVO";

            var bosques = SqlDAO.ExecuteQueryProcedure(sqlOperation);

            if (bosques.Count > 0)
            {
                foreach (var item in bosques)
                {
                    var bosque = buildBosque(item);
                    listaBosques.Add((T)Convert.ChangeType(bosque, typeof(T)));
                }
            }

            return listaBosques;
        }


        public List<T> RetrieveAllPendientes<T>()
        {
            var listaPendientes = new List<T>();
            var sqlOperation = new SqlOperation();
            sqlOperation.ProcedureName = "SP_TIPO_PENDIENTE";

            var pendientes = SqlDAO.ExecuteQueryProcedure(sqlOperation);

            if (pendientes.Count > 0)
            {
                foreach (var item in pendientes)
                {
                    var pendiente = buildPendiente(item);
                    listaPendientes.Add((T)Convert.ChangeType(pendiente, typeof(T)));
                }
            }

            return listaPendientes;
        }

        public override T RetrieveById<T>(int id)
        {
            throw new NotImplementedException();
        }

        public override void Update(BaseDTO baseDTO)
        {
            throw new NotImplementedException();
        }




        private TipoBosque buildBosque(Dictionary<String, object> row)
        {
            var bosque = new TipoBosque() {
            Id = (int)row["IdTipoBosque"],
            NombreBosque = (string)row["NombreBosque"],
            PorcentajePago = (decimal)row["PorcentajePago"]
            };


            return bosque;
        }

        private Pendiente buildPendiente(Dictionary<String, object> row)
        {
            var pendiente = new Pendiente()
            {
                Id = (int)row["IdPendiente"],
                NombrePendiente = (string)row["NombrePendiente"],
                PorcentajePago = (decimal)row["PorcentajePago"]
            };


            return pendiente;
        }


    }




 

}
