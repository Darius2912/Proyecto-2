using DataAccess.DAO;
using Entities_DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.CRUD
{
    public class EvaluacionCrudFactory : CrudFactory
    {
        public EvaluacionCrudFactory()
        {
            SqlDAO = SqlDAO.GetInstance();
        }
        public override void Create(BaseDTO baseDTO)
        {
            throw new NotImplementedException();
        }
        public void Create(EvaluacionDTO e)
        {
            var operation = new SqlOperation();
            operation.ProcedureName = "CrearEvaluacion";

            operation.AddIntParam("PropiedadId", e.PropiedadId);
            operation.AddIntParam("IdUsuario", e.IdUsuario);
            operation.AddStringParam("Estado", e.Estado);
            operation.AddStringParam("Observaciones", e.Observaciones);
            operation.AddDateTimeParam("FechaEvaluacion", e.FechaEvaluacion);

            SqlDAO.ExecuteProcedure(operation);
        }

        public List<ReporteDTO> RetrieveReportes(int? provincia, int? canton, int? distrito, DateTime? desde, DateTime? hasta)
        {
            var lista = new List<ReporteDTO>();

            var op = new SqlOperation();
            op.ProcedureName = "sp_ReporteEvaluaciones";

            op.AddIntParam("Provincia", provincia ?? 0);
            op.AddIntParam("Canton", canton ?? 0);
            op.AddIntParam("Distrito", distrito ?? 0);
            op.AddDateTimeParam("Desde", desde ?? DateTime.MinValue);
            op.AddDateTimeParam("Hasta", hasta ?? DateTime.MaxValue);

            var results = SqlDAO.ExecuteQueryProcedure(op);

            foreach (var row in results)
            {
                lista.Add(new ReporteDTO
                {
                    NombreFinca = row["NombreFinca"].ToString(),
                    Fecha = Convert.ToDateTime(row["Fecha"]),
                    Estado = row["Estado"].ToString(),
                    Observaciones = row["Observaciones"]?.ToString()
                });
            }

            return lista;
        }
        public override void Delete(BaseDTO baseDTO)
        {
            throw new NotImplementedException();
        }

        public override List<T> RetrieveAll<T>()
        {
            throw new NotImplementedException();
        }

        public override T RetrieveById<T>(int id)
        {
            throw new NotImplementedException();
        }

        public override void Update(BaseDTO baseDTO)
        {
            throw new NotImplementedException();
        }
    }
}
