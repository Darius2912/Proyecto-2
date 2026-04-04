using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
 //   [SesionRequerida]
    public class HomeController : Controller
    {
        public IActionResult Index() => View();

        //[RolRequerido(2)]
        public IActionResult Dashboard()
        {
            ViewBag.IdUsuario = HttpContext.Session.GetInt32("IdUsuario");
            ViewBag.Rol = HttpContext.Session.GetInt32("Rol");
            return View();
        }


        
        public IActionResult Reportes()
        {
            ViewBag.IdUsuario = HttpContext.Session.GetInt32("IdUsuario");
            ViewBag.Rol = HttpContext.Session.GetInt32("Rol");
            return View();
        }
        

        public IActionResult RegistroPropiedades()
        {
            ViewBag.IdUsuario = HttpContext.Session.GetInt32("IdUsuario");
            ViewBag.Rol = HttpContext.Session.GetInt32("Rol");
            return View();
        }
        public IActionResult Pagos()
        {
            ViewBag.IdUsuario = HttpContext.Session.GetInt32("IdUsuario");
            ViewBag.Rol = HttpContext.Session.GetInt32("Rol");
            return View();
        }


        public IActionResult MisPropiedades()
        {
            ViewBag.IdUsuario = HttpContext.Session.GetInt32("IdUsuario");
            ViewBag.Rol = HttpContext.Session.GetInt32("Rol");
            return View();
        }
    }
}