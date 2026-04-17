using System;
using System.Collections.Generic;

namespace WebApp.Models
{
    public class PagosViewModel
    {
        // =========================
        // DATOS DE ENTRADA
        // =========================
        public string PropiedadSeleccionada { get; set; } = "Finca Los Pinos";
        public int AnioSeleccionado { get; set; } = DateTime.Now.Year;

        public decimal Hectareas { get; set; }
        public decimal PrecioPorHectarea { get; set; }

        public decimal Vegetacion { get; set; }
        public decimal RecursosHidricos { get; set; }
        public decimal Pendiente { get; set; }

        public decimal MaxPorcentaje { get; set; } = 40;

        // =========================
        // RESULTADOS
        // =========================
        public decimal PagoBase { get; set; }
        public decimal TotalPago { get; set; }

        // =========================
        // FACTURA
        // =========================
        public string NumeroFactura { get; set; } = "";
        public DateTime FechaPago { get; set; }

        // =========================
        // MENSAJES
        // =========================
        public string Mensaje { get; set; } = "";
        public string Auditoria { get; set; } = "";

        // =========================
        // LISTAS
        // =========================
        public List<PagoHistorial> Historial { get; set; } = new();
        public List<PagoHistorial> HistorialFiltrado { get; set; } = new();
        public List<PagoHistorial> PlanPagos { get; set; } = new();

        public PagoHistorial? PagoDetalle { get; set; }
    }

}