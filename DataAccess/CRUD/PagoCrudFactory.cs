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

            sqlOperation.AddStringParam("Estado  ", planPago.Estado);
            sqlOperation.AddIntParam("IdTipoPendiente  ", planPago.IdTipoPendiente);
            sqlOperation.AddDecimalParam("PorcentajeTerreno  ", planPago.PorcentajeTerreno);
            sqlOperation.AddDecimalParam("PorcentajeHidrico  ", planPago.PorcentajeHidrico);
            sqlOperation.AddIntParam("IdConfiguracionParametros  ", planPago.IdConfiguracionParametros);

            SqlDAO.ExecuteProcedure(sqlOperation);
        }


        public  void CreatePago(BaseDTO baseDTO)
        {
        
            var pago = baseDTO as PagoMensual;
            var sqlOperation = new SqlOperation();
            sqlOperation.ProcedureName = "SP_CREATE_PAGO_MENSUALES";

            sqlOperation.AddIntParam("IdPropiedad ", pago.IdPropiedad);
            sqlOperation.AddIntParam("NumeroMes ", pago.NumeroMes);
            sqlOperation.AddDateTimeParam("FechaPago ", pago.FechaPago);
            sqlOperation.AddDecimalParam("Monto ", pago.Monto);
            sqlOperation.AddStringParam("Estado ", pago.Estado);



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

        public List<T> RetrieveAllPlanesPendientes<T>()
        {
            var lista = new List<T>();
            var sqlOperation = new SqlOperation();
            sqlOperation.ProcedureName = "SP_PLANES_PENDIENTES";

            var planes = SqlDAO.ExecuteQueryProcedure(sqlOperation);


            if (planes.Count > 0)
            {
                foreach (var item in planes)
                {
                    var plan = buildPlan(item);
                    lista.Add((T)Convert.ChangeType(plan, typeof(T)));

                }
                
            }
            return lista;
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

        public Parametros RetrieveParametro()
        {
            var sqlOperation = new SqlOperation();
            sqlOperation.ProcedureName = "SP_OPTENER_PARAMETROS";

            var resultados = SqlDAO.ExecuteQueryProcedure(sqlOperation);

            if (resultados.Count > 0)
            {
                return buildParametro(resultados[0]);
            }

            return null;
        }

        public PlanPago RetrievePlanPagoById(int Id)
        {
            var sqlOperation = new SqlOperation();
            sqlOperation.ProcedureName = "SP_PLANES_APROBADO_BY_ID";
            sqlOperation.AddIntParam("Id", Id);
            var resultados = SqlDAO.ExecuteQueryProcedure(sqlOperation);

            if (resultados.Count > 0)
            {
                return buildPlan(resultados[0]);
            }

            return null;
        }


        public Pendiente RetrieveParametroPendiente(int Id)
        {
            var sqlOperation = new SqlOperation();
            sqlOperation.ProcedureName = "SP_TIPO_PENDIENTE_byID";
            sqlOperation.AddIntParam("Id", Id);
            var resultados = SqlDAO.ExecuteQueryProcedure(sqlOperation);

            if (resultados.Count > 0)
            {
                return buildPendiente(resultados[0]);
            }

            return null;
        }


        public TipoBosque RetrieveParametroBosque(int Id)
        {
            var sqlOperation = new SqlOperation();
            sqlOperation.ProcedureName = "SP_TIPOBOSQUE_ACTIVO_byId";
            sqlOperation.AddIntParam("Id", Id);
            var resultados = SqlDAO.ExecuteQueryProcedure(sqlOperation);

            if (resultados.Count > 0)
            {
                return buildBosquebYiD(resultados[0]);
            }

            return null;
        }




        public override T RetrieveById<T>(int id)
        {
            throw new NotImplementedException();
        }

        public override void Update(BaseDTO baseDTO)
        {
           
            throw new NotImplementedException();
        }

        public  void ApprobarPlan(BaseDTO baseDTO)
        {
            var plan = baseDTO as PlanPago;
            var sqlOperation = new SqlOperation();
            sqlOperation.ProcedureName = "SP_PLANES_APROBAR";

            sqlOperation.AddIntParam("Id", plan.IdPropiedad);


            SqlDAO.ExecuteProcedure(sqlOperation);
            
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


        private TipoBosque buildBosquebYiD(Dictionary<String, object> row)
        {
            var bosque = new TipoBosque()
            {
                
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

        private Parametros buildParametro(Dictionary<String, object> row)
        {
            var parametros = new Parametros()
            {
                PrecioPorHectarea = (decimal)row["PrecioPorHectarea"],
                PorcentajeRios = (decimal)row["PorcentajeRios"],
                PorcentajeNaciente = (decimal)row["PorcentajeNaciente"]
            };


            return parametros;
        }



        private PlanPago buildPlan(Dictionary<String, object> row)
        {
            var planPago = new PlanPago()
            {
                IdPropiedad = (int)row["idPropiedad"],
                FechaCalculo = (DateTime)row["FechaCalculo"],
                PrecioBaseHectarea = (decimal)row["PrecioBaseHectarea"],
                PorcentajeBosque = (decimal)row["PorcentajeBosque"],
                PorcentajeTerreno = (decimal)row["PorcentajeTerreno"],
                PorcentajeHidrico = (decimal)row["PorcentajeHidrico"],
                Estado = (string)row["Estado"],
                TotalPago = (decimal)row["TotalPago"]
            };


            return planPago;
        }



    }

    




    }
