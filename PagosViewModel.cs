using System.Collections.Generic;

namespace WebApp.Models
{
    public class PagosViewModel
    {
        public string PropiedadSeleccionada { get; set; }
        public string MesSeleccionado { get; set; }
        public int AnioSeleccionado { get; set; }

        public decimal PagoBase { get; set; }

        public decimal Vegetacion { get; set; }
        public decimal RecursosHidricos { get; set; }
        public decimal Pendiente { get; set; }

        public decimal TotalPago { get; set; }

        public string Mensaje { get; set; }

        public List<PagoHistorial> Historial { get; set; } = new();
        public List<PagoHistorial> HistorialFiltrado { get; set; } = new();

        public PagoHistorial PagoDetalle { get; set; }
    }
}
