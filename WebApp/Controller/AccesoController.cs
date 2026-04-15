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

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

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

                    HttpContext.Session.SetInt32("IdUsuario", data.IdUsuario);
                    HttpContext.Session.SetInt32("Rol", data.Rol);

                    // 🔥 REDIRECCIÓN SEGURA
                    return RedirectToAction("Index", "Home");
                }

                ViewBag.Error = json;
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error: " + ex.Message;
                return View();
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Acceso");
        }
    }
}