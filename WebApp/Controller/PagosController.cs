using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class PagosController : Controller
    {
        public IActionResult Index()
        {
            return Content("MVC FUNCIONA");
        }
    }
}