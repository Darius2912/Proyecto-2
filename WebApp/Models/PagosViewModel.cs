using System.Collections.Generic;

namespace WebApp.Models
{
    public class PagosViewModel
    {
        public string PropiedadSeleccionada { get; set; } = "Finca Los Pinos";
        public string MesSeleccionado { get; set; } = "Enero";
        public int AnioSeleccionado { get; set; } = 2026;

        public decimal PagoBase { get; set; } = 100000;

        public decimal Vegetacion { get; set; }
        public decimal RecursosHidricos { get; set; }
        public decimal Pendiente { get; set; }

        // 🔥 REGLA CONFIGURABLE
        public decimal MaxPorcentaje { get; set; } = 40;

        public decimal TotalPago { get; set; }

        public string Mensaje { get; set; } = string.Empty;

        public List<PagoHistorial> Historial { get; set; } = new();
        public List<PagoHistorial> HistorialFiltrado { get; set; } = new();

        // 🔥 PLAN DE PAGOS
        public List<PagoHistorial> PlanPagos { get; set; } = new();

        public PagoHistorial? PagoDetalle { get; set; }
    }
}