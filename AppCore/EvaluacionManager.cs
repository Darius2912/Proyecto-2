using DataAccess.CRUD;
using Entities_DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AppCore
{
    public class EvaluacionManager : BaseManager
    {

        private readonly EvaluacionCrudFactory _factory;

        public EvaluacionManager()
        {
            _factory = new EvaluacionCrudFactory();
        }

        public void Create(EvaluacionDTO e)
        {
            if (e.FechaEvaluacion == DateTime.MinValue)
            {
                e.FechaEvaluacion = DateTime.Now;
            }

            var factory = new EvaluacionCrudFactory();
            factory.Create(e);
        }

        public List<ReporteDTO> ObtenerReportes(string provincia, string canton, string distrito, DateTime? desde, DateTime? hasta)
        {
            var factory = new EvaluacionCrudFactory();
            return factory.RetrieveReportes(provincia, canton, distrito, desde, hasta);
        }
    }
}
