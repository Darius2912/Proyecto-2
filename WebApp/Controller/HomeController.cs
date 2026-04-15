using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using System.Linq;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        // =========================
        // INDEX
        // =========================
        public IActionResult Index()
        {
            CargarSesion();
            return View();
        }

        // =========================
        // DASHBOARD
        // =========================
        [RolRequerido(2)]
        public IActionResult Dashboard()
        {
            CargarSesion();
            return View();
        }

        // =========================
        // REPORTES
        // =========================
        [SesionRequerida]
        public IActionResult Reportes()
        {
            CargarSesion();
            return View();
        }

        // =========================
        // REGISTRO PROPIEDADES
        // =========================
        [SesionRequerida]
        public IActionResult RegistroPropiedades()
        {
            CargarSesion();
            return View();
        }

        // =========================
        // MIS PROPIEDADES
        // =========================
        [SesionRequerida]
        public IActionResult MisPropiedades()
        {
            CargarSesion();
            return View();
        }

        // =========================
        // REPORTES ADMIN
        // =========================
        [RolRequerido(2)]
        public IActionResult ReportesAdmin()
        {
            CargarSesion();
            return View();
        }

        // =========================
        // PAGOS (GET)
        // =========================
        [SesionRequerida]
        [HttpGet]
        public IActionResult Pagos()
        {
            CargarSesion();

            var model = new PagosViewModel
            {
                Historial = ObtenerHistorial(),
                HistorialFiltrado = ObtenerHistorial()
            };

            return View(model);
        }

        // =========================
        // PAGOS (POST)
        // =========================
        [SesionRequerida]
        [HttpPost]
        public IActionResult Pagos(PagosViewModel model)
        {
            CargarSesion();

            model.Historial = ObtenerHistorial();

            // VALIDACIÓN
            if (model.PagoBase <= 0)
                ModelState.AddModelError("", "El pago base debe ser mayor a 0");

            decimal totalPorcentaje =
                model.Vegetacion + model.RecursosHidricos + model.Pendiente;

            // 🔥 LÍMITE CONFIGURABLE (40%)
            decimal porcentajeFinal = Math.Min(totalPorcentaje, model.MaxPorcentaje);

            if (!ModelState.IsValid)
            {
                model.HistorialFiltrado = model.Historial;
                return View(model);
            }

            // 🔥 CÁLCULO
            decimal ajuste = model.PagoBase * (porcentajeFinal / 100);
            model.TotalPago = model.PagoBase + ajuste;

            string periodo = $"{model.MesSeleccionado} {model.AnioSeleccionado}";

            // BUSCAR HISTORIAL
            model.PagoDetalle = model.Historial
                .FirstOrDefault(p =>
                    p.Propiedad == model.PropiedadSeleccionada &&
                    p.Periodo == periodo);

            if (model.PagoDetalle == null)
            {
                model.Mensaje = "No hay datos históricos, se muestra simulación.";

                model.PagoDetalle = new PagoHistorial
                {
                    Propiedad = model.PropiedadSeleccionada,
                    Periodo = periodo,
                    Monto = model.TotalPago,
                    Estado = "Simulado"
                };
            }

            // HISTORIAL FILTRADO
            model.HistorialFiltrado = model.Historial
                .Where(h => h.Propiedad == model.PropiedadSeleccionada)
                .ToList();

            // 🔥 PLAN DE PAGOS SIMULADO (6 meses)
            model.PlanPagos = new List<PagoHistorial>();

            for (int i = 0; i < 6; i++)
            {
                var fecha = DateTime.Now.AddMonths(i);

                model.PlanPagos.Add(new PagoHistorial
                {
                    Propiedad = model.PropiedadSeleccionada,
                    Periodo = fecha.ToString("MMMM yyyy"),
                    Monto = model.TotalPago,
                    Estado = "Pendiente"
                });
            }

            return View(model);
        }

        // =========================
        // MÉTODO PARA SESIÓN
        // =========================
        private void CargarSesion()
        {
            ViewBag.IdUsuario = HttpContext.Session.GetInt32("IdUsuario");
            ViewBag.Rol = HttpContext.Session.GetInt32("Rol");
        }

        // =========================
        // DATOS DE PRUEBA
        // =========================
        private List<PagoHistorial> ObtenerHistorial()
        {
            return new List<PagoHistorial>
            {
                new PagoHistorial
                {
                    Propiedad = "Finca Los Pinos",
                    Periodo = "Enero 2026",
                    Monto = 123000,
                    Estado = "Pagado"
                },
                new PagoHistorial
                {
                    Propiedad = "Finca La Esperanza",
                    Periodo = "Febrero 2026",
                    Monto = 110000,
                    Estado = "En proceso"
                }
            };
        }
    }
}