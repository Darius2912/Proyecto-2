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
        public void Create(EvaluacionDTO e)
        {
            var crud = new EvaluacionCrudFactory();
            crud.Create(e);
        }

        public List<ReporteDTO> ObtenerReportes(int? provincia, int? canton, int? distrito, DateTime? desde, DateTime? hasta)
        {
            var factory = new EvaluacionCrudFactory();
            return factory.RetrieveReportes(provincia, canton, distrito, desde, hasta);
        }

    }
}
