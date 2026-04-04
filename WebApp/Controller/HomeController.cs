using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    [SesionRequerida]
    public class HomeController : Controller
    {
        public IActionResult Index() => View();

        [RolRequerido(2)]
        public IActionResult Dashboard()
        {
            ViewBag.IdUsuario = HttpContext.Session.GetInt32("IdUsuario");
            ViewBag.Rol = HttpContext.Session.GetInt32("Rol");
            return View();
        }
    }
}