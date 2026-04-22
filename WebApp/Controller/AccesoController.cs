using Entities_DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace WebApp.Controllers
{
    public class AccesoController : Controller
    {
        private readonly HttpClient _http;

        public AccesoController(IHttpClientFactory httpClientFactory)
        {
            _http = httpClientFactory.CreateClient();
        }

        // LOGIN GET
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // LOGIN POST
        [HttpPost]
        public async Task<IActionResult> Login(string correo, string contrasena)
        {
            try
            {
                var payload = JsonSerializer.Serialize(new
                {
                    Correo = correo,
                    Contrasena = contrasena
                });

                var content = new StringContent(payload, Encoding.UTF8, "application/json");

                var response = await _http.PostAsync(
                    "https://ecommerce-w-apehakegexd0bedr.eastus-01.azurewebsites.net/api/Usuario/Login",
                    content);

                var json = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var data = JsonSerializer.Deserialize<SesionDTO>(json,
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                    if (data == null)
                    {
                        ViewBag.Error = "Error al procesar datos";
                        return View();
                    }

                    // GUARDAR SESIÓN
                    HttpContext.Session.SetInt32("IdUsuario", data.IdUsuario);
                    HttpContext.Session.SetInt32("Rol", data.Rol);

                    // 🔥 REDIRECCIÓN CORRECTA
                    return data.Rol == 1
                        ? RedirectToAction("PagosUsuario", "Home")
                        : RedirectToAction("Pagos", "Home");
                }

                ViewBag.Error = "*Credenciales incorrectas";
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error: " + ex.Message;
                return View();
            }
        }

        // REGISTER GET
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // REGISTER POST
        [HttpPost]
        public async Task<IActionResult> Register(string nombre, string correo, string contrasena)
        {
            try
            {
                var payload = JsonSerializer.Serialize(new
                {
                    Nombre = nombre,
                    Correo = correo,
                    Contrasena = contrasena
                });

                var content = new StringContent(payload, Encoding.UTF8, "application/json");

                var response = await _http.PostAsync(
                    "https://ecommerce-w-apehakegexd0bedr.eastus-01.azurewebsites.net/api/Usuario/Registrar",
                    content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Login");
                }

                var error = await response.Content.ReadAsStringAsync();
                ViewBag.Error = error;

                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error: " + ex.Message;
                return View();
            }
        }

        // LOGOUT
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Acceso");
        }

        // DEBUG SESIÓN
        [HttpGet]
        public IActionResult ObtenerSesion()
        {
            var idUsuario = HttpContext.Session.GetInt32("IdUsuario");
            var rol = HttpContext.Session.GetInt32("Rol");

            return Json(new { idUsuario, rol });
        }

        // RECOVERY
        [HttpGet]
        public IActionResult StartRecovery()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Recovery()
        {
            return View();
        }
    }
}