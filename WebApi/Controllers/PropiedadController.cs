using AppCore;
using Entities_DTOs;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropiedadController : ControllerBase
    {

        private readonly PropiedadManager _propiedadManager;


        public PropiedadController(PropiedadManager propiedadManager)
        {
            _propiedadManager = propiedadManager;
        }

        [HttpGet("reverse")]
        public async Task<IActionResult> Reverse(double lat, double lon)
        {
            using var client = new HttpClient();

            client.DefaultRequestHeaders.Add("User-Agent", "FincasApp/1.0 (contacto@tufinca.com)");
            client.DefaultRequestHeaders.Add("Accept-Language", "es");

            var url = $"https://nominatim.openstreetmap.org/reverse?lat={lat}&lon={lon}&format=json";

            var response = await client.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();

            return Content(content, "application/json");
        }
        

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> CrearPropiedad([FromForm] PropiedadDTO propiedad)
        {
            try
            {
                var idPropiedad = _propiedadManager.Create(propiedad);
                var rutasFotos = new List<string>();

                if (propiedad.Fotografias != null && propiedad.Fotografias.Count > 0)
                {
                    var carpetaDestino = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "propiedades");

                    if (!Directory.Exists(carpetaDestino))
                        Directory.CreateDirectory(carpetaDestino);

                    foreach (var foto in propiedad.Fotografias)
                    {
                        var nombreArchivo = Guid.NewGuid().ToString() + Path.GetExtension(foto.FileName);
                        var rutaFisica = Path.Combine(carpetaDestino, nombreArchivo);

                        using (var stream = new FileStream(rutaFisica, FileMode.Create))
                        {
                            await foto.CopyToAsync(stream);
                        }

                        var rutaGuardar = "/uploads/propiedades/" + nombreArchivo;
                        rutasFotos.Add(rutaGuardar);
                        _propiedadManager.CreateFoto(idPropiedad, rutaGuardar);
                    }
                }

                // 1. Guardar la propiedad en BD
           
                // 2. Obtener el Id generado
                // 3. Guardar cada ruta en PropiedadFoto
             

                return Ok(new
                {
                    mensaje = "Propiedad guardada correctamente",
                    fotos = rutasFotos
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }


}

