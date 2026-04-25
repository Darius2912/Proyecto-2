using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities_DTOs
{
    public class PlanPago : BaseDTO
{

        public int IdPropiedad { get; set; }
        public int IdTipoBosque { get; set; }
        public decimal PrecioBaseHectarea { get; set; }
        public decimal PorcentajeBosque { get; set; }
        public decimal TotalPago { get; set; }
        public DateTime FechaCalculo { get; set; }
        public string Estado { get; set; }
        public int IdTipoPendiente { get; set; }
        public decimal PorcentajeTerreno { get; set; }
        public decimal PorcentajeHidrico { get; set; }
        public int IdConfiguracionParametros { get; set; }

    }
}
