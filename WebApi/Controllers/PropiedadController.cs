using AppCore;
using Entities_DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropiedadController : ControllerBase
    {
        private readonly PropiedadManager _propiedadManager;
        private readonly EvaluacionManager _evaluacionManager;

        public PropiedadController(PropiedadManager propiedadManager, EvaluacionManager evaluacionManager)
        {
            _propiedadManager = propiedadManager;
            _evaluacionManager = evaluacionManager;
        }

        [HttpGet("reverse")]
        public async Task<IActionResult> Reverse(double lat, double lon)
        {
            using var client = new HttpClient();

            client.DefaultRequestHeaders.Add("User-Agent", "FincasApp/1.0 (jeanrva@gmail.com)");
            client.DefaultRequestHeaders.Add("Accept-Language", "es");

            var url = $"https://nominatim.openstreetmap.org/reverse?lat={lat}&lon={lon}&format=json";

            var response = await client.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();

            return Content(content, "application/json");
        }

        [HttpGet("Estado/{Estado}")]
        public IActionResult ObtenerPorEstado(string Estado)
        {
            var lista = _propiedadManager.ObtenerPorEstado(Estado);
            return Ok(lista);
        }

        [HttpGet("{id:int}")]
        public IActionResult ObtenerPorId(int id)
        {
            var propiedad = _propiedadManager.ObtenerPorId(id);

            if (propiedad == null)
                return NotFound();

            return Ok(propiedad);
        }

        [HttpPost("evaluar")]
        public IActionResult Evaluar([FromBody] EvaluacionDTO dto)
        {
            _evaluacionManager.Create(dto);
            return Ok();
        }

        [HttpGet("evaluacion/{id}")]
        public IActionResult ObtenerEvaluacion(int id)
        {
            var evaluacion = _evaluacionManager.ObtenerPorPropiedad(id);

            if (evaluacion == null)
                return NotFound();

            return Ok(evaluacion);
        }

        [HttpGet("usuario/{idUsuario}")]
        public IActionResult ObtenerPorUsuario(int idUsuario)
        {
            var lista = _propiedadManager.ObtenerPorUsuario(idUsuario);
            return Ok(lista);
        }

        [HttpGet("usuarioApproved/{idUsuario}")]
        public IActionResult ObtenerAprobadaPorUsuario(int idUsuario)
        {
            var lista = _propiedadManager.RetrieveApprovedPropertiesByUsuarioId(idUsuario);
            return Ok(lista);
        }

        

        [HttpGet("provincias")]
        public IActionResult Provincias()
        {
            return Ok(_propiedadManager.ObtenerProvincias());
        }

        // 🔹 CANTONES
        [HttpGet("cantones/{provincia}")]
        public IActionResult Cantones(string provincia)
        {
            return Ok(_propiedadManager.ObtenerCantones(provincia));
        }

        // 🔹 DISTRITOS
        [HttpGet("distritos/{canton}")]
        public IActionResult Distritos(string canton)
        {
            return Ok(_propiedadManager.ObtenerDistritos(canton));
        }

        // 🔥 REPORTES
        [HttpGet("reportes")]
        public IActionResult Reportes(
    string? provincia,
    string? canton,
    string? distrito,
    string? desde,
    string? hasta)
        {
            DateTime fechaDesde = new DateTime(1753, 1, 1);
            DateTime fechaHasta = DateTime.Now;

            if (!string.IsNullOrEmpty(desde))
                DateTime.TryParse(desde, out fechaDesde);

            if (!string.IsNullOrEmpty(hasta))
                fechaHasta = DateTime.Parse(hasta).AddDays(1); 

            return Ok(_evaluacionManager.ObtenerReportes(
                provincia ?? "",
                canton ?? "",
                distrito ?? "",
                fechaDesde,
                fechaHasta
            ));
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> CrearPropiedad([FromForm] PropiedadDTO propiedad)
        {
            try
            {

             //   var idUsuarioForm = Request.Form["IdUsuario"];

              

              //  propiedad.IdUsuario = int.Parse(idUsuarioForm);





                var test = propiedad.Latitud;
                var test2 = propiedad.Longitud;

                


                var idPropiedad = _propiedadManager.Create(propiedad);

                var rutasFotos = new List<string>();

                
                if (propiedad.Fotografias != null && propiedad.Fotografias.Count > 0)
                {
                    var carpetaDestino = Path.Combine(
                        Directory.GetCurrentDirectory(),
                        "wwwroot",
                        "uploads",
                        "propiedades"
                    );

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

                return Ok(new
                {
                    mensaje = "Propiedad guardada correctamente",

                    id = idPropiedad,
                   

                    fotos = rutasFotos
                });
            }
            catch (FormatException)
            {
                return BadRequest("Formato inválido en Latitud o Longitud.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}