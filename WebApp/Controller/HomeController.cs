using Entities_DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;
namespace WebApp.Controllers
{

    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            ViewBag.IdUsuario = HttpContext.Session.GetInt32("IdUsuario");
            ViewBag.Rol = HttpContext.Session.GetInt32("Rol");
            return View();
        }

        [RolRequerido(2)]
        public IActionResult Dashboard()
        {
            ViewBag.IdUsuario = HttpContext.Session.GetInt32("IdUsuario");
            ViewBag.Rol = HttpContext.Session.GetInt32("Rol");
            return View();
        }


        [RolRequerido(3)]
        public IActionResult ReportesIngeniero()
        {
            ViewBag.IdUsuario = HttpContext.Session.GetInt32("IdUsuario");
            ViewBag.Rol = HttpContext.Session.GetInt32("Rol");
            return View();
        }

        [SesionRequerida]
        public IActionResult RegistroPropiedades()
        {
            ViewBag.IdUsuario = HttpContext.Session.GetInt32("IdUsuario");
            ViewBag.Rol = HttpContext.Session.GetInt32("Rol");
            return View();
        }
        [SesionRequerida]
        public IActionResult Pagos()
        {
            ViewBag.IdUsuario = HttpContext.Session.GetInt32("IdUsuario");
            ViewBag.Rol = HttpContext.Session.GetInt32("Rol");
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
            ViewBag.IdUsuario = HttpContext.Session.GetInt32("IdUsuario");
            ViewBag.Rol = HttpContext.Session.GetInt32("Rol");

            var httpClient = new HttpClient();

            var propiedad = await httpClient.GetFromJsonAsync<PropiedadDTO>(
                $"https://localhost:7106/api/Propiedad/{id}"
            );

            return View(propiedad);
        }

        [RolRequerido(2)]
        public IActionResult ReportesAdmin()
        {
            ViewBag.IdUsuario = HttpContext.Session.GetInt32("IdUsuario");
            ViewBag.Rol = HttpContext.Session.GetInt32("Rol");
            return View();
        }




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

    }
}