using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities_DTOs
{
    public class PagoMensual : BaseDTO
    {
        public int IdPagoMensual { get; set; }

        public int IdPlanPago { get; set; }
        public int IdPropiedad { get; set; }

        public int NumeroMes { get; set; } // 1 a 12

        public DateTime FechaPago { get; set; }

        public decimal Monto { get; set; }

        public string Estado { get; set; } // Pendiente, Pagado

    }
}
