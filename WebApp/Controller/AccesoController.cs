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
        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(string correo, string contrasena)
        {
            var payload = JsonSerializer.Serialize(new { Correo = correo, Contrasena = contrasena });
            var content = new StringContent(payload, Encoding.UTF8, "application/json");

            //  var response = await _http.PostAsync("https://localhost:7106/api/Usuario/Login", content);
            var response = await _http.PostAsync("https://ecommerce-w-apehakegexd0bedr.eastus-01.azurewebsites.net/api/Usuario/Login", content);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var data = JsonSerializer.Deserialize<SesionDTO>(json,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                HttpContext.Session.SetInt32("IdUsuario", data.IdUsuario);
                HttpContext.Session.SetInt32("Rol", data.Rol);

                return data.Rol == 1
                    ? RedirectToAction("Index", "Home")
                    : RedirectToAction("Dashboard", "Home");
            }

            ViewBag.Error = "Credenciales incorrectas";
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Acceso");
        }



        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }



        [HttpGet]
        public ActionResult StartRecovery()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Recovery()
        {
            return View();
        }





    }
}