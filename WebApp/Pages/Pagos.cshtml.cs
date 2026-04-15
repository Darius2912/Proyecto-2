using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;

namespace WebApp.Pages
{
    public class PagosModel : PageModel
    {
        [BindProperty]
        public string PropiedadSeleccionada { get; set; } = "Finca Los Pinos";

        [BindProperty]
        public string MesSeleccionado { get; set; } = "Enero";

        [BindProperty]
        public int AnioSeleccionado { get; set; } = 2026;

        [BindProperty]
        public decimal PagoBase { get; set; } = 100000;

        [BindProperty]
        public decimal Vegetacion { get; set; }

        [BindProperty]
        public decimal RecursosHidricos { get; set; }

        [BindProperty]
        public decimal Pendiente { get; set; }

        public decimal TotalPago { get; set; }

        public string Mensaje { get; set; } = "";

        public List<PagoHistorial> Historial { get; set; } = new();
        public List<PagoHistorial> HistorialFiltrado { get; set; } = new();

        public PagoHistorial? PagoDetalle { get; set; }

        public void OnGet()
        {
            Historial = ObtenerHistorial();
            HistorialFiltrado = Historial;
        }

        public void OnPost()
        {
            Historial = ObtenerHistorial();

            // VALIDACIONES
            if (PagoBase <= 0)
                ModelState.AddModelError("", "El pago base debe ser mayor a 0");

            decimal totalPorcentaje = Vegetacion + RecursosHidricos + Pendiente;

            if (totalPorcentaje > 40)
                ModelState.AddModelError("", "Los ajustes no pueden superar el 40%");

            if (!ModelState.IsValid)
            {
                HistorialFiltrado = Historial;
                return;
            }

            // CALCULO
            decimal ajuste = PagoBase * (totalPorcentaje / 100);
            TotalPago = PagoBase + ajuste;

            string periodo = $"{MesSeleccionado} {AnioSeleccionado}";

            // BUSCAR HISTORIAL
            PagoDetalle = Historial
                .FirstOrDefault(p => p.Propiedad == PropiedadSeleccionada && p.Periodo == periodo);

            // SI NO EXISTE ? SIMULAR
            if (PagoDetalle == null)
            {
                Mensaje = "No hay datos históricos para el periodo seleccionado. Se muestra una simulación.";

                PagoDetalle = new PagoHistorial
                {
                    Propiedad = PropiedadSeleccionada,
                    Periodo = periodo,
                    Monto = TotalPago,
                    Estado = "Simulado"
                };
            }

            // FILTRO
            HistorialFiltrado = Historial
                .Where(h => h.Propiedad == PropiedadSeleccionada)
                .ToList();
        }

        public List<PagoHistorial> ObtenerHistorial()
        {
            return new List<PagoHistorial>
            {
                new PagoHistorial { Propiedad = "Finca Los Pinos", Periodo = "Enero 2026", Monto = 123000, Estado = "Pagado" },
                new PagoHistorial { Propiedad = "Finca La Esperanza", Periodo = "Febrero 2026", Monto = 110000, Estado = "En proceso" }
            };
        }
    }

    public class PagoHistorial
    {
        public string Propiedad { get; set; } = "";
        public string Periodo { get; set; } = "";
        public decimal Monto { get; set; }
        public string Estado { get; set; } = "";
    }
}