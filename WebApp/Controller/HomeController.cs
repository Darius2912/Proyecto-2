<<<<<<< HEAD
﻿using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using System.Linq;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
=======
﻿using Entities_DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;
namespace WebApp.Controllers
{

    public class HomeController : Controller
    {

>>>>>>> origin/integration-final
        public IActionResult Index()
        {
            CargarSesion();
            return View();
        }

        // 🔥 SOLO ADMIN
        [RolRequerido(2)]
        public IActionResult Dashboard()
        {
            CargarSesion();
            return View();
        }

<<<<<<< HEAD
        // 🔥 AMBOS (usuario y admin)
        [SesionRequerida]
        public IActionResult Reportes()
=======

        [RolRequerido(3)]
        public IActionResult ReportesIngeniero()
>>>>>>> origin/integration-final
        {
            CargarSesion();
            return View();
        }

        [SesionRequerida]
        public IActionResult RegistroPropiedades()
        {
            CargarSesion();
            return View();
        }

        [RolRequerido(1)]
        public async Task<IActionResult> MisPropiedades()
        {
            var idUsuario = HttpContext.Session.GetInt32("IdUsuario");

            ViewBag.IdUsuario = idUsuario;
            ViewBag.Rol = HttpContext.Session.GetInt32("Rol");

            var httpClient = new HttpClient();

            var propiedades = await httpClient.GetFromJsonAsync<List<PropiedadDTO>>(
                $"https://localhost:7106/api/Propiedad/usuario/{idUsuario}"
            );

            return View(propiedades);
        }

        [RolRequerido(1)]
        public async Task<IActionResult> DetallePropiedad(int id)
        {
<<<<<<< HEAD
            CargarSesion();
            return View();
        }

        // =========================
        // 🔥 PAGOS (REDIRECCIÓN POR ROL)
        // =========================
        [SesionRequerida]
        public IActionResult Pagos()
        {
            CargarSesion();

            int? rol = HttpContext.Session.GetInt32("Rol");

            if (rol == 2)
                return RedirectToAction("PagosAdmin");

            return RedirectToAction("PagosUsuario");
        }

        // =========================
        // 🔥 PAGOS ADMIN
        // =========================
        [RolRequerido(2)]
        public IActionResult PagosAdmin()
        {
            CargarSesion();

            var historial = ObtenerHistorial();

            var model = new PagosViewModel
            {
                Historial = historial,
                HistorialFiltrado = historial,
                FincasPendientes = historial.Where(x => x.Estado == "Pendiente").ToList(),
                TotalDeuda = historial.Where(x => x.Estado == "Pendiente").Sum(x => x.Monto),
                CantidadPendientes = historial.Count(x => x.Estado == "Pendiente")
            };

            return View("Pagos", model); // usa la vista admin
        }

        // =========================
        // 🔥 PAGOS USUARIO
        // =========================
        [SesionRequerida]
        public IActionResult PagosUsuario()
        {
            CargarSesion();

            var model = new PagosViewModel
            {
                PagoDetalle = new PagoHistorial
                {
                    Propiedad = "Finca Los Pinos",
                    Periodo = "Enero 2026",
                    Monto = 240000,
                    Estado = "Pendiente"
                },
                TotalPago = 240000,
                PagoMensual = 20000,
                NumeroFactura = "FAC-12345",
                FechaPago = DateTime.Now
            };

            model.PlanPagos = new List<PagoHistorial>();

            for (int i = 0; i < 12; i++)
            {
                var fecha = new DateTime(DateTime.Now.Year, i + 1, 1);

                model.PlanPagos.Add(new PagoHistorial
                {
                    Propiedad = "Finca Los Pinos",
                    Periodo = fecha.ToString("MMMM"),
                    Monto = model.PagoMensual,
                    Estado = i < 4 ? "Pagado" : "Pendiente"
                });
            }

            return View(model);
        }

        // 🔥 SOLO ADMIN
=======
            ViewBag.IdUsuario = HttpContext.Session.GetInt32("IdUsuario");
            ViewBag.Rol = HttpContext.Session.GetInt32("Rol");

            var httpClient = new HttpClient();

            var propiedad = await httpClient.GetFromJsonAsync<PropiedadDTO>(
                $"https://localhost:7106/api/Propiedad/{id}"
            );

            return View(propiedad);
        }

>>>>>>> origin/integration-final
        [RolRequerido(2)]
        public IActionResult ReportesAdmin()
        {
            CargarSesion();
            return View();
        }

<<<<<<< HEAD
        // =========================
        // SESIÓN
        // =========================
        private void CargarSesion()
        {
            ViewBag.IdUsuario = HttpContext.Session.GetInt32("IdUsuario");
            ViewBag.Rol = HttpContext.Session.GetInt32("Rol");
        }

        // =========================
        // DATOS
        // =========================
        private List<PagoHistorial> ObtenerHistorial()
        {
            return new List<PagoHistorial>
            {
                new PagoHistorial { Propiedad="Finca Los Pinos", Periodo="Enero 2026", Monto=120000, Estado="Pagado"},
                new PagoHistorial { Propiedad="Finca La Esperanza", Periodo="Febrero 2026", Monto=100000, Estado="Pendiente"},
                new PagoHistorial { Propiedad="Finca El Bosque", Periodo="Marzo 2026", Monto=150000, Estado="Pendiente"},
                new PagoHistorial { Propiedad="Finca Verde", Periodo="Abril 2026", Monto=90000, Estado="Pagado"},
                new PagoHistorial { Propiedad="Finca Santa Rosa", Periodo="Mayo 2026", Monto=200000, Estado="Pendiente"}
            };
        }
=======



        [RolRequerido(3)]
        public async Task<IActionResult> Evaluar(int id)
        {
            ViewBag.IdUsuario = HttpContext.Session.GetInt32("IdUsuario");
            ViewBag.Rol = HttpContext.Session.GetInt32("Rol");

            var httpClient = new HttpClient();

            var propiedad = await httpClient.GetFromJsonAsync<PropiedadDTO>(
                $"https://localhost:7106/api/Propiedad/{id}"
            );

            return View(propiedad);
        }

        [RolRequerido(3)]
        public async Task<IActionResult> VisitaTecnica()
        {
            ViewBag.IdUsuario = HttpContext.Session.GetInt32("IdUsuario");
            ViewBag.Rol = HttpContext.Session.GetInt32("Rol");

            var httpClient = new HttpClient();

            var pendientes = await httpClient.GetFromJsonAsync<List<PropiedadDTO>>(
                "https://localhost:7106/api/Propiedad/Estado/Pendiente"
            );

            var aprobadas = await httpClient.GetFromJsonAsync<List<PropiedadDTO>>(
                "https://localhost:7106/api/Propiedad/Estado/Aprobada"
            );

            var rechazadas = await httpClient.GetFromJsonAsync<List<PropiedadDTO>>(
                "https://localhost:7106/api/Propiedad/Estado/Rechazada"
            );

            var todas = new List<PropiedadDTO>();
            if (pendientes != null) todas.AddRange(pendientes);
            if (aprobadas != null) todas.AddRange(aprobadas);
            if (rechazadas != null) todas.AddRange(rechazadas);

            return View(todas);
        }

>>>>>>> origin/integration-final
    }
}