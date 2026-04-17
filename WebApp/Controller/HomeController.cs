using Microsoft.AspNetCore.Authorization;
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
            ViewBag.IdUsuario = HttpContext.Session.GetInt32("IdUsuario");
            ViewBag.Rol = HttpContext.Session.GetInt32("Rol");
            return View();
        }

        // =========================
        // DASHBOARD
        // =========================
        [RolRequerido(2)]
        public IActionResult Dashboard()
        {
            ViewBag.IdUsuario = HttpContext.Session.GetInt32("IdUsuario");
            ViewBag.Rol = HttpContext.Session.GetInt32("Rol");
            return View();
        }

        // =========================
        // REPORTES
        // =========================
        [SesionRequerida]
        public IActionResult Reportes()
        {
            ViewBag.IdUsuario = HttpContext.Session.GetInt32("IdUsuario");
            ViewBag.Rol = HttpContext.Session.GetInt32("Rol");
            return View();
        }

        // =========================
        // REGISTRO PROPIEDADES
        // =========================
        [SesionRequerida]
        public IActionResult RegistroPropiedades()
        {
            ViewBag.IdUsuario = HttpContext.Session.GetInt32("IdUsuario");
            ViewBag.Rol = HttpContext.Session.GetInt32("Rol");
            return View();
        }

        // =========================
        // PAGOS (GET)
        // =========================
        [SesionRequerida]
        [HttpGet]
        public IActionResult Pagos()
        {
            ViewBag.IdUsuario = HttpContext.Session.GetInt32("IdUsuario");
            ViewBag.Rol = HttpContext.Session.GetInt32("Rol");

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
            ViewBag.IdUsuario = HttpContext.Session.GetInt32("IdUsuario");
            ViewBag.Rol = HttpContext.Session.GetInt32("Rol");

            model.Historial = ObtenerHistorial();

            // VALIDACIONES
            if (model.Hectareas <= 0)
                ModelState.AddModelError("", "Ingrese hectáreas válidas");

            if (model.PrecioPorHectarea <= 0)
                ModelState.AddModelError("", "Ingrese precio válido");

            if (!ModelState.IsValid)
            {
                model.HistorialFiltrado = model.Historial;
                return View(model);
            }

            // CÁLCULO BASE
            model.PagoBase = model.Hectareas * model.PrecioPorHectarea;

            decimal totalPorcentaje =
                model.Vegetacion + model.RecursosHidricos + model.Pendiente;

            decimal porcentajeFinal = Math.Min(totalPorcentaje, model.MaxPorcentaje);

            decimal ajuste = model.PagoBase * (porcentajeFinal / 100);
            model.TotalPago = model.PagoBase + ajuste;

            string periodo = model.AnioSeleccionado.ToString();

            // FACTURA
            model.NumeroFactura = Guid.NewGuid().ToString().Substring(0, 8).ToUpper();
            model.FechaPago = DateTime.Now;

            // DETALLE
            model.PagoDetalle = new PagoHistorial
            {
                Propiedad = model.PropiedadSeleccionada,
                Periodo = periodo,
                Monto = model.TotalPago,
                Estado = "Calculado"
            };

            // HISTORIAL
            model.HistorialFiltrado = model.Historial
                .Where(h => h.Propiedad == model.PropiedadSeleccionada)
                .ToList();

            // PLAN ANUAL
            model.PlanPagos = new List<PagoHistorial>();

            for (int i = 0; i < 12; i++)
            {
                var fecha = new DateTime(model.AnioSeleccionado, i + 1, 1);

                model.PlanPagos.Add(new PagoHistorial
                {
                    Propiedad = model.PropiedadSeleccionada,
                    Periodo = fecha.ToString("MMMM yyyy"),
                    Monto = model.TotalPago / 12,
                    Estado = "Pendiente"
                });
            }

            // AUDITORÍA
            model.Auditoria = $"Cálculo generado el {DateTime.Now}";

            return View(model);
        }

        // =========================
        // MIS PROPIEDADES
        // =========================
        [SesionRequerida]
        public IActionResult MisPropiedades()
        {
            ViewBag.IdUsuario = HttpContext.Session.GetInt32("IdUsuario");
            ViewBag.Rol = HttpContext.Session.GetInt32("Rol");
            return View();
        }

        // =========================
        // REPORTES ADMIN
        // =========================
        [RolRequerido(2)]
        public IActionResult ReportesAdmin()
        {
            ViewBag.IdUsuario = HttpContext.Session.GetInt32("IdUsuario");
            ViewBag.Rol = HttpContext.Session.GetInt32("Rol");
            return View();
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
                    Periodo = "2026",
                    Monto = 120000,
                    Estado = "Pagado"
                },
                new PagoHistorial
                {
                    Propiedad = "Finca La Esperanza",
                    Periodo = "2026",
                    Monto = 100000,
                    Estado = "Pendiente"
                }
            };
        }
    }
}