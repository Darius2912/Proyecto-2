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

        public List<ReporteDTO> RetrieveReportes(string provincia, string canton, string distrito, DateTime? desde, DateTime? hasta)
        {
            var lista = new List<ReporteDTO>();

            var op = new SqlOperation();
            op.ProcedureName = "sp_ReporteEvaluaciones";

            op.AddStringParam("Provincia", provincia ?? "");
            op.AddStringParam("Canton", canton ?? "");
            op.AddStringParam("Distrito", distrito ?? "");
            op.AddDateTimeParam("Desde", desde ?? new DateTime(1753, 1, 1));
            op.AddDateTimeParam("Hasta", hasta ?? DateTime.Now);


            var results = SqlDAO.ExecuteQueryProcedure(op);

            foreach (var row in results)
            {
                lista.Add(new ReporteDTO
                {
                    NombreFinca = row["NombreFinca"].ToString(),
                    Fecha = row["Fecha"] != DBNull.Value ? Convert.ToDateTime(row["Fecha"]) : DateTime.Now,
                    Estado = row["Estado"].ToString(),
                    Observaciones = row["Observaciones"]?.ToString(),
                    PropiedadId = Convert.ToInt32(row["PropiedadId"]),
                    Ubicacion = row["Ubicacion"].ToString(), // 🔥
                    TamanoHectareas = Convert.ToDecimal(row["TamanoHectareas"])
                });
            }

            return lista;
        }

        public ReporteDTO RetrieveByPropiedad(int propiedadId)
        {
            var op = new SqlOperation();
            op.ProcedureName = "sp_ObtenerEvaluacionPorPropiedad";
            op.AddIntParam("PropiedadId", propiedadId);

            var results = SqlDAO.ExecuteQueryProcedure(op);

            if (results.Count == 0)
                return null;

            var primera = results[0];

            var dto = new ReporteDTO
            {
                PropiedadId = Convert.ToInt32(primera["PropiedadId"]),
                NombreFinca = primera["NombreFinca"].ToString(),
                Ubicacion = primera["Ubicacion"].ToString(),
                TamanoHectareas = Convert.ToDecimal(primera["TamanoHectareas"]),
                Estado = primera["Estado"].ToString(),
                Observaciones = primera["Observaciones"]?.ToString(),
                Fecha = Convert.ToDateTime(primera["FechaEvaluacion"]),
                Fotos = new List<string>()
            };

            // 🔥 AQUÍ VA ESTO
            foreach (var row in results)
            {
                if (row.ContainsKey("RutaFoto") && row["RutaFoto"] != DBNull.Value)
                {
                    dto.Fotos.Add(row["RutaFoto"].ToString());
                }
            }

            return dto;
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
